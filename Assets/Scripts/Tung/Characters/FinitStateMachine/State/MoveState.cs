using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveState : State
    {
        private Character _character;
        private bool isMove;
        private GameObject _gameObject;
        public MoveState(Entity entity, StateMachine stateMachine, string animationName,GameObject gameObject) : base(entity, stateMachine, animationName)
        {
            this._gameObject = gameObject;
        }

        public override void Enter()
        {
            base.Enter();
            Timing.RunCoroutine(Move().CancelWith(_gameObject));
        }

        public override void DoCheck()
        {
            base.DoCheck();
            isMove = entity.CheckMove();
        }

        private IEnumerator<float> Move()
        {
            while (isMove)
            {
                entity.Move();
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}