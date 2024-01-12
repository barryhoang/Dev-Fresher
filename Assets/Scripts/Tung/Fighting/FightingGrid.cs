using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
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
        public List<PathNode> pathNodes;
        
        public Tilemap _tileTest;
        public TileBase highTileBase;

        private void Start()
        {
            _pathfinding.Init();
            AddEnemies();
            _onFighting.OnRaised += OnCombat;
            Timing.RunCoroutine(Test().CancelWith(gameObject));
        }

        private void OnDisable()
        {
            _onFighting.OnRaised -= OnCombat;
        }

        private void OnCombat()
        {
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }
        
        // xu ly di chuyen nhiem vu la chuyen bat vao cho cac character va enemy den diem den
        // tim con gan nhat se di chuyen den va 4 huong gan nhat 
        // tim xong chuyen path vao cho chung no di chuyen 
        private IEnumerator<float> Move()
        {
            while (true)
            {
                foreach (var unit in _listSoapCharacter)
                {
                    var posStart = unit.transform.position.ToV2Int();
                    var target = _listSoapEnemies.GetClosest(unit.transform.position,_gridMap);
                    pathNodes = _pathfinding.FindPath(posStart.x,posStart.y,target.x, target.y);
                    if (pathNodes.Count == 1)
                    {
                        unit._eventPathNodes.Raise(pathNodes[0]);
                    }
                    else
                    {
                        unit._eventPathNodes.Raise(pathNodes[0]);
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

        private void UpdateGrid()
        {
            for (int i = 0; i < _gridMap.size.x; i++)
            {
                for (int j = 0; j < _gridMap.size.y; j++)
                {
                    if (_gridMap.Value[i, j] == null)
                    {
                        _tileTest.SetTile(new Vector3Int(i, j, 0), null);
                    }
                    else
                    {
                      
                    }
                }
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
