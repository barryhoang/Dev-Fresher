using System;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlacementInput : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private ScriptableEventVector2 buttonDown;
        [SerializeField] private FightingMapVariable _fightingMap;
        private void Update()
        {
            float a = 5;
          
             if (Input.GetMouseButton(0))
            {
                Debug.Log(_fightingMap.Value.Length+" DO DAI");
                Vector2 cursorScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
                buttonDown.Raise(cursorWorldPoint);
            }
        }
    }
}