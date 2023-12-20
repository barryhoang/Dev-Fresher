using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveEnemy : MoveState
    {
        private Enemy _enemy;
        private bool _isAttack;
        
        public MoveEnemy(Entity entity, StateMachine stateMachine, NameAnimation animationName, GameObject gameObject,Enemy enemy) : base(entity, stateMachine, animationName, gameObject)
        {
            _enemy = enemy;
        }
    
        public override void Enter()
        {
            base.Enter();
            Timing.RunCoroutine(Move( _enemy.GetTarget(),Vector3.zero),"Move");
        }

        public override void DoCheck()
        {
            base.DoCheck();
            IsMove = _enemy.CheckRangeAttack();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _enemy.StateMachine.ChangeState(_enemy.AttackEnemy);
            }
        }
        
        private IEnumerator<float> Move(Vector3 target,Vector3 posFlip)
        {
            while (IsMove)
            {
                target = _enemy.GetTarget();
                Move(target);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
