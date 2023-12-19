using UnityEngine;
using Minh;
using System.Linq;
namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_list_Player.asset", menuName = "Soap/ScriptableLists/MinhPlayer")]
    public class ScriptableListPlayer : ScriptableList<Player>
    {
        public Player GetClosest(Vector3 position)
        {
            if (IsEmpty)
            {
                return null;
            }

            var closest = _list.OrderBy(player => (position - player.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
