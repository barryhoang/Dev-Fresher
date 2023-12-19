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
            this._character = character;
        }

        public override void DoCheck()
        {
            base.DoCheck();
            IsMove = _character.FindEnemy();
        }

        public override void Enter()
        {
            base.Enter();
            Timing.RunCoroutine(Move(_character.EnemyWork.transform),"Move");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!IsMove)
            {
                _character.StateMachine.ChangeState(_character.WeaponAttack);
            }
        }
    }
}