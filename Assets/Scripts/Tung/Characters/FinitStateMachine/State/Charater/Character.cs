
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;

namespace Tung
{
    public class Character : Entity
    {
       [SerializeField] private Weapon _weapon;
       [SerializeField] private ScriptableEventFloat _characterDamageEnemy;
       [SerializeField] private ScriptableListEnemy _listSoapEnemy;
       [SerializeField] private ScriptableListCharacter _listCharacter;
       private Enemy _enemyWork;
       public int indexWork;
       public Enemy EnemyWork => _enemyWork;
       public WeaponAttackCharacter WeaponAttack { get; private set; }
        public MoveCharacter MoveCharacter { get; private set; }
        public IdleCharacter IdleCharacter { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            IdleCharacter = new IdleCharacter(this,StateMachine,NameAnimation.IDLE,this);
            MoveCharacter = new MoveCharacter(this,StateMachine,NameAnimation.MOVE,gameObject,this );
            WeaponAttack = new WeaponAttackCharacter(this,StateMachine,NameAnimation.ATTACK,this);
            _listCharacter.Add(this);
            
            StateMachine.InitiateState(IdleCharacter);
        }
        
        public void OnDisable()
        {
            _listCharacter.Remove(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")  && _weapon._isAttacking)
            {
                _characterDamageEnemy.Raise(1); ;
            }
        }

        public void GetTarget()
        {
            var enemy = _listSoapEnemy.GetClosest(transform.position);
            _enemyWork = enemy;
            for (int i = 0; i < _enemyWork.isFull.Count; i++)
            {
                if (!_enemyWork.isFull[i])
                {
                    indexWork = i;
                    _enemyWork.isFull[i] = true;
                    return;
                }
            }
        }
        
        public void SetWeaponAttack() => _weapon._isAttacking = !_weapon._isAttacking;
    }
}