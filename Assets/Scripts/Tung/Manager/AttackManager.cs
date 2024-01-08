using System.Collections.Generic;
using Entity;
using MEC;
using UnityEngine;

namespace Tung
{
    public class AttackManager : MonoBehaviour
    {
        public static AttackManager instance { get; private set; }
        [SerializeField] private ScriptableListCharacter _listSoapCharacter;
        [SerializeField] private ScriptableListEnemy _listSoapEnemy;
        private void Awake()
        {
            instance = this;
        }

        public void AttackSystem()
        {
            foreach (var entity in _listSoapCharacter)
            {
                if (entity.isReadyAttack)
                {
                    if (!entity.isAttacking)
                    {
                        entity.isAttack = true;
                        Timing.RunCoroutine(entity.Attacking().CancelWith(entity.gameObject), "Attack");
                    }
                }
            }
            foreach (var entity in _listSoapEnemy)
            {
                if (entity.isReadyAttack)
                {
                    if (!entity.isAttacking)
                    {
                        entity.isAttack = true;
                        Timing.RunCoroutine(entity.Attacking().CancelWith(entity.gameObject), "Attack");
                    }
                }
            }
        }
    }
}
