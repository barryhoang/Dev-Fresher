using System.Collections;
using System.Collections.Generic;
using MEC;
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
            IsMove = _character.FinishMove();
        }

        public override void Enter()
        {
            _character.FindAttack();
            posTager =  FindTarget();
            base.Enter();
            Timing.RunCoroutine(Move(posTager,_character.EnemyWork.transform.position),"Move");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _character.StateMachine.ChangeState(_character.WeaponAttack);
            }
        }

        protected override Vector3 FindTarget()
        {
            return _character.FindTarget();
        }
    }
}