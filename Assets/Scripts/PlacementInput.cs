using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class PlacementInput : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 btnDown;
    [SerializeField] private ScriptableEventVector2 btnDrag;
    [SerializeField] private ScriptableEventVector2 btnUp;
    
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
        }else if (Input.GetMouseButton(0))
        {
            btnDrag.Raise(GetMousePoint());
        }else if (Input.GetMouseButtonUp(0))
        {
            btnUp.Raise(GetMousePoint());
        }
    }
    private Vector2 GetMousePoint()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
