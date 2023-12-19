using UnityEngine;

namespace Tung
{
    public  class State 
    {
        protected Entity entity;
        protected StateMachine stateMachine;
        protected NameAnimation animationName;

        public State(Entity entity, StateMachine stateMachine, NameAnimation animationName)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            this.animationName = animationName;
        }
        
        
        public virtual void Enter()
        {
            DoCheck();
            entity._animatorController.SetAnimator(animationName,true);
        }

        public virtual void Exit()
        {
            entity._animatorController.SetAnimator(animationName,false);
        }

        public virtual void LogicUpdate()
        {
            DoCheck();
        }

        public virtual void PhysicalUpdate()
        {
            DoCheck();
        }

        public virtual void DoCheck()
        {
            
        }
    }
}
