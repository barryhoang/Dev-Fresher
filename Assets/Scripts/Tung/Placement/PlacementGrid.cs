using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class PlacementGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit unit;
        [SerializeField] private ScriptableEventVector2 _eventDown;
        [SerializeField] private ScriptableEventVector2 _eventUp;
        [SerializeField] private GridMapVariable _gridMap;
        private void Start()
        {
            _eventDown.OnRaised += checkUnitInCell;
        }

        private void checkUnitInCell(Vector2 value)
        {
            Vector2Int mousePos = new Vector2Int(Mathf.RoundToInt(value.x),Mathf.RoundToInt(value.y));

            if ((mousePos.x < 0 || mousePos.y < 0))
            {
                
            } 
        }
        
    }
}