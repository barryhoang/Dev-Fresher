using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_GripMap.asset", menuName = "Soap/ScriptableVariables/GripMap")]
    public class GridMapVariable : ScriptableVariable<int[,]>
    {
        public Vector2Int size;
        public override void Init()
        {
            _value = new int[size.x,size.y];
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
        
        internal bool CheckWalkable(int xPos, int yPos,Vector3 posStart)
        {
            if (_value[(int) posStart.x, (int) posStart.y] != 0)
            {
                return true;
            }
            return _value[xPos, yPos] == 0;
        }
    }
}
