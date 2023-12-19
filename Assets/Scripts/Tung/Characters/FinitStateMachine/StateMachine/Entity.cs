using System;
using System.Collections.Generic;
using Obvious.Soap;
using Obvious.Soap.Example;
using UnityEngine;

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

        public bool IsIdle
        {
            get => _isIdle;
            set => _isIdle = value;
        }
        public float moveSpeed = 5f;

        public List<Transform> placeAttack;
        public List<bool> slotPlaceAttack;

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

        public void Move(Transform target)
        {
            var position = transform.position;
            var dir = target.position - position;
            dir.Normalize();
            position += dir * (moveSpeed * Time.deltaTime);
            transform.position = position;
        }
        
        public Vector3 CheckSlot()
        {
            for (int i = 0; i < 4; i++)
            {
                if (!slotPlaceAttack[i])
                {
                    return placeAttack[i].position;
                }
            }

            return Vector3.zero;
        }
        // public bool CheckMove()
        // {
        //     var temp = Vector3.Distance(transform.position, enemy.transform.position);
        //     return temp > 1;
        // }
    }
}