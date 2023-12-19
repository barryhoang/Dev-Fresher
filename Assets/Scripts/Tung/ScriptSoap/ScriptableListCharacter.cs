using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_list_Character.asset", menuName = "Soap/ScriptableLists/Character")]
    public class ScriptableListCharacter : ScriptableList<Character>
    {
        public Character GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(character  => (position - character.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
