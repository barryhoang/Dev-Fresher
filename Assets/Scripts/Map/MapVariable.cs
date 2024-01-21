using Obvious.Soap;
using Units;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "scriptable_variable_map.Asset",menuName = "Data/Soap/ScriptableVariables/MapVariable")]
    public class MapVariable : ScriptableVariable<Unit[,]>
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
            return y >= 0 && y < size.y;
        }
        public bool CheckWalkable(int xPos, int yPos,Unit unit)
        {
            if (_value[xPos, yPos] == unit)
            {
                return true;
            }

            return _value[xPos, yPos] == null;
        }
    }
}
