using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Tung
{
    public class DungeonLevelManager : MonoBehaviour
    {
        [SerializeField] private IntVariable _currentlevel;
        [SerializeField] private IntVariable _livesCharacter;
        [SerializeField] private IntVariable _timeRound;
        [SerializeField] private ScriptableEventInt _numberEnemy;
        [SerializeField] private ScriptableEventNoParam _clickWin;
        [SerializeField] private ScriptableEventNoParam _clickReset;
        [SerializeField] private ScriptableEventNoParam _clickLose;
        [SerializeField] private PlacementManager _placementManager;
        [SerializeField] private DungeonLevelGrid _levelGrid;

        private void Start()
        {
            _numberEnemy.Raise(_currentlevel.Value);
            _clickWin.OnRaised += NextLevel;
            _clickLose.OnRaised += Lose;
            _clickReset.OnRaised += ResetLevel;
        }

        private void OnDisable()
        {
            _clickWin.OnRaised -= NextLevel;
            _clickLose.OnRaised -= Lose;
            _clickReset.OnRaised -= ResetLevel;
        }

        private void Lose()
        {
            // _levelGrid.OnLose();
            // _livesCharacter.ResetToInitialValue();
            // _timeRound.ResetToInitialValue();
            // _placementManager.gameObject.SetActive(true);
            // _placementManager.ResetPosCharacter();
            // _currentlevel.Value = 1;
            // _numberEnemy.Raise(_currentlevel);
            SceneManager.LoadScene(0);
        }

        private void ResetLevel()
        {
            _timeRound.ResetToInitialValue();
            _levelGrid.OnLose();
            _placementManager.gameObject.SetActive(true);
            _placementManager.ResetPosCharacter();
            _numberEnemy.Raise(_currentlevel);
        }
        private void NextLevel()
        {
            _timeRound.ResetToInitialValue();
            _placementManager.gameObject.SetActive(true);
            _placementManager.ResetPosCharacter();
            _currentlevel.Value++;
            _numberEnemy.Raise(_currentlevel);
        }
    }
}