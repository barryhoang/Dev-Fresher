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
            base.Enter();
            Timing.RunCoroutine(Move(_enemy.CharacterWork.transform),"Move");
        }

        public override void DoCheck()
        {
            base.DoCheck();
            IsMove = _enemy.FindCharacter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _enemy.StateMachine.ChangeState(_enemy.AttackEnemy);
            }
        }
    }
}
