
using System.Collections;
using System.Collections.Generic;
using MEC;

namespace Tung
{
    public class AttackNoWeapon : AttackState
    {
        private Enemy _enemy;

        protected AttackNoWeapon(Entity entity, StateMachine stateMachine, NameAnimation animationName,Enemy enemy) : base(entity, stateMachine, animationName)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            entity._animatorController.SetDir(DirectionAttack(_enemy.CharacterWork.transform.position)*-1);
            DoCheck();
            Timing.RunCoroutine(RateAttack(), "Attack");
        }

        private IEnumerator<float> RateAttack()
        {
            while (true)
            {
                entity._animatorController.SetAnimator(NameAnimation.ATTACK,true);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
