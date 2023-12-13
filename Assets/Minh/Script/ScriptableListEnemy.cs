using System.Linq;
using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_list_Enemy.asset", menuName = "Soap/ScriptableLists/Enemy")]
    public class ScriptableListEnemy : ScriptableList<Enemy>
    {
        public Enemy GetClosest(Vector3 position)
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
