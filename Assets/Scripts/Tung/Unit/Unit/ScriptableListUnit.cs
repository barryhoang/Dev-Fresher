using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_list_Unit.asset", menuName = "Soap/ScriptableLists/Unit")]
    public class ScriptableListUnit : ScriptableList<Unit>
    {
        public Unit GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(character => (position - character.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
