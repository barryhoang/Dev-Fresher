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
           for (int i = 0; i < _units.Count; i++)
           {
               _listSoapUnit.Add(_units[i]);
               _gridMap.Value[Mathf.RoundToInt(_units[i].transform.position.x),Mathf.RoundToInt(_units[i].transform.position.y)] = _units[i];
           }
       }
    }
}
