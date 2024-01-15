using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_YourType.asset", menuName = "Soap/ScriptableVariables/YourType")]
    public class FightingGridVariable : ScriptableVariable<Unit[,]>
    {
        public Vector2Int size;

        public override void Init()
        {
            _value = new Unit[size.x,size.y];
            base.Init();
        }
    }
}
