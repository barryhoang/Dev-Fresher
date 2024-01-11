using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlacementInput : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 _buttonDown;
        [SerializeField] private ScriptableEventVector2 _buttonDrag;
        [SerializeField] private ScriptableEventVector2 _buttonUp;
        [SerializeField] private MouseDrag _mouseDrag;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _buttonDown.Raise(GetMousePoint());
            }
            
            else if (Input.GetMouseButtonUp(0))
            {
                _buttonUp.Raise(GetMousePoint());
            }
        }

        private Vector2 GetMousePoint()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }
}