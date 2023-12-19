using UnityEngine;

namespace Tung
{
    public class WeaponAttackCharacter : AttackWeaponState
    {
        private Character _character;
        public WeaponAttackCharacter(Entity entity, StateMachine stateMachine, NameAnimation animationName,Character character) : base(entity, stateMachine, animationName)
        {
            this._character = character;
        }

        public override void Enter()
        {
            base.Enter();
            _character.SetWeaponAttack();
        }

        public override void Exit()
        {
            base.Exit();
            _character.SetWeaponAttack();
        }
    }
}
