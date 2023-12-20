using System;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class Enemy : Entity
    {
            [SerializeField] private ScriptableListEnemy _listEnemy;
            [SerializeField] private ScriptableListCharacter _listCharacter;
            private Character _characterWork;
           
            public int indexWork;
            public Character CharacterWork => _characterWork;
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

            private void OnDisable()
            {
                _listEnemy.Remove(this);
            }
            

            public void FindAttack()
            {
                var enemy = _listCharacter.GetClosest(transform.position);
                _characterWork = enemy;
            }
            public void GetTarget()
            {
                var enemy = _listCharacter.GetClosest(transform.position);
                _characterWork = enemy;
                for (int i = 0; i < _characterWork.isFull.Count; i++)
                {
                    if (!_characterWork.isFull[i])
                    {
                        indexWork = i;
                        _characterWork.isFull[i] = true;
                        return;
                    }
                }
            }
        
           
    }
}
