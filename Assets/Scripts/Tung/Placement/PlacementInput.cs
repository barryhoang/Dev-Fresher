using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tung
{
    public class PlacementInput : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 _eventDrag;
        [SerializeField] private ScriptableEventVector2 _eventDown;
        [SerializeField] private ScriptableEventVector2 _eventUp;

        private bool isDraging;

        private void Start()
        {
            Timing.RunCoroutine(MouseInput().CancelWith(gameObject));
        }

        private IEnumerator<float> MouseInput()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _eventDown.Raise( MousePosition());
                }

                if (Input.GetMouseButton(0))
                {
                    _eventDrag.Raise( MousePosition());
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    _eventUp.Raise( MousePosition());
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        private Vector2 MousePosition()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }
    }
}