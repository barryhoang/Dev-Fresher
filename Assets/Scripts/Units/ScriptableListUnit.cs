using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "scriptable_list_Unit.asset", menuName = "Data/Soap/ScriptableLists/Unit")]

    public class ScriptableListUnit : ScriptableList<Unit>
    {
        public Unit GetClosestUnit(Vector3 pos)
        {
            if (IsEmpty) return null;
            var closest = _list.OrderBy(character 
                => (pos - character.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
