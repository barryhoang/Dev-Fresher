using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class PlacementManager : MonoBehaviour
    {
        [SerializeField] private Button _buttonFighting;
        [SerializeField] private ScriptableEventNoParam _onFighting; 
        [SerializeField] private ScriptableListUnit _listSoapUnit;
       [SerializeField] private GridMapVariable _gridMap;
       [SerializeField] private List<Unit> _units;
       [SerializeField] private GameObject gridMap;

       private void Start()
       {
           _buttonFighting.onClick.AddListener(OnFighting);
           SpawnUnit();
       }

       private void OnFighting()
       {
           _onFighting.Raise();
           _buttonFighting.gameObject.SetActive(false);
           gameObject.SetActive(false);
           gridMap.SetActive(false);
       }

       private void SpawnUnit()
       {
           foreach (var unit in _units)
           {
               _listSoapUnit.Add(unit);
               _gridMap.Value[Mathf.RoundToInt(unit.transform.position.x),Mathf.RoundToInt(unit.transform.position.y)] = unit;
           }
       }
    }
}
