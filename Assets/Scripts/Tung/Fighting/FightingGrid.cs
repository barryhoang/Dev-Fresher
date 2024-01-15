using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;
using Path = System.IO.Path;

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
        
        private IEnumerator<float> Move()
        {
            foreach (var unit in _listSoapCharacter)
            {
                var posTarget = _listSoapEnemies.GetClosest(unit.transform.position,_gridMap);
                unit.Path  =  ABPath.Construct(transform.position, (Vector2)posTarget, null);
                unit.ai.StartPath(unit.Path);
            }
            yield return Timing.WaitForOneFrame;
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
