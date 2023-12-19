using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveState : State
    {
        protected bool IsMove;
        
        protected Vector3 posTager;

        protected MoveState(Entity entity, StateMachine stateMachine, NameAnimation animationName,GameObject gameObject) : base(entity, stateMachine, animationName)
        {
        }
        
        
        
        protected IEnumerator<float> Move(Vector3 target,Vector3 posFlip)
        {
            while (IsMove)
            {
                target = FindTarget();
                entity.Move(target);
                entity.Flip(posFlip);
                yield return Timing.WaitForOneFrame;
            }
        }

        protected virtual Vector3 FindTarget()
        {
            return Vector3.zero;
        }
    }
}