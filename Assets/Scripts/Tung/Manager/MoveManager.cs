using System;
using System.Security.Cryptography;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class MoveManager : MonoBehaviour
    {
        public static MoveManager instance { get; private set; }

        [SerializeField] private ScriptableListCharacter _listSoapCharacter;
        [SerializeField] private ScriptableListEnemy _listSoapEnemy;

        private void Awake()
        {
            instance = this;
        }

        public void Move()
        {
            foreach (var entity in _listSoapCharacter)
            {
                var closest = _listSoapEnemy.GetClosest(entity.transform.position);
                if(entity.isAttacking) continue;
                if (!entity.isMoving)
                {
                    entity.isMoving = true;
                    entity.entityTarget = closest;
                    Timing.RunCoroutine(entity.Move());
                }
                else
                {
                    entity.entityTarget = closest;
                }
            }
            foreach (var entity in _listSoapEnemy)
            {
                var closest = _listSoapCharacter.GetClosest(entity.transform.position);
                if(entity.isAttacking) continue;
                if (!entity.isMoving)
                {
                    entity.isMoving = true;
                    entity.entityTarget = closest;
                    Timing.RunCoroutine(entity.Move());
                }
                else
                {
                    entity.entityTarget = closest;
                }
            }
        }
    }
}
