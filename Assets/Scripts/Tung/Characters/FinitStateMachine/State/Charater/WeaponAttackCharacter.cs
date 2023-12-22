using System.Collections.Generic;
using MEC;
using Unity.VisualScripting;
using UnityEngine;

namespace Tung
{
    public class WeaponAttackCharacter : AttackWeaponState
    {
        private Character _character;
        private bool isAttack;
        public WeaponAttackCharacter(Entity entity, StateMachine stateMachine, NameAnimation animationName,Character character) : base(entity, stateMachine, animationName)
        {
            _character = character;
        }

        public override void DoCheck()
        {
            base.DoCheck();
            isAttack = entity.CheckRangeAttack();
        }

        public override void Enter()
        {
            DoCheck();
            _character.SetWeaponAttack();
            Timing.RunCoroutine(AttackRate().CancelWith(_character.gameObject),Segment.Update);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isAttack)
            {
                _character.StateMachine.ChangeState(_character.MoveCharacter);
            }
        }

        private IEnumerator<float> AttackRate()
        {
            while (!isAttack)
            {
                if (isAttack)
                {
                    _character.StateMachine.ChangeState(_character.MoveCharacter);
                }
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
