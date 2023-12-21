using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Obvious.Soap;
namespace Minh
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableListPlayer _soapListPlayer;
        [SerializeField] private ScriptableEventNoParam _allEnemyDied;
        [SerializeField] private IntVariable _currentLevel;
        public GameState _gameState;

        private void Start()
        {
            _gameState = GameState.Placement;
            _soapListEnemy.OnItemRemoved += OnListNumberChanged;
        }
        private void OnDestroy()
        {
            _soapListEnemy.OnItemRemoved -= OnListNumberChanged;
        }

        private void OnListNumberChanged(Enemy enemy)
        {
            if (_soapListEnemy.Count == 0)
            {
                _allEnemyDied.Raise();
                _currentLevel.Value++;
                Debug.Log("Clear");
            }
        }

        public void OnFight()
        {
            _gameState = GameState.Fighting;
        }

        public void EndFight()
        {
            _gameState = GameState.Placement;
        }

        private void Reset()
        {
            throw new NotImplementedException();
        }
    }
}