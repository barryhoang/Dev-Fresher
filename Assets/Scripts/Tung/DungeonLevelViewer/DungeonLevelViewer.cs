using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class DungeonLevelViewer : MonoBehaviour
    {
        [SerializeField] private IntVariable _timeRound;
        [SerializeField] private IntVariable _livesRound;
        [SerializeField] private ScriptableEventInt _eventDie;
        [SerializeField] private ScriptableEventNoParam _onFight;
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventNoParam _onReset;
        [SerializeField] private ScriptableEventNoParam _onLose;
        [SerializeField] private ScriptableEventNoParam _timeOut;
        private bool isCountDown;
        private void OnEnable()
        {
            _onFight.OnRaised += OnFight;
            _eventDie.OnRaised += Updatelives;
            _onWin.OnRaised += StopCountDown;
            _onReset.OnRaised += StopCountDown;
            _onLose.OnRaised += StopCountDown;
        }

        private void OnDisable()
        {
            _onFight.OnRaised -= OnFight;
            _eventDie.OnRaised -= Updatelives;
            _onWin.OnRaised -= StopCountDown;
            _onReset.OnRaised -= StopCountDown;
            _onLose.OnRaised -= StopCountDown;
            _timeRound.ResetToInitialValue();
        }

        private void StopCountDown()
        {
            isCountDown = false;
        }

        private void Updatelives(int value)
        {
            _livesRound.Value -= value;
            if (_livesRound.Value <= 0)
            {
                _livesRound.Value = 0;
            }
        }

        private void OnFight()
        {
            isCountDown = true;
            Timing.RunCoroutine(CountDown().CancelWith(gameObject));
        }

        private IEnumerator<float> CountDown()
        {
            while (_timeRound.Value > 0)
            {
                if (!isCountDown) break;
                yield return Timing.WaitForSeconds(1f);
                _timeRound.Value--;
            }
        }
    }
}