using System.Runtime.CompilerServices;
using UnityEngine;

namespace Tung
{
    public class AttackWeaponState : AttackState
    {
        protected AttackWeaponState(Entity entity, StateMachine stateMachine, NameAnimation animationName) : base(entity, stateMachine, animationName)
        {
        }
    }
}
