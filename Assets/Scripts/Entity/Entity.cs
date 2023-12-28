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

        public Vector3 posStart;
        public bool isMoving;
        public bool isMove;
        public bool isReadyAttack;
        public bool isAttack;
        public bool isAttacking;
        public List<PosAttack> posAttacks;

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

        public IEnumerator<float> Move(Entity entity,Vector3 posAttack)
        {
            if (isAttacking || entity == null)
            {
                isMove = false;
            }
            while (isMove)
            {
                isMoving = true;
                var currentX = (int) transform.position.x;
                var currentY = (int) transform.position.y;
                var targetX = (int) posAttack.x;
                var targetY = (int) posAttack.y;
            
                var pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX, targetY, transform.position);

                if (pathNodes.Count > 0)
                {
                    Debug.Log(GridManager.instance.gridMap.Get(pathNodes[0].xPos,pathNodes[0].yPos));
                    var target = new Vector3(pathNodes[0].xPos,pathNodes[0].yPos);
                    var dir = (target - transform.position).normalized;
                    transform.position += dir * _speed * Time.deltaTime;
                }

                if (Vector3.Distance(posAttack, transform.position) < 0.1f)
                {
                    _objectAttack = entity;
                    isReadyAttack = true;
                    isMove = false;
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
