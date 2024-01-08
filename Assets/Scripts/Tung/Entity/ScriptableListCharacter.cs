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
            var closest = _list.OrderBy(character => (position - character.transform.position).sqrMagnitude).First();
            return closest;
        }
        public Character GetCharacter(Vector3 position)
        {
            foreach (var entity in this)
            {
                if ((int)position.x == (int)entity.transform.position.x)
                {
                    return entity;
                }
            }
            var closest = _list.OrderBy(Enemy => (position - Enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
