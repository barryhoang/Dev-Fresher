using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu(fileName = "scriptable_variable_GripMap.asset", menuName = "Soap/ScriptableVariables/GripMap")]
    public class GridMapVariable : ScriptableVariable<Unit[,]>
    {
        public Vector2Int size;
        private Unit _hero;
        
        public override void Init()
        {
            _value = new Unit[size.x,size.y];
            base.Init();
        }

        public bool CheckPosition(int x, int y)
        {
            if(x < 0 || x >= size.x)
                return false;
            if (y < 0 || y >= size.y)
                return false;
            return true;
        }
        public bool CheckWalkable(int xPos, int yPos)
        {
            return _value[xPos, yPos] == null;
        }
    }
}
