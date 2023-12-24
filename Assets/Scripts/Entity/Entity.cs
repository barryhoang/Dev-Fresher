using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entity
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityData _entityData;
        [SerializeField] protected FloatVariable _speed;

        [SerializeField] private Image healthBar;
        [SerializeField] private GameObject _panelStats;
        private Vector3 posStart;
        private Entity _objectAttack;
        
        private float _currentHealth;

        public bool isReadyAttack;
        public bool isAttack;
        public bool isAttacking;
        
        protected virtual void OnEnable()
        {
            posStart = transform.position;
            _currentHealth = _entityData.InitHealth.Value;
            Debug.Log(_currentHealth);
        }
        protected virtual void OnDisable()
        {
            transform.position = posStart;
        }

        private void OnMouseEnter()
        {
            _panelStats.SetActive(true);
        }
        private void OnMouseExit()
        {
            _panelStats.SetActive(false);
        }

        public void Move(Entity entity)
        {
            var posTaget = entity.transform.position;
            var position = transform.position;
            var distance = Vector3.Distance(position, posTaget);
            if (distance <= 1f)
            {
                isReadyAttack = true;
                _objectAttack = entity;
                return;
            }
            Vector2 dir = posTaget - position;
            dir.Normalize();
            position += (Vector3)(_speed * Time.deltaTime * dir);
            //TODO amimation Move
            transform.position = position;
        }

        public void ResetPosAndState()
        {
            transform.position = posStart;
            _currentHealth = _entityData.InitHealth.Value;
            healthBar.fillAmount = 1;
            isReadyAttack = false;
            isAttacking = false;
            isAttack = false;
        }
        public IEnumerator<float> Attacking()
        {
            while (isAttack)
            {
                isAttacking = true;
                //TODO amimation Attack
                _objectAttack.TakeDamage();
                if (!_objectAttack.gameObject.activeInHierarchy)
                {
                    isReadyAttack = false;
                    isAttacking = false;
                    yield break;
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }

        public void TakeDamage()
        {
            //TODO animation Hurt
            _currentHealth--;
            healthBar.fillAmount = _currentHealth / _entityData.InitHealth;
            if (_currentHealth <= 0)
            {
                //TODO animation Death
                gameObject.SetActive(false);
            }
        }

    }
}
