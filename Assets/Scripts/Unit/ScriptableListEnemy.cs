using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "scriptable_list_Enemy.asset", menuName = "Soap/Lists/Enemy")]
    public class ScriptableListEnemy : ScriptableList<global::Unit.Enemy>
    {
        public global::Unit.Enemy GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            
            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}