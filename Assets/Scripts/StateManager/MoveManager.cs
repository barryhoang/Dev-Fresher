using System.Collections.Generic;
using Apathfinding;
using Entity;
using MEC;
using UnityEngine;

namespace StateManager
{
    public class MoveManager : MonoBehaviour
    {
        public static MoveManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }
        
        
        public void Move(ScriptableListCharacter character ,ScriptableListEnemy enemy)
        {
            foreach (var entity in character)
            {
                var position = entity.transform.position;
                var closest = enemy.GetClosest(position);
                if(!entity.isMoving)
                    for (int i = 0; i < closest.posAttacks.Count;i++)
                    {
                        if (!closest.posAttacks[i].isFull)
                        {
                            closest.posAttacks[i].isFull = true;
                            entity.isMove = true;
                            entity.isMoving = true;
                            Timing.RunCoroutine(entity.Move(closest, i).CancelWith(entity.gameObject));
                            break;
                        }
                    }
            }
            foreach (var entity in enemy)
            {
                var position = entity.transform.position;
                var closest = character.GetClosest(position);
                if(!entity.isMoving)
                    for (int i = 0; i < closest.posAttacks.Count;i++)
                    {
                        if (!closest.posAttacks[i].isFull)
                        {
                            closest.posAttacks[i].isFull = true;
                            entity.isMove = true;
                            entity.isMoving = true;
                            Timing.RunCoroutine(entity.Move(closest, i).CancelWith(entity.gameObject));
                            break;
                        }
                    }
            }   
        }
    }
}
