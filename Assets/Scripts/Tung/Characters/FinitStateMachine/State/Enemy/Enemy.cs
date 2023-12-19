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

            public void FindAttack()
            {
                var enemy = _listCharacter.GetClosest(transform.position);
                _characterWork = enemy;
            }
            public Vector3 FindTarget()
            {
                for(int i = 0;  i < _characterWork.isFull.Count;i++)
                {
                    if (!_characterWork.isFull[i])
                    {
                        _indexWork = i;
                        return _characterWork.posAttack[i].position;
                    }
                }
                return Vector3.zero;
            }
        
        
            public bool FinishMove()
            {
                if (CheckMoveTarget(_characterWork.posAttack[_indexWork].position))
                {
                    _characterWork.isFull[_indexWork] = true;
                    return false;
                }
                return true;
            }

            protected void Start()
            {
                _listEnemy.Add(this);    
            }

            private void OnDisable()
            {
                _listEnemy.Remove(this);
            }
    }
}
