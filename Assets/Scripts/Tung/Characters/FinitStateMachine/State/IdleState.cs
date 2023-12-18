using UnityEngine;

namespace Tung
{
    public class IdleState : State
    {
        private Character _character;
        
        public IdleState(Entity entity, StateMachine stateMachine, string animationName,Character character) : base(entity, stateMachine, animationName)
        {
            this._character = character;
        }


        public override void Enter()
        {
            base.Enter();
            entity.SetIdleState();
        }

        public override void DoCheck()
        {
            base.DoCheck();
            
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
        }

        public override void Exit()
        {
            base.Exit();
            entity.SetIdleState();
        }
    }
}