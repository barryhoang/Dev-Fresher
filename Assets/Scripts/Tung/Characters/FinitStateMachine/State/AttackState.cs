using UnityEngine;

namespace Tung
{
    public class AttackState : State
    {
        public AttackState(Entity entity, StateMachine stateMachine, string animationName) : base(entity, stateMachine, animationName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            entity.SetWeaponAttack();
        }
        
        public override void Exit()
        {
            base.Exit();
            entity.SetWeaponAttack();
        }
    }
}
