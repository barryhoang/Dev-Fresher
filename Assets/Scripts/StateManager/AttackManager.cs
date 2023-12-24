using Entity;
using MEC;
using UnityEngine;

namespace StateManager
{
    public class AttackManager : MonoBehaviour
    {
        public static AttackManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public void AttackSystem(ScriptableListCharacter character,ScriptableListEnemy enemies)
        {
            foreach (var entity in character)
            {
                if (entity.isReadyAttack)
                {
                    if (!entity.isAttacking)
                    {
                        entity.isAttack = true;
                        Timing.RunCoroutine(entity.Attacking().CancelWith(entity.gameObject),"Attack");
                    }
                }
            }
            foreach (var entity in enemies)
            {
                if (entity.isReadyAttack)
                {
                    if (!entity.isAttacking)
                    {
                        entity.isAttack = true;
                        Timing.RunCoroutine(entity.Attacking().CancelWith(entity.gameObject),"Attack");
                    }
                }
            }
        }
    }
}
