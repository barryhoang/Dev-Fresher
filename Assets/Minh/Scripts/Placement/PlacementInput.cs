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
        
        private void Update()
        {
            float a = 5;
            Debug.Log((int)(a+0.8f));
             if (Input.GetMouseButton(0))
            {
                Vector2 cursorScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
                buttonDown.Raise(cursorWorldPoint);
            }
        }
    }
}