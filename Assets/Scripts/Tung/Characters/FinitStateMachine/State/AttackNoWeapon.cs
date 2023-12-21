
using System.Collections;
using System.Collections.Generic;
using MEC;

namespace Tung
{
    public class AttackNoWeapon : AttackState
    {
        private Enemy _enemy;

        protected AttackNoWeapon(Entity entity, StateMachine stateMachine, NameAnimation animationName,Enemy enemy) : base(entity, stateMachine, animationName)
        {
            _enemy = enemy;
        }
    }
}
