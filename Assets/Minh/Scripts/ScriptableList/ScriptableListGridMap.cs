using UnityEngine;
using Minh;
namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_list_GridMap.asset", menuName = "Soap/ScriptableLists/GridMap")]
    public class ScriptableListGridMap : ScriptableList<bool[,]>
    {
        public bool[,] _arrayPosition=new bool[18,10];
        
        
    }
}
