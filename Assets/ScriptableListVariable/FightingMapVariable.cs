using UnityEngine;
using Minh;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_FightingMapVariable.asset", menuName = "Soap/ScriptableVariables/FightingMapVariable")]
    public class FightingMapVariable : ScriptableVariable<Hero[,]>
    {
        public Vector2Int size;
        public override void Init()
        {
            _value=new Hero[size.x,size.y];
            base.Init();
        }
    }
}
