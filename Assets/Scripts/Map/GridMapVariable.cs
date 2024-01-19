using Obvious.Soap;
using Unit;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "scriptable_variable_GridMapVariable.asset", menuName = "Soap/ScriptableVariables/GridMapVariable")]
    public class GridMapVariable : ScriptableVariable<BaseUnit[,]>
    {
        public Vector2Int size;

        public override void Init()
        {
            _value = new BaseUnit[size.x, size.y];
            base.Init();
        }
    }
}