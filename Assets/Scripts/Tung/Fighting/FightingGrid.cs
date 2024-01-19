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
        [SerializeField] private ScriptableEventNoParam _onWin;
        private bool _isCombat;
        
        public GridMapVariable gridMap;
        public Tilemap test;
        public TileBase tileBase;
        private void Start()
        {
            _onFighting.OnRaised += OnCombat;
            _onWin.OnRaised += OnWin;
            Timing.RunCoroutine(Test().CancelWith(gameObject));
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

        private IEnumerator<float> Test()
        {
            while (true)
            {
                for (int i = 0; i < gridMap.size.x; i++)
                {
                    for (int j = 0; j < gridMap.size.y; j++)
                    {
                        if (gridMap.Value[i, j] == null)
                        {
                            test.SetTile(new Vector3Int(i,j),null);
                        }
                        else
                        {
                            test.SetTile(new Vector3Int(i,j),tileBase);
                        }
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
