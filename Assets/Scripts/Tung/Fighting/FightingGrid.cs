using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tung
{
    public class FightingGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit _listSoapEnemies;
        [SerializeField] private ScriptableListUnit _listSoapCharacter;
        [SerializeField] private ScriptableEventNoParam _onFighting;
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private List<Unit> _enemies;
        [SerializeField] private Pathfinding _pathfinding;
        
        public Tilemap _tileTest;
        public TileBase highTileBase;

        private void Start()
        {
            _pathfinding.Init();
            AddEnemies();
            _onFighting.OnRaised += OnCombat;
        }

        private void OnDisable()
        {
            _onFighting.OnRaised -= OnCombat;
        }

        private void OnCombat()
        {
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }
        
        private IEnumerator<float> Move()
        {
            while(true)
            {
                foreach (var unit in _listSoapCharacter)
                {
                    if(unit.isAttacking) continue;
                    var target = _listSoapEnemies.GetClosest(unit.transform.position,_gridMap);
                    if (!unit.isMove)
                    {
                        unit.isMove = true;
                        Timing.RunCoroutine(unit.Move(target).CancelWith(gameObject));
                    }
                    else
                    {
                        unit.unitTarget = target;
                    }
                  
                }
                foreach (var unit in _listSoapEnemies)
                {
                    if(unit.isAttacking) continue;
                    var target = _listSoapCharacter.GetClosest(unit.transform.position,_gridMap);
                    if (!unit.isMove)
                    {
                        unit.isMove = true;
                        Timing.RunCoroutine(unit.Move(target).CancelWith(gameObject));
                    }
                    else
                    {
                        unit.unitTarget = target;
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }
        
        private IEnumerator<float> Test()
        {
            while (true)
            {
                TestGrid();
                yield return Timing.WaitForOneFrame;
            }
        }
        private void AddEnemies()
        {
            if(_enemies.Count == 0) return;
            foreach (var unit in _enemies)
            {
                _listSoapEnemies.Add(unit);
                _gridMap.Value[(int) unit.transform.position.x, (int) unit.transform.position.y] = unit;
            }
        }

        private void TestGrid()
        {
            for (int i = 0; i < _gridMap.size.x; i++)
            {
                for (int j = 0; j < _gridMap.size.y; j++)
                {
                    if (_gridMap.Value[i, j])
                    {
                        _tileTest.SetTile(new Vector3Int(i, j, 0), highTileBase);
                    }
                    else
                    {
                        _tileTest.SetTile(new Vector3Int(i, j, 0), null);
                    }
                }
            }
        }
    }
}
