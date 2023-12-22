using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Obvious.Soap;
using Unity.VisualScripting;

namespace Minh
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableListPlayer _soapListPlayer;
        [SerializeField] private ScriptableListPlayer _soapListDiedPlayer;
        [SerializeField] private ScriptableEventNoParam _playerRevive;
        [SerializeField] private ScriptableEventNoParam _allEnemyDied;
        [SerializeField] private ScriptableEventNoParam _allPlayerDied;
        [SerializeField] private IntVariable _currentLevel;

        public AllLevelSpawner _LevelSpawner;
        public GameState _gameState;

        private void Start()
        {
            _gameState = GameState.Placement;
            _soapListEnemy.OnItemRemoved += OnListNumberChanged;
            _soapListPlayer.OnItemRemoved += OnPlayerListChanged;
        }
        private void OnDestroy()
        {
            _soapListEnemy.OnItemRemoved -= OnListNumberChanged;
            _soapListPlayer.OnItemRemoved -= OnPlayerListChanged;
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
        
        private void OnPlayerListChanged(Player player)
        {
            if (_soapListPlayer.Count == 0)
            {
                _allPlayerDied.Raise();
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

        public void AllPlayerRevive()
        {
            Debug.Log("B");
            foreach (Player p in _soapListDiedPlayer)
            {
                Debug.Log("A");
                p.gameObject.SetActive(true);
                p._health = p.characterStats._maxHealth;
                p.transform.position = p.Playerplacement._prevTransform;
                
            }
            _soapListDiedPlayer.Reset();
            foreach (Enemy e in _soapListEnemy)
            {
                e.Die();
            }
            _soapListEnemy.Reset();
        
            _gameState = GameState.Placement;
            _LevelSpawner.SpawnNewLevel();
            Debug.Log("spawn");
        }
    }
}