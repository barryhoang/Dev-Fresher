using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class PlacementInput : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 btnDown;
    [SerializeField] private ScriptableEventVector2 btnDrag;
    [SerializeField] private ScriptableEventNoParam btnUp;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            btnDown.Raise(GetMousePoint());
        }else if (Input.GetMouseButtonDown(0))
        {
            btnDrag.Raise(GetMousePoint());
        }else if (Input.GetMouseButtonDown(0))
        {
            btnUp.Raise();
        }
    }
    private Vector2 GetMousePoint()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
