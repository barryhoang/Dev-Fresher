using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_list_Enemy.asset", menuName = "Soap/ScriptableLists/Enemy")]
    public class ScriptableListEnemy : ScriptableList<Enemy>
    {
        public List<Enemy> GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).ToList();
            foreach (var temp in closest)
            {
               
            } 
            return closest;
        }
        public Enemy PosEnemies(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
