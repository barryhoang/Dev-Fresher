using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tung
{
    public class PlacementManager : MonoBehaviour
    {
       [SerializeField] private ScriptableListUnit _listSoapUnit;
       [SerializeField] private GridMapVariable _gridMap;
       [SerializeField] private List<Unit> _units;

       private void Start()
       {
           SpawnUnit();
       }

       private void SpawnUnit()
       {
           for (int i = 0; i < _units.Count; i++)
           {
               _listSoapUnit.Add(_units[i]);
               _gridMap.Value[Mathf.RoundToInt(_units[i].transform.position.x),Mathf.RoundToInt(_units[i].transform.position.y)] = _units[i];
           }
       }
    }
}
