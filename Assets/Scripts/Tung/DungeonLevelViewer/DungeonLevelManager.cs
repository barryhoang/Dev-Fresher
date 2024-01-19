using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class DungeonLevelManager : MonoBehaviour
    {
        [SerializeField] private IntVariable _currentlevel;
        [SerializeField] private ScriptableEventInt _numberEnemy;
        [SerializeField] private Button _buttonNextLevel;
        [SerializeField] private ScriptableEventNoParam _onFight;
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventNoParam _onLose;
        [SerializeField] private GameObject _levelViewer;
        [SerializeField] private GameObject _panelWin;
        [SerializeField] private PlacementManager _placementManager;
        
        private void Start()
        {
            _buttonNextLevel.onClick.AddListener(NextLevel);
            _numberEnemy.Raise(_currentlevel.Value);
            _onFight.OnRaised += OnFight;
            _onWin.OnRaised += StopCountDown;
            _onLose.OnRaised += StopCountDown;
        }

        private void OnDisable()
        {
            _onFight.OnRaised -= OnFight;
            _onWin.OnRaised -= StopCountDown;
            _onLose.OnRaised -= StopCountDown;
        }

        private void StopCountDown()
        {
            _levelViewer.SetActive(false);
            _panelWin.SetActive(true);
        }

        private void OnFight()
        {
            _levelViewer.SetActive(true);
         
        }
        
        private void NextLevel()
        {
            _placementManager.gameObject.SetActive(true);
            _currentlevel.Value++;
            _panelWin.SetActive(false);
            _numberEnemy.Raise(_currentlevel);
        }
    }
}