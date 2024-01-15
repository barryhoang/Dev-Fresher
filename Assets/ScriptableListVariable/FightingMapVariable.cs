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
        public bool CheckPosition(int x, int y)
        {
            if (x < 0||x>=size.x)
            {
                return false;
            }

            if (y < 0 || y >= size.y)
            {
                return false;
            }
            return true;
        }

        internal bool CheckWalkable(int xPos, int yPos)
        {
            return _value[xPos, yPos]==null;
        }
    }
}
