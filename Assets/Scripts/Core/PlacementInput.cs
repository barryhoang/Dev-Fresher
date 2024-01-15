
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class PlacementInput : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 _onMouseDown;
    [SerializeField] private ScriptableEventVector2 _onMouseHold;
    [SerializeField] private ScriptableEventVector2 _onMouseUp;
    
    private void Start()
    {
        Timing.RunCoroutine(_getMousePositions().CancelWith(gameObject));
    }

    private IEnumerator<float> _getMousePositions()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _onMouseDown.Raise(getMousePosition());
            }
            else if (Input.GetMouseButton(0))
            {
                _onMouseHold.Raise(getMousePosition());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _onMouseUp.Raise(getMousePosition());
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    private Vector2 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
