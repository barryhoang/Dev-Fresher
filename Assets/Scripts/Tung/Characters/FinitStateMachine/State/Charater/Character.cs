
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Tung
{
    public class Character : Entity
    {
       [SerializeField] private Weapon _weapon;
       [SerializeField] private ScriptableEventFloat _characterDamageEnemy;
       [SerializeField] private ScriptableListEnemy _listSoapEnemy;
       [SerializeField] private ScriptableListCharacter _listCharacter;
       public List<Enemy> temp;
       private Enemy _enemyWork;

       public ScriptableListEnemy ListEnemy => _listSoapEnemy;
       public Weapon Weapon => _weapon;    
       public WeaponAttackCharacter WeaponAttack { get; private set; }
        public MoveCharacter MoveCharacter { get; private set; }
        public IdleCharacter IdleCharacter { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            IdleCharacter = new IdleCharacter(this,StateMachine,NameAnimation.IDLE,this);
            MoveCharacter = new MoveCharacter(this,StateMachine,NameAnimation.MOVE,gameObject,this );
            WeaponAttack = new WeaponAttackCharacter(this,StateMachine,NameAnimation.ATTACK,this);
            
            
        }

        protected override void Start()
        {
            base.Start();
            StateMachine.InitiateState(IdleCharacter);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,_raidusAttack);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _listCharacter.Add(this);
        }

        protected override void OnDisable()
        {
            _listCharacter.Remove(this);
        }
        public Vector3 GetTarget()
        {
            if (!_listSoapEnemy.IsEmpty)
            {
                var enemies = _listSoapEnemy.GetClosest(transform.position);
                temp = enemies;
                if(enemies.Count > 0)
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].gameObject.activeInHierarchy)
                        {
                            return enemies[i].gameObject.transform.position;
                        }
                    }
            }
            return Vector3.zero;
        }

        public void SetWeaponAttack() => _weapon.isAttacking = !_weapon.isAttacking;
    }
}