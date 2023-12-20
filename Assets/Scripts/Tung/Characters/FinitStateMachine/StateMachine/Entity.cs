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
        #region UnityVariable
        public AnimationController _animatorController;
       
        #endregion

        #region State
        public StateMachine StateMachine { get; private set; }
        #endregion
        
        #region OtherVariable
        
        private bool _isIdle;
       [SerializeField] private int _facingRight = 1;
       
       public bool IsIdle
        {
            get => _isIdle;
            set => _isIdle = value;
        }
        public float moveSpeed = 5f;

       public List<Transform> posAttack; 
       public List<bool> isFull;
       protected int _indexWork;
        #endregion
        
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
        
        #endregion

        public void Move(Vector3 target)
        {
            var position = transform.position;
            var dir = target - position;
            dir.Normalize();
            position += dir * (moveSpeed * Time.deltaTime);
            transform.position = position;
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
        public bool CheckMoveTarget(Vector3 target)
        {
            var distance = Vector3.Distance(target, transform.position);
            return distance <= 0.1f;
        }
    }
}