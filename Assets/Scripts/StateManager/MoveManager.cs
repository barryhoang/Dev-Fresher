using System.Collections.Generic;
using Apathfinding;
using Entity;
using MEC;
using Obvious.Soap;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace StateManager
{
    public class MoveManager : MonoBehaviour
    {
        public static MoveManager instance { get; private set; }

        [SerializeField] private GridMapVariable _gridMap;

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
                Vector3Int temp = new Vector3Int(Mathf.RoundToInt(closest.transform.position.x),
                    Mathf.RoundToInt(closest.transform.position.y),0);
                if(temp == DirTarget(temp))
                {
                    closest = enemy.GetSecondClosest(transform.position);
                    temp = new Vector3Int(Mathf.RoundToInt(closest.transform.position.x),
                        Mathf.RoundToInt(closest.transform.position.y),0);
                    Debug.Log("A");
                }
                entity.Move(closest, DirTarget(temp));
            }

            foreach (var entity in enemy)
            {
                var position = entity.transform.position;
                var closest = character.GetClosest(position);
                Vector3Int temp = new Vector3Int((int)closest.transform.position.x,
                    (int) closest.transform.position.y,0);
                entity.Move(closest, DirTarget(temp));
            }
        }

        private Vector3Int DirTarget(Vector3Int temp)
        {
            float distanceMin = 1000f;
            int x = 0;
            int y = 0;
            if (!_gridMap.Value[temp.x - 1, temp.y])
            {
                x = -1;
                distanceMin = Vector2.Distance(transform.position, new Vector2(temp.x - 1, temp.y));
            }
            if (!_gridMap.Value[temp.x + 1, temp.y])
            {
                float distanceRight = Vector2.Distance(transform.position, new Vector2(temp.x + 1, temp.y));
                if (distanceRight < distanceMin)
                {
                    x = 1;
                    y = 0;
                    distanceMin = distanceRight;
                }
            }
            if (!_gridMap.Value[temp.x, temp.y + 1])
            {
                float distanceUp = Vector2.Distance(transform.position, new Vector2(temp.x, temp.y + 1));
                if (distanceUp < distanceMin)
                {
                    x = 0;
                    y = 1;
                    distanceMin = distanceUp;
                }
            }
            if (!_gridMap.Value[temp.x, temp.y - 1])
            {
                float distanceDown = Vector2.Distance(transform.position, new Vector2(temp.x, temp.y - 1));
                if (distanceDown < distanceMin)
                {
                    x = 0;
                    y = -1;
                    distanceMin = distanceDown;
                }
            }
            return new Vector3Int(temp.x + x,temp.y + y);
        }
    }
}
