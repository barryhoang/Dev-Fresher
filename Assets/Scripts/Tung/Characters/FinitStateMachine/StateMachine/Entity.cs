using System;
using Obvious.Soap.Example;
using UnityEngine;

namespace Tung
{
    public class Entity : MonoBehaviour
    {
        #region UnityVariable
        private Animator _animator;

        public Animator Animator => _animator;
        #endregion

        #region State
        private StateMachine _stateMachine;
        
        public MoveState MoveState { get; private set; }
        
        #endregion

        #region OtherVariable

        public GameObject enemy;
        
        public float moveSpeed = 5f;
        
        #endregion

        #region UnityFunciton
        private void Awake()
        {
            _stateMachine = new StateMachine();
            
            MoveState = new MoveState(this,_stateMachine,"Move",gameObject);
            
            _stateMachine.InitiateState(MoveState);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _stateMachine.currentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.currentState.PhysicalUpdate();
        }
        #endregion


        public void Move()
        {
            var position = transform.position;
            var dir = enemy.transform.position - position;
            dir.Normalize();
            position += dir * (moveSpeed * Time.deltaTime);
            transform.position = position;
        }

        public bool CheckMove()
        {
            return Vector3.Distance(transform.position, enemy.transform.position)  > 1f;
        }
    }
}