﻿using System.Collections;
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
            IsMove = _character.CheckRangeAttack();
        }
        
        public override void Enter()
        {
            _character.GetTarget();
            base.Enter();
            Timing.RunCoroutine(Move(_character.GetTarget(),Vector3.zero),Segment.Update,"Move");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Move(_character.GetTarget());
            if (!IsMove)
            {
                _character.StateMachine.ChangeState(_character.WeaponAttack);
            }
        }
        public override void Exit()
        {
            base.Exit();
            Timing.KillCoroutines(Move(_character.GetTarget(), Vector3.zero));
        }

        private IEnumerator<float> Move(Vector3 target,Vector3 posFlip)
        {
            while (IsMove)
            {
                target = _character.GetTarget();
                Move(target);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}