using System.Collections.Generic;
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
        public bool isMoving;
        public bool isMove;
        public bool isReadyAttack;
        public bool isAttack;
        public bool isAttacking;
        public List<PosAttack> posAttacks;
        public List<PathNode> PathNodes;

        protected virtual void OnEnable()
        {
            _aniController.SetAnimation(AnimationName.Idle,true);
            posStart = _tilemap.WorldToCell(transform.position);
            _panelStats.SetText(_entityData);
            _currentHealth = _entityData.InitHealth.Value;
            _gridMap.Value[0, 1] = 2;
            Debug.Log(_gridMap.Value[0,1]);
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

        public IEnumerator<float> Move(Entity entity,int index)
        {
            if (isAttacking || entity == null)
            {
                isMove = false;
            }
            _aniController.SetAnimation(AnimationName.Idle,false);
            _aniController.SetAnimation(AnimationName.Move,true);
            
            var currentX = (int) transform.position.x;
            var currentY = (int) transform.position.y;
            
            int targetOldX = -1;
            int targetOldY = -1;
            var targetX = (int) entity.posAttacks[index].posAttack.position.x;
            var targetY = (int) entity.posAttacks[index].posAttack.position.y;
            var pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY, transform.position);
            int currentIndex = 0;
            while (isMove)
            {
                _aniController.SetAnimation(AnimationName.Move,true);
                isMoving = true;
                targetX = (int) entity.posAttacks[index].posAttack.position.x;
                targetY = (int) entity.posAttacks[index].posAttack.position.y;
               
                if (targetOldX != targetX || targetOldY != targetY)
                {
                    pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY, transform.position);
                    // currentIndex = 0;
                    targetOldX = targetX;
                    targetOldY = targetY;
                } 
               
                if (pathNodes != null)
                {
                    var target = new Vector3(pathNodes[currentIndex].xPos,pathNodes[currentIndex].yPos);
                    if (Vector3.Distance(target, transform.position) > 0.1f)
                    {
                        var dir = (target - transform.position).normalized;
                        transform.position += dir * _speed * Time.deltaTime;
                        PathNodes = pathNodes;
                    }
                    else
                    {
                        currentIndex++;
                        if (pathNodes.Count <= currentIndex)
                        {
                            // currentIndex = 0;
                            _objectAttack = entity;
                            isReadyAttack = true;
                            isMove = false;
                            _aniController.SetAnimation(AnimationName.Move,false);
                        }
                    }
                   
                }
                yield return Timing.WaitForOneFrame;
            }
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
                _objectAttack.TakeDamage();
                if (!_objectAttack.gameObject.activeInHierarchy)
                {
                    isReadyAttack = false;
                    isAttacking = false;
                    isAttack = false;
                }
                yield return Timing.WaitForSeconds(_entityData.InitAttackSpeed);
            }
        }

        public void TakeDamage()
        {
            //TODO animation Hurt
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

[System.Serializable]
public class PosAttack
{
    public Transform posAttack;
    public bool isFull;
}

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
