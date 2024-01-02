using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "scriptable_list_Enemy.asset", menuName = "Soap/ScriptableLists/Enemy")]
    public class ScriptableListEnemy : ScriptableList<Enemy>
    {
        public Enemy GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
        public Enemy GetSecondClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;

            var secondClosest = _list
                .OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude)
                .Skip(1)
                .FirstOrDefault();

            return secondClosest;
        }
    }
}
