using System.Linq;
using UnityEngine;
using Minh;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_list_Player.asset", menuName = "Soap/ScriptableLists/Player")]
    public class ScriptableListPlayer : ScriptableList<Player>
    {
        public Player GetClosest(Vector3 position)
        {
            if (IsEmpty)
            {
                return null;
            }

            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
