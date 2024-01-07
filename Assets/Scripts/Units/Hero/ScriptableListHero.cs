using System.Linq;
using Obvious.Soap;
using UnityEngine;

namespace Units.Hero
{
    [CreateAssetMenu(fileName = "scriptable_list_Hero.asset", menuName = "Soap/Lists/Hero")]
    public class ScriptableListHero : ScriptableList<global::Hero>
    { 
        public global::Hero GetClosest(Vector3 position)
        {
            if (IsEmpty)
                return null;
            var closest = _list.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
            return closest;
        }
    }
}
