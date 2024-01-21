using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_YourType.asset", menuName = "Soap/ScriptableVariables/YourType")]
    public class FightingGridVariable : ScriptableVariable<Unit[,]>
    {
        public Vector2Int size;
        private FightingGridVariable _fightingGrid;

        public override void Init()
        {
            _value = new Unit[size.x, size.y];
            base.Init();
        }

        /*internal bool CheckWalkable(int xPos, int yPos)
        {
            return _fightingGrid.Value[xPos, yPos] == null;
        }

        internal bool CheckPosition(int xPos, int yPos)
        {
            return _fightingGrid.Value[xPos, yPos] == null;
        }*/
    }
}