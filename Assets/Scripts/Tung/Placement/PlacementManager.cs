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
        [SerializeField] private ScriptableEventUnit _eventPosUnit;
        [SerializeField] private ScriptableListUnit _listSoapCharacter;
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private List<Unit> _units;
        [SerializeField] private GameObject _gridSize;

        private Vector2Int[] _posCharacter = new Vector2Int[4];

        private void OnEnable()
        {
            _gridSize.SetActive(true);
            _buttonFighting.gameObject.SetActive(true);
        }

        private void Start()
        {
            SpawnUnit();
            ResetPosCharacter();
            _buttonFighting.onClick.AddListener(OnFighting);
            _eventPosUnit.OnRaised += UpdatePos;
        }

        private void OnDestroy()
        {
            _eventPosUnit.OnRaised -= UpdatePos;
        }
        private void UpdatePos(Unit unit)
        {
            for (int i = 0; i < _listSoapCharacter.Count; i++)
            {
                if (_listSoapCharacter[i] == unit)
                {
                    _posCharacter[i] = _listSoapCharacter[i].transform.position.ToV2Int();
                    return;
                }
            }
        }

        private void OnFighting()
        {
            _onFighting.Raise();
            _buttonFighting.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _gridSize.SetActive(false);
        }

        public void ResetPosCharacter()
        {
            _listSoapCharacter.RemoveRange(0, _listSoapCharacter.Count);
            for (int i = 0; i < _units.Count; i++)
            {
                _listSoapCharacter.Add(_units[i]);
            }
            for (int i = 0; i < _listSoapCharacter.Count; i++)
            {
                _listSoapCharacter[i].gameObject.SetActive(false);
                var pos = _listSoapCharacter[i].transform.position.ToV2Int();
                _gridMap.Value[pos.x, pos.y] = null;
                _units[i].transform.position = (Vector2)_posCharacter[i];
                _gridMap.Value[_posCharacter[i].x, _posCharacter[i].y] = _units[i];
                _listSoapCharacter[i].gameObject.SetActive(true);
            }
        }
        private void SpawnUnit()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                _listSoapCharacter.Add(_units[i]);
                var pos = _units[i].transform.position.ToV2Int();
                _gridMap.Value[pos.x, pos.y] = _units[i];
                _posCharacter[i] = pos;
            }

        }
    }
}
