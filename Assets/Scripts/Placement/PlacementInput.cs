using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Placement
{
    public class PlacementInput : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 onMouseDrag;
        [SerializeField] private ScriptableEventVector2 onMouseDown;
        [SerializeField] private ScriptableEventVector2 onMouseUp;

        private void OnEnable()
        {
            Timing.RunCoroutine(MouseInput().CancelWith(gameObject));
        }
    
        private IEnumerator<float> MouseInput()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    onMouseDown.Raise( MousePosition());
                }

                if (Input.GetMouseButton(0))
                {
                    onMouseDrag.Raise( MousePosition());
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    onMouseUp.Raise( MousePosition());
                }
                yield return Timing.WaitForOneFrame;
            }
            // ReSharper disable once IteratorNeverReturns
        }
        private static Vector2 MousePosition()
        {   
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }                   
    }
}
