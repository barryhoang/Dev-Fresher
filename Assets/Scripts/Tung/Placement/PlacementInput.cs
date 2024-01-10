using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tung
{
    public class PlacementInput : MonoBehaviour
    {
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
                    isDraging = true;
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _eventDown.Raise(mousePosition);
                }

                if (Input.GetMouseButtonDown(0) && isDraging)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _eventDown.Raise(mousePosition);
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    isDraging = false;
                    
                }
                
                
                yield return Timing.WaitForOneFrame;
            }
        } 
    }
}