using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class AttackEnemy : AttackNoWeapon
    {
        private Enemy _enemy;
        private bool isMoveBack;
        public AttackEnemy(Entity entity, StateMachine stateMachine, NameAnimation animationName, Enemy enemy) : base(entity, stateMachine, animationName, enemy)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            entity._animatorController.SetDir(DirectionAttack(_enemy.HeathEntity.transform.position)*-1);
            DoCheck();
            Timing.RunCoroutine(RateAttack().CancelWith(_enemy.gameObject),Segment.LateUpdate, "Attack");
        }

        public override void Exit()
        {
            base.Exit();
                Timing.KillCoroutines(RateAttack());
        }

        private IEnumerator<float> RateAttack()
        {
            while (true)
            {
                entity._animatorController.SetAnimator(NameAnimation.ATTACK,true);
                // _enemy.HeathEntity.TakeDamage(1);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
