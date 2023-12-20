using UnityEngine;

namespace Tung
{
    public class AttackState : State
    {
        protected bool isBoolMoveBack;
        protected AttackState(Entity entity, StateMachine stateMachine, NameAnimation animationName) : base(entity, stateMachine, animationName)
        {
            
        }

        protected Vector3 DirectionAttack(Vector3 target)
        {
            var dir = target - entity.transform.position;
            dir.Normalize();
            return dir;
        }
        
    }
}
