using System.Linq;
using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_list_Hero.asset", menuName = "Soap/ScriptableLists/Hero")]
    public class ScriptableListHero : ScriptableList<Hero>
    {
        public Hero GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
