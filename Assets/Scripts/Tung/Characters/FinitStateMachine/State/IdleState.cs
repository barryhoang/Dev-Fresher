using UnityEngine;

namespace Tung
{
    public class IdleState : State
    {
        protected bool IsIlde;

        protected IdleState(Entity entity, StateMachine stateMachine, NameAnimation animationName) : base(entity, stateMachine, animationName)
        {
        }


        public override void Enter()
        {
            base.Enter();
            IsIlde = entity.IsIdle;
        }

        public override void DoCheck()
        {
            base.DoCheck();
            IsIlde = entity.IsIdle;
        }
    }
}