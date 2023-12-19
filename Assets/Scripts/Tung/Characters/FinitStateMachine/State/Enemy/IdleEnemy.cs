using UnityEngine;

namespace Tung
{
    public class IdleEnemy : IdleState
    {
        private Enemy _enemy;
        public IdleEnemy(Entity entity, StateMachine stateMachine, NameAnimation animationName,Enemy enemy) : base(entity, stateMachine, animationName)
        {
            this._enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsIlde)
            {
                _enemy.StateMachine.ChangeState(_enemy.MoveEnemy);
            }
        }
    }
}
