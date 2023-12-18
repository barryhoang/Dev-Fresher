using System.Linq;
using Enemy;
using Obvious.Soap.Example;
using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_list_Enemy_1.asset", menuName = "Soap/ScriptableLists/Enemy_1")]
    public class ScriptableListEnemy_1 : ScriptableList<Enemy_1>
    {
        public Enemy_1 GetClosest(Vector3 position)
        {
            if (IsEmpty)
            {
                return null;
            }

            var closet = _list.OrderBy(enemy1 => (position - enemy1.transform.position).sqrMagnitude).First();
            return closet;
        }

        public Enemy_1 GetEnemy1()
        {
            var closet = _list.OrderBy(enemy1 => (enemy1.dame)).First();
            return closet;
        }
    }
}
