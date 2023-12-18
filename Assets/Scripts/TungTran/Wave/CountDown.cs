using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TungTran.Player.Player;
using UnityEngine;

namespace TungTran.Wave
{
    public class CountDown : MonoBehaviour
    {
        private Spawn _spawn;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField]  private IntVariable _timeInWave;
        [SerializeField] private IntVariable _currentWave;
        [SerializeField] private ScriptableEventBool _stop;
        [SerializeField] private ScriptableEventBool _play;
        
        private void Start()
        {
            _play.Raise(true);
            
        }
        public void StartCountdownTimer()
        {
            Timing.RunCoroutine(CountdownTimer().CancelWith(gameObject));
        }
        
        private IEnumerator<float> CountdownTimer()
        {
            while (_timeInWave.Value > 0)
            {
                yield return Timing.WaitForSeconds(1f);
                _timeInWave.Value--;
            }
            _playerHealth.ResetHealthPlayer();
            _timeInWave.ResetToInitialValue();
            _currentWave.Value++;
            _stop.Raise(true);
            Timing.KillCoroutines("Spawn");
        }
    }
}
