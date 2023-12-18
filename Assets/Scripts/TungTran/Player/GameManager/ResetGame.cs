using System;
using Obvious.Soap;
using TungTran.Player.Player;
using UnityEngine;

namespace TungTran.Player.GameManager
{
    public class ResetGame : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private IntVariable _currentWave;
        [SerializeField] private IntVariable _timeWave;
        public void ResetWave()
        {
            Time.timeScale = 1f;
            _playerHealth.ResetHealthPlayer();
            _playerData.ResetStatPlayer();
            _currentWave.ResetToInitialValue();
            _timeWave.ResetToInitialValue();
        }
    }
}
