using System.Collections.Generic;
using Apathfinding;
using Entity;
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
                if (closest == null) return;
                int currentX = (int) position.x;
                int currentY = (int) position.y;
                int targetX = 0;
                int targetY = 0;
                int index = 0;
                if (!entity.isMove)
                {
                    for (int i = 0; i < closest.isFull.Count; i++)
                    {
                        if (!closest.isFull[i])
                        { 
                            targetX = (int) closest.posAttack[i].transform.position.x;
                            targetY = (int) closest.posAttack[i].transform.position.y;
                            index = i;
                            break;
                        }
                    }
                }
                List<PathNode> pathNodes = GridManager.instance._pathfinding.FindPath(currentX, currentY, targetX,   targetY, position);
                if (pathNodes.Count > 0)
                {
                    entity.Move(closest,pathNodes[0],index);
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
