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
                    foreach (var pos in closest.posAttacks)
                    {
                        if (!pos.isFull)
                        {
                            pos.isFull = true;
                            entity.isMove = true;
                            entity.isMoving = true;
                            Timing.RunCoroutine(entity.Move(closest, pos.posAttack.position).CancelWith(entity.gameObject));
                            break;
                        }
                    }
            }
            // foreach (var entity in enemy)
            // {
            //     var position = entity.transform.position;
            //     var closest = character.GetClosest(entity.transform.position);
            //     int currentX = (int) position.x;
            //     int currentY = (int) position.y;
            //     var position1 = closest.transform.position;
            //     int targetX = (int)  position1.x;
            //     int targetY = (int)  position1.y;
            //     List<PathNode> pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX,targetY, position);
            //     entity.Move(closest,pathNodes);
            // }   
        }
    }
}
