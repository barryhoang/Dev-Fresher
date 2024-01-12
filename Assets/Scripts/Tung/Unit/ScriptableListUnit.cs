using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_list_Unit.asset", menuName = "Soap/ScriptableLists/Unit")]
    public class ScriptableListUnit : ScriptableList<Unit>
    {
        public Vector2Int GetClosest(Vector3 position,GridMapVariable _gridMap)
        {
            if (IsEmpty)
                return Vector2Int.zero;
            var closest = _list.OrderBy(character => (position - character.transform.position).sqrMagnitude).First();

            Vector2Int temp = closest.transform.position.ToV2Int();
            Vector2Int start = position.ToV2Int();
            Vector2Int value = Vector2Int.zero;
            float minDistance = float.MaxValue;
            Vector2Int[] listDir = {temp + Vector2Int.down,temp + Vector2Int.left,temp+ Vector2Int.right ,temp + Vector2Int.up};
            
            foreach (var dir in listDir)
            {
                float distance = Vector2Int.Distance(start,dir);
                
                if (distance < minDistance && _gridMap.Value[dir.x, dir.y] == null)
                {
                    minDistance = distance;
                    value = dir;
                }
            }
            return value;
        }

        private Vector2Int GetClosestInDirection(Vector3 position, Vector3 direction)
        {
            var closestUnit = _list.OrderBy(character => Vector3.Dot(direction, character.transform.position - position)).First();
            return new Vector2Int((int)closestUnit.transform.position.x, (int)closestUnit.transform.position.y);
        }
    }
}
