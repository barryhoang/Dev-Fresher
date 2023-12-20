using UnityEngine;

namespace Tung
{
    public class AttackEnemy : AttackNoWeapon
    {
        private Enemy _enemy;
        
        public AttackEnemy(Entity entity, StateMachine stateMachine, NameAnimation animationName, Enemy enemy) : base(entity, stateMachine, animationName, enemy)
        {
            _enemy = enemy;
        }
    }
}
