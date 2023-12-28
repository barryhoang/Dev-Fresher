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
        [SerializeField] protected bool isDrag;
        
        [SerializeField] private Image healthBar;
        [SerializeField] private AnimationController _aniController;
       
        private Entity _objectAttack;
        private float _currentHealth;
        private int _currentIndex =0;
        
        public Vector3 posStart;
        public bool isMove;
        public bool isReadyAttack;
        public bool isAttack;
        public bool isAttacking;
        public List<Transform> posAttack;
        public List<bool> isFull;
        
        
        protected virtual void OnEnable()
        {
            _aniController.SetAnimation(AnimationName.Idle,true);
            posStart = _tilemap.WorldToCell(transform.position);
            _panelStats.SetText(_entityData);
            _currentHealth = _entityData.InitHealth.Value;
        }
        protected virtual void OnDisable()
        {
            transform.position = posStart;
        }

        private void OnMouseEnter()
        {
            if(!isDrag)
                _panelStats.gameObject.SetActive(true);
        }
        private void OnMouseExit()
        {
            _panelStats.gameObject.SetActive(false);
        }

        public void Move(Entity entity,PathNode pathNodes,int index)
        {
            if (isAttacking || entity == null)
            {
                return;
            }
            if (pathNodes != null)
            {
                isMove = true;
                Vector3 positionTarget = entity.posAttack[index].transform.position;
                if (Vector3.Distance(positionTarget, transform.position) > 0.1f)
                {
                    Vector3 dir = (positionTarget - transform.position).normalized;
                    transform.position += dir * _speed * Time.deltaTime;
                }
                else
                {
                    entity.isFull[index] = true;
                    _objectAttack = entity;
                    GridManager.instance.gridMap.Set(pathNodes.xPos,pathNodes.yPos,2);
                    // isReadyAttack = true;
                }
                // else
                // {
                //     // GridManager.instance.gridMap.Set((int) posStart.x, (int) posStart.y,2);
                // }      
            }

                //     if (_currentIndex >= pathNodes.Count) _currentIndex = 0;
            //     PathNode currentNode = pathNodes[_currentIndex];
            //     Vector3 positionTarget = new Vector3(currentNode.xPos,currentNode.yPos,0);
            //     if (Vector3.Distance(positionTarget, transform.position) > 0.1f)
            //     {
            //         Vector3 dir = (positionTarget - transform.position).normalized;
            //         transform.position += dir * _speed * Time.deltaTime;
            //         
            //     }
            //     else
            //     {
            //         _currentIndex++;
            //         if (_currentIndex >= pathNodes.Count)
            //         {
            //             GridManager.instance.gridMap.Set((int) posStart.x, (int) posStart.y,2);
            //             _currentIndex = 0;
            //             entity.isFull[index] = true;
            //             isReadyAttack = true;
            //             _objectAttack = entity;
            //             transform.position = _tilemap.WorldToCell(transform.position);
            //         }
            //     }
      
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
