using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class Enemy : Entity
    {
            [SerializeField] private ScriptableListEnemy _listEnemy;
            [SerializeField] private ScriptableListCharacter _listCharacter;
            private Character _characterWork;
            public bool isDeath;
            public IdleEnemy IdleEnemy { get; private set; }
            public MoveEnemy MoveEnemy { get; private set; }
            public AttackEnemy AttackEnemy { get; private set; }

            protected override void Awake()
            {
                base.Awake();
                IdleEnemy = new IdleEnemy(this,StateMachine,NameAnimation.IDLE,this);
                MoveEnemy = new MoveEnemy(this,StateMachine,NameAnimation.MOVE,gameObject,this);
                AttackEnemy = new AttackEnemy(this,StateMachine,NameAnimation.ATTACK,this);
                StateMachine.InitiateState(IdleEnemy);
            }

            protected  void Start()
            {
                _listEnemy.Add(this);
            }
            
            protected override void Update()
            {
                base.Update();
               isDeath = gameObject.activeInHierarchy;
            }
            private void OnDisable()
            {
                _listEnemy.Remove(this);
            }
            
            public Vector3 GetTarget()
            {
                var character = _listCharacter.GetClosest(transform.position);
                return character.transform.position;
            }
        
           
    }
}
