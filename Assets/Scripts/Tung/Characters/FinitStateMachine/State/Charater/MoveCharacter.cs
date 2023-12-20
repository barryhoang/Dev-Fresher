using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class MoveCharacter : MoveState
    {
        private Character _character;
       
        public MoveCharacter(Entity entity, StateMachine stateMachine, NameAnimation animationName, GameObject gameObject,Character character) : base(entity, stateMachine, animationName, gameObject)
        {
            _character = character;
        }
        public override void DoCheck()
        {
            base.DoCheck();
            IsMove = FinishMove(_character.EnemyWork.posAttack[_character.indexWork].position);
        }
        
        public override void Enter()
        {
            if (_character.EnemyWork == null)
            {
                _character.GetTarget();
            }
            base.Enter();
            Timing.RunCoroutine(Move(_character.EnemyWork.posAttack[_character.indexWork].position,Vector3.zero),"Move");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _character.StateMachine.ChangeState(_character.WeaponAttack);
            }
        }
        protected IEnumerator<float> Move(Vector3 target,Vector3 posFlip)
        {
            while (IsMove)
            {
                target = _character.EnemyWork.posAttack[_character.indexWork].position;
                Move(target);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}