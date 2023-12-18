using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace TungTran.Enemy
{
    [CreateAssetMenu(fileName = "scriptable_list_Enemy_1.asset", menuName = "Soap/ScriptableLists/Enemy_1")]
    public class ScriptableListEnemyOne : ScriptableList<EnemyOne>
    {
        public EnemyOne GetClosest(Vector3 position)
        {
            if (IsEmpty)
            {
                return null;
            }

            var closet = _list.OrderBy(enemy1 => (position - enemy1.transform.position).sqrMagnitude).First();
            return closet;
        }

        public EnemyOne GetEnemy1()
        {
            var closet = _list.OrderBy(enemy1 => (enemy1.damage)).First();
            return closet;
        }
    }
}
