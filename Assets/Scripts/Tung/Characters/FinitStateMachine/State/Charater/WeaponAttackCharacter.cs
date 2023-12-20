using UnityEngine;

namespace Tung
{
    public class WeaponAttackCharacter : AttackWeaponState
    {
        private Character _character;
        private bool isBackMoving;
        public WeaponAttackCharacter(Entity entity, StateMachine stateMachine, NameAnimation animationName,Character character) : base(entity, stateMachine, animationName)
        {
            _character = character;
        }

        public override void DoCheck()
        {
            base.DoCheck();
        }

        public override void Enter()
        {
            // var position =  - 
            // // entity.ShouldFlip(position);
            _character.SetWeaponAttack();
            entity._animatorController.SetDir(DirectionAttack(_character.HeathEntity.transform.position));
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
        }
        public override void Exit()
        {
            base.Exit();
            _character.SetWeaponAttack();
        }
    }
}
