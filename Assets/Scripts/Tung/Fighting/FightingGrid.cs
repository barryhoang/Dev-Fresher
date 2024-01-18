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
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private List<Unit> _enemies;
        [SerializeField] private Pathfinding _pathfinding;
        private bool _isCombat;

        public Tilemap _tileTest;
        public TileBase highTileBase;

        private void Start()
        {
            _pathfinding.Init();
            AddEnemies();
            _onFighting.OnRaised += OnCombat;
            _onWin.OnRaised += OnWin;

        }

        private void OnDisable()
        {
            _onWin.OnRaised -= OnWin;
            _onFighting.OnRaised -= OnCombat;
        }

        private void OnCombat()
        {
            _isCombat = true;
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }

        private void OnWin() => _isCombat = false;
        private IEnumerator<float> Move()
        {
            while(_isCombat)
            {
                foreach (var unit in _listSoapCharacter)
                {
                    if(unit.isAttacking) continue;
                    var target = _listSoapEnemies.GetClosest(unit.transform.position);
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
                    var target = _listSoapCharacter.GetClosest(unit.transform.position);
                    if (!unit.isMove)
                    {
                        unit.isMove = true;
                        Timing.RunCoroutine(unit.Move(target).CancelWith(gameObject),"Move");
                    }
                    else
                    {
                        unit.unitTarget = target;
                    }
                }
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
    }
}
