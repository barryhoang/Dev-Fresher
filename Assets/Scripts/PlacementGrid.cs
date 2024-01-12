
using System;
using System.Collections;
using System.Collections.Generic;
using Maps;
using UnityEngine;
using UnityEngine.Tilemaps;
using Obvious.Soap;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField] private MapVariable _mapVariable;
    [SerializeField] private ScriptableEventVector2 btnDown;
    [SerializeField] private ScriptableEventVector2 btnDrag;
    [SerializeField] private ScriptableEventNoParam btnUp;


    private void Start()
    {
        btnDown.Raise();
    }
}
