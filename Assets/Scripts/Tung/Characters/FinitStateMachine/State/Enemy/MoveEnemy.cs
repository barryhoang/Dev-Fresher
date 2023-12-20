using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveEnemy : MoveState
    {
        private Enemy _enemy;
        
        public MoveEnemy(Entity entity, StateMachine stateMachine, NameAnimation animationName, GameObject gameObject,Enemy enemy) : base(entity, stateMachine, animationName, gameObject)
        {
            _enemy = enemy;
        }
    
        public override void Enter()
        {
            _enemy.GetTarget();
            base.Enter();
            Timing.RunCoroutine(Move( _enemy.CharacterWork.posAttack[_enemy.indexWork].position,Vector3.zero),"Move");
        }

        public override void DoCheck()
        {
            base.DoCheck();
            IsMove = FinishMove(_enemy.CharacterWork.posAttack[_enemy.indexWork].position);
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _enemy.StateMachine.ChangeState(_enemy.AttackEnemy);
            }
        }
        protected IEnumerator<float> Move(Vector3 target,Vector3 posFlip)
        {
            while (IsMove)
            {
                target = _enemy.CharacterWork.posAttack[_enemy.indexWork].position;
                Move(target);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
