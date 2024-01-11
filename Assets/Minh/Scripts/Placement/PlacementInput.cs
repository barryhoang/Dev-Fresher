using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlacementInput : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 _buttonDown;
        [SerializeField] private ScriptableEventVector2 _buttonDrag;
        [SerializeField] private ScriptableEventNoParam _buttonUp;

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
            else if (Input.GetMouseButton(0))
            {
                _buttonDrag.Raise(GetMousePoint());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _buttonUp.Raise();
            }
        }

        private Vector2 GetMousePoint()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}