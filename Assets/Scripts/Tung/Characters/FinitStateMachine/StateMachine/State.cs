using UnityEngine;

namespace Tung
{
    public abstract class State 
    {
        protected Entity entity;
        protected StateMachine stateMachine;
        protected string animationName;

        public State(Entity entity, StateMachine stateMachine, string animationName)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            this.animationName = animationName;
        }
        
        
        public virtual void Enter()
        {
            DoCheck();
            // entity.Animator.SetBool(animationName,true);
            Debug.Log("animationName");
        }

        public virtual void Exit()
        {
            
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
