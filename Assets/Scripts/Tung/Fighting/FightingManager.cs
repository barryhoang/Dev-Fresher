using System.Collections.Generic;
using UnityEngine;

namespace Tung
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit _listSoapEnemies;
        [SerializeField] private ScriptableListUnit _listSoapCharacter;
        [SerializeField] private GridMapVariable _gridMap;
        
        private List<Unit> _enemies;
    }
}
