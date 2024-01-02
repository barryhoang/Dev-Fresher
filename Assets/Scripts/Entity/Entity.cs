using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Animation;
using Apathfinding;
using MEC;
using Obvious.Soap;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Entity
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityData _entityData;
        [SerializeField] protected FloatVariable _speed;
        [SerializeField] protected Tilemap _tilemap;
        [SerializeField] protected StatPanel _panelStats;
        [SerializeField] protected bool _isDrag;
        [SerializeField] protected GridMapVariable _gridMap;
        
        [SerializeField] private Image healthBar;
        [SerializeField] private AnimationController _aniController;
       
        private Entity _objectAttack;
        private float _currentHealth;
        
        public Vector3 posStart;
        public bool isMove;
        public bool isMoving;
        public bool isReadyAttack;
        public bool isAttack;
        public bool isAttacking;
        public Vector3Int posAttack;
        public List<PathNode> pathNodes;

        protected virtual void OnEnable()
        {
            _aniController.SetAnimation(AnimationName.Idle,true);
            posStart = _tilemap.WorldToCell(transform.position);
            _panelStats.SetText(_entityData);
            _currentHealth = _entityData.InitHealth.Value;
            _gridMap.Value[(int) transform.position.x, (int) transform.position.y] = true;
        }
        protected virtual void OnDisable()
        {
            transform.position = posStart;
        }

        private void OnMouseEnter()
        {
            if(!_isDrag)
                _panelStats.gameObject.SetActive(true);
        }
        private void OnMouseExit()
        {
            _panelStats.gameObject.SetActive(false);
        }

        public IEnumerator<float> Move(Entity entity)
        {
            if (isAttacking || entity == null)
            {
                isMove = false;
            }
            int currentPathIndex = 0;
            var position = transform.position;
            var currentX = (int) position.x;
            var currentY = (int) position.y;
            Vector3Int temp = new Vector3Int(Mathf.RoundToInt(entity.transform.position.x),
                Mathf.RoundToInt(entity.transform.position.y),0);
            var targetX = DirTarget(temp).x;
            var targetY = DirTarget(temp).y;
            pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY);
            while (currentPathIndex < pathNodes.Count)
            {
                PathNode currentNode = pathNodes[currentPathIndex];
                Vector3 targetPosition = new Vector3(currentNode.xPos,currentNode.yPos,0);
                if (!_gridMap.Value[currentNode.xPos,currentNode.yPos])
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                    {
                        if (currentPathIndex > 0)
                        {
                            _gridMap.Value[pathNodes[currentPathIndex - 1].xPos, pathNodes[currentPathIndex - 1].yPos] =
                                false;
                        }
                        _gridMap.Value[currentNode.xPos, currentNode.yPos] = true;
                        currentPathIndex++;
                        if (currentPathIndex <= pathNodes.Count - 1)
                        {
                            if (_gridMap.Value[pathNodes[currentPathIndex].xPos,
                                pathNodes[currentPathIndex].yPos])
                            {
                                pathNodes.Clear();
                                currentX = (int) transform.position.x;
                                currentY = (int) transform.position.y;
                                targetX = DirTarget(temp).x;
                                targetY = DirTarget(temp).y;
                                pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY);
                                currentPathIndex = 0;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Current node is occupied. Cannot move." + " " + gameObject.name);
                   isMove = false;
                }

                yield return Timing.WaitForOneFrame;
            }
        }
        private Vector3Int DirTarget(Vector3Int temp)
        {
            float distanceMin = 1000f;
            int x = 0;
            int y = 0;
            if (!_gridMap.Value[temp.x - 1, temp.y])
            {
                x = -1;
                distanceMin = Vector2.Distance(transform.position, new Vector2(temp.x - 1, temp.y));
            }
            if (!_gridMap.Value[temp.x + 1, temp.y])
            {
                float distanceRight = Vector2.Distance(transform.position, new Vector2(temp.x + 1, temp.y));
                if (distanceRight < distanceMin)
                {
                    x = 1;
                    y = 0;
                    distanceMin = distanceRight;
                }
            }
            if (!_gridMap.Value[temp.x, temp.y + 1] && temp.y + 1 < _gridMap.size.y)
            {
                float distanceUp = Vector2.Distance(transform.position, new Vector2(temp.x, temp.y + 1));
                if (distanceUp < distanceMin)
                {
                    x = 0;
                    y = 1;
                    distanceMin = distanceUp;
                }
            }
            if (!_gridMap.Value[temp.x, temp.y - 1] && temp.y - 1 > 0)
            {
                float distanceDown = Vector2.Distance(transform.position, new Vector2(temp.x, temp.y - 1));
                if (distanceDown < distanceMin)
                {
                    x = 0;
                    y = -1;
                    distanceMin = distanceDown;
                }
            }
            return new Vector3Int(temp.x + x,temp.y + y);
        }
        public void ResetPosAndState()
        {
            _panelStats.SetText(_entityData);
            _currentHealth = _entityData.InitHealth.Value;
            transform.position = posStart;
            transform.GetChild(0).position = posStart;
            healthBar.fillAmount = 1;
            isReadyAttack = false;
            isAttacking = false;
            isAttack = false;
            _aniController.SetAnimation(AnimationName.Move,false);
            _aniController.SetAnimation(AnimationName.Idle,true);
        }
        public IEnumerator<float> Attacking()
        {
            while (isAttack)
            {
                isAttacking = true;
                //TODO amimation Attack
                _aniController.SetAnimation(AnimationName.Attack,true);
                _objectAttack.TakeDamage(this,1);
                if (!_objectAttack.gameObject.activeInHierarchy)
                {
                    transform.GetChild(0).localPosition = Vector3.zero;
                    isReadyAttack = false;
                    isAttacking = false;
                    isAttack = false;
                }
                yield return Timing.WaitForSeconds(_entityData.InitAttackSpeed);
            }
        }

        public void TakeDamage(Entity entity,float damage)
        {
            //TODO animation Hurt
            if (!isAttacking)
            {
                isAttacking = true;
                isAttack = true;
                _objectAttack = entity;
                transform.position = _tilemap.WorldToCell(transform.position);
                Timing.RunCoroutine(Attacking().CancelWith(gameObject));
            }
            _aniController.SetAnimation(AnimationName.Hit,true);
            _currentHealth--;
            healthBar.fillAmount = _currentHealth / _entityData.InitHealth;
            if (_currentHealth <= 0)
            {
                //TODO animation Death
                _aniController.SetAnimation(AnimationName.Dead,true);
                gameObject.SetActive(false);
            }
        }
    }
}


// public void Move(Entity entity, Vector3Int targetPos)
// {
//     if (isAttacking || entity == null)
//     {
//         return;
//     }
//     _gridMap.Value[(int) transform.position.x, (int) transform.position.y] = false;
//     var position = transform.position;
//     var currentX = (int) position.x;
//     var currentY = (int) position.y;
//     var targetX = targetPos.x;
//     var targetY = targetPos.y;
//     pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY);
//     if (pathNodes.Count > 0 && pathNodes != null)
//     {
//         // if (_gridMap.Value[pathNodes[1].xPos, pathNodes[1].yPos] == true)
//         // {
//         //     pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY);
//         // }
//         var target = new Vector3(pathNodes[0].xPos,pathNodes[0].yPos);
//         var dir = (target - transform.position).normalized;
//         transform.position += _speed * Time.deltaTime *dir;
//         if (Vector3.Distance(targetPos, transform.position) <= 0.1f)
//         {
//             transform.position = _tilemap.WorldToCell(transform.position);
//             _objectAttack = entity;
//             isReadyAttack = true;
//             _gridMap.Value[(int) transform.position.x, (int) transform.position.y] = true;
//             
//             _aniController.SetAnimation(AnimationName.Move, false);
//         }
//     }
//     else
//     {
//         transform.position = _tilemap.WorldToCell(transform.position);
//     }
// }




// public void Move(Entity entity)
// {
//     if (isAttacking || entity == null)
//     {
//         return;
//     }
//     
//     _aniController.SetAnimation(AnimationName.Idle,false);
//     
//     var posTaget  = entity.transform.position;
//     var position = transform.position;
//     var distance = Vector3.Distance(position, posTaget);
//     if (distance <= 1f)
//     {
//         isReadyAttack = true;
//         _objectAttack = entity;
//         _aniController.SetAnimation(AnimationName.Move,false);
//         return;
//     }
//     
//     Vector2 dir = posTaget - position;
//     dir.Normalize();
//     position += (Vector3)(_speed * Time.deltaTime * dir);
//     
//     //TODO amimation Move
//     _aniController.SetAnimation(AnimationName.Move,true);
//     transform.position = position;
// }

//        public IEnumerator<float> Move(Entity entity)
//        {
//            if (isAttacking || entity == null)
//            {
//                isMove = false;
//            }
//
//            while (isMove)
//            {
//                _aniController.SetAnimation(AnimationName.Idle,false);
//                var currentX = (int) transform.position.x;
//                var currentY = (int) transform.position.y;
//                
//                int targetOldX = -1;
//                int targetOldY = -1;
//                var targetX = (int) posTarget.x;
//                var targetY = (int) posTarget.y;
//                var pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY);
//            
//                _aniController.SetAnimation(AnimationName.Move,true);
//                isMoving = true;
//                targetX = (int) posTarget.x;
//                targetY = (int) posTarget.y;
//                   
//                if (targetOldX != targetX || targetOldY != targetY)
//                { 
//                    currentX = (int) transform.position.x;
//                    currentY = (int) transform.position.y;
//                    targetOldX = targetX;
//                    targetOldY = targetY;
//                    pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY);
//                    Debug.Log("A");
//                    currentIndex = 0;
//                } 
//                   
//                if (pathNodes != null)
//                {
//                    var target = new Vector3(pathNodes[currentIndex].xPos,pathNodes[currentIndex].yPos);
//                    if (Vector3.Distance(target, transform.position) > 0.1f)
//                    {
//                        var dir = (target - transform.position).normalized;
//                        transform.position += dir * _speed * Time.deltaTime;
//                        this.pathNodes = pathNodes;
//                    }
//                    else
//                    {
//                        currentIndex++;
//                        if (pathNodes.Count <= currentIndex)
//                        {
//                            currentIndex = 0;
//                            _objectAttack = entity;
//                            isReadyAttack = true;
//                            isMove = false;
//                            _aniController.SetAnimation(AnimationName.Move,false);
//                        }
//                    }
//                }
//                yield return Timing.WaitForOneFrame;
//            }
//        }
