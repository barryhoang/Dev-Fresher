using System;
using Obvious.Soap;
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
        public StateMachine StateMachine { get; private set; }
        
        public MoveState MoveState { get; private set; }
        public AttackState AttackState { get; private set; }
        
        #endregion
        #region OtherVariable

        private Weapon _weapon;
        private bool _isIdle;
        [SerializeField] private ScriptableEventNoParam _fight;
        
        public GameObject enemy;
        public float moveSpeed = 5f;

        #endregion

        #region UnityFunciton
        private void Awake()
        {
            StateMachine = new StateMachine();
            
            MoveState = new MoveState(this,StateMachine,"Move",gameObject);
            AttackState = new AttackState(this,StateMachine,"Attack");
            
            StateMachine.InitiateState(MoveState);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _weapon = transform.GetChild(0).GetComponent<Weapon>();
        }

        private void Update()
        {
            StateMachine.currentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.currentState.PhysicalUpdate();
        }
        #endregion
        
        public void SetWeaponAttack() => _weapon._isAttacking = !_weapon._isAttacking;
        public void SetIdleState() => _isIdle = !_isIdle;
        
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