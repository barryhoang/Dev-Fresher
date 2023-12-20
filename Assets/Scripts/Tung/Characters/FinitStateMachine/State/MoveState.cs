using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Tung
{
    public class MoveState : State
    {
        protected bool IsMove;
        protected Vector3 posTarget;

        protected MoveState(Entity entity, StateMachine stateMachine, NameAnimation animationName,GameObject gameObject) : base(entity, stateMachine, animationName)
        {
        }

       
        protected bool FinishMove(Vector3 target)
        {
            var distance = Vector2.Distance(entity.transform.position, target);
            return distance > 1f;
        }

        protected void Move(Vector3 target)
        {
            var position = entity.transform.position;
            var dir = target - position;
            dir.Normalize();
            position += dir * (entity.moveSpeed * Time.deltaTime);
            entity.transform.position = position;
        }

       
    }
}