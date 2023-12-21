using System.Collections.Generic;
using MEC;
using Unity.VisualScripting;
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
            isBackMoving = _character.HeathEntity.isDeath;
        }

        public override void Enter()
        {
            DoCheck();
            _character.SetWeaponAttack();
            Timing.RunCoroutine(AttackRate());
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isBackMoving)
            {
                _character.StateMachine.ChangeState(_character.MoveCharacter);
            }
        }

        private IEnumerator<float> AttackRate()
        {
            while (!isBackMoving)
            {
                entity._animatorController.SetDir(DirectionAttack(_character.HeathEntity.transform.position));
                entity._animatorController.SetAnimator(NameAnimation.ATTACK,true);
                _character.Weapon.RotateAttackEnemy();
                _character.HeathEntity.TakeDamage(1);
                yield return Timing.WaitForSeconds(1f);
            }
        }

        public override void Exit()
        {
            base.Exit();
            Timing.KillCoroutines(AttackRate());
            _character.SetWeaponAttack();
        }
    }
}
