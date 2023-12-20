using System;
using System.Collections.Generic;
using Obvious.Soap;
using Obvious.Soap.Example;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tung
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private int _facingRight = 1;
        private bool _isIdle;
        
        
        public StateMachine StateMachine { get; private set; }
        public bool IsIdle
        {
            get => _isIdle;
            set => _isIdle = value;
        }
        public AnimationController _animatorController;
        public LayerMask layerEntity;
        public CharacterHeath HeathEntity => _heathEntity;
        public float moveSpeed = 5f;
       
       protected CharacterHeath _heathEntity;
       [SerializeField] protected float _raidusAttack;
       
       #region UnityFunciton
        protected virtual void Awake()
        {
            StateMachine = new StateMachine();
            _isIdle = true;
        }

        protected virtual void Update()
        {
            StateMachine.currentState.LogicUpdate();
        }

        protected virtual void FixedUpdate()
        {
            StateMachine.currentState.PhysicalUpdate();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,_raidusAttack);
        }
        #endregion

        public bool CheckRangeAttack()
        {
            var collider = Physics2D.OverlapCircleAll(transform.position, _raidusAttack, layerEntity);

            foreach (var coll in collider)
            {
                if (coll != null)
                {
                    Debug.Log(coll.name);
                    _heathEntity = coll.GetComponent<CharacterHeath>();       
                    return false;
                }   
            }
            return true;
        }
        
        public void ShouldFlip(Vector3 target)
        {
            var temp = target.x - transform.position.x;
            int value = 1;
            if (temp > 0)
            {
                value = 1;
            }
            else if (temp < 0)
            {
                value = -1;
            }

            if (value != _facingRight)
            {
                Flip();
            }
        }

        public void Flip()
        {
            _facingRight *= -1;
            transform.Rotate(0,180,0);
        }
    }
}