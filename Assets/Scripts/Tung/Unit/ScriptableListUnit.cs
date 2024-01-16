using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_list_Unit.asset", menuName = "Soap/ScriptableLists/Unit")]
    public class ScriptableListUnit : ScriptableList<Unit>
    {
        public Unit GetClosest(Vector3 position,GridMapVariable _gridMap)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(character => (position - character.transform.position).sqrMagnitude).First();
            return closest;
        }

        private Vector2Int GetClosestInDirection(Vector3 position, Vector3 direction)
        {
            var closestUnit = _list.OrderBy(character => Vector3.Dot(direction, character.transform.position - position)).First();
            return new Vector2Int((int)closestUnit.transform.position.x, (int)closestUnit.transform.position.y);
        }
    }
}
