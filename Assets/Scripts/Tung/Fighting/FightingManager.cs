using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tung
{
    public class FightingManager : MonoBehaviour
    {
        // quan ly nhan vat va quai
        [SerializeField] private ScriptableListUnit _listSoapEnemies;
        [SerializeField] private ScriptableListUnit _listSoapCharacter;
        [SerializeField] private GridMapVariable _gridMap;
        
        private List<Unit> _enemies;

        private void Start()
        {
            
        }

        private void AddEnemies()
        {
            if(_enemies.Count == 0) return;
            foreach (var unit in _enemies)
            {
                _listSoapEnemies.Add(unit);
            }
        }
        // kiem tra thang thua 
        // thang gui event cua thang 
        // thua gui event cua thua 
    }
}
