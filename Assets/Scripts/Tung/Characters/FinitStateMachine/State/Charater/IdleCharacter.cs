using UnityEngine;

namespace Tung
{
    public class IdleCharacter : IdleState
    {
        private Character _character;
        
        public IdleCharacter(Entity entity, StateMachine stateMachine, NameAnimation animationName,Character character) : base(entity, stateMachine, animationName)
        {
            _character = character;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsIlde)
            {
                _character.StateMachine.ChangeState(_character.MoveCharacter);
            }
        }
    }
}
