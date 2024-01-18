using System;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class DungeronLevelManager : MonoBehaviour
    {
        [SerializeField] private IntVariable _currentLevel;
        [SerializeField] private ScriptableEventNoParam _onLose;
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventInt _spawnEnemy;
        [SerializeField] private GameObject _placementManager;
        [SerializeField] private GameObject _fightingManager;
        private void Start()
        {
            _onLose.OnRaised += ResetLevel;
            _onWin.OnRaised += NextLevel;
        }

        private void OnDestroy()
        {
            _onLose.OnRaised -= ResetLevel;
            _onWin.OnRaised -= NextLevel;
        }

        private void ResetLevel()
        {
            _currentLevel.Value = 1;
        }

        private void NextLevel()
        {
            _currentLevel.Value++;
            SpawnEnemy();
            _placementManager.SetActive(true);
            _fightingManager.SetActive(false);
        }

        public void SpawnEnemy()
        {
            _spawnEnemy.Raise(_currentLevel);
        }
    }
}