using System.Collections;
using System.Collections.Generic;
using Maps;
using Units.Hero;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private MapVariable mapVariable;

    private void Start()
    {
        mapVariable.Init();
    }
    
    
}
