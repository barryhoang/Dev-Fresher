using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveEnemy : MoveState
    {
        private Enemy _enemy;
        
        public MoveEnemy(Entity entity, StateMachine stateMachine, NameAnimation animationName, GameObject gameObject,Enemy enemy) : base(entity, stateMachine, animationName, gameObject)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            _enemy.FindAttack();
            posTager =  FindTarget();
            base.Enter();
            Timing.RunCoroutine(Move(posTager,_enemy.CharacterWork.transform.position),"Move");
        }

        public override void DoCheck()
        {
            base.DoCheck();
            IsMove = _enemy.FinishMove();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _enemy.StateMachine.ChangeState(_enemy.AttackEnemy);
            }
        }
        protected override Vector3 FindTarget()
        {
            return _enemy.FindTarget();
        }
    }
}
