using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_list_Unit.asset", menuName = "Soap/ScriptableLists/Unit")]
    public class ScriptableListUnit : ScriptableList<Unit>
    {
        public void UpdateCellLocation(Unit unit,Vector2Int pos)
        {
            for (int i = 0; i < Count; i++)
            {
                if (unit == this[i])
                {
                    unit.transform.position = new Vector3(pos.x,pos.y);
                    return;
                }
            }
        }
    }
}
