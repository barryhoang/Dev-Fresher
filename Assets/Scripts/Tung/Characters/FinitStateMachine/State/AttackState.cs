using UnityEngine;

namespace Tung
{
    public class AttackState : State
    {
        protected AttackState(Entity entity, StateMachine stateMachine, NameAnimation animationName) : base(entity, stateMachine, animationName)
        {
            
        }
    }
}
