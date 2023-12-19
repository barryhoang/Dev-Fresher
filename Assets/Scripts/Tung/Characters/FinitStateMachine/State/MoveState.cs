using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveState : State
    {
        private Character _character;
        private GameObject _gameObject;

        protected bool IsMove;

        protected MoveState(Entity entity, StateMachine stateMachine, NameAnimation animationName,GameObject gameObject) : base(entity, stateMachine, animationName)
        {
            _gameObject = gameObject;
        }

        protected IEnumerator<float> Move(Transform target)
        {
            while (IsMove)
            {
                entity.Move(target);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}