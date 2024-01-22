using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private IntVariable _livesCharacter;
        [SerializeField] private IntVariable _timeRound;
        [SerializeField] private ScriptableListUnit _listSoapEnemies;
        [SerializeField] private ScriptableEventUnit _eventDieUnit;
        [SerializeField] private ScriptableListUnit _listSoapCharacter;
        [SerializeField] private ScriptableEventNoParam _onFinghting;
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventNoParam _onReset;
        [SerializeField] private ScriptableEventNoParam _onLose;
        [SerializeField] private ScriptableEventInt eventDieInt;
        private int _numberCharacterDie;

        private void OnEnable()
        {
            _onFinghting.OnRaised += Check;
            _eventDieUnit.OnRaised += SetDie;
            _numberCharacterDie = 0;
        }

        private void OnDisable()
        {
            _onFinghting.OnRaised -= Check;
            _eventDieUnit.OnRaised -= SetDie;
        }

        private void SetDie(Unit unit)
        {
            foreach (var character in _listSoapCharacter)
            {
                if (character != unit) continue;
                _listSoapCharacter.Remove(character);
                _numberCharacterDie++;
                return;
            }
            foreach (var enemy in _listSoapEnemies)
            {
                if (enemy != unit) continue;
                _listSoapEnemies.Remove(enemy);
                return;
            }
        }

        private void Check()
        {
            Timing.RunCoroutine(CheckWinOrLose().CancelWith(gameObject), "Combat");
        }

        private IEnumerator<float> CheckWinOrLose()
        {
            while (true)
            {

                if (_listSoapEnemies.IsEmpty)
                {
                    _onWin.Raise();
                    break;
                }

                if (_listSoapCharacter.IsEmpty || _timeRound.Value <= 0)
                {
                    if (_livesCharacter.Value - _numberCharacterDie <= 0)
                    {
                        _onLose.Raise();
                    }
                    else
                    {
                        _onReset.Raise();
                    }
                    Timing.KillCoroutines("Combat");
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
            eventDieInt.Raise(_numberCharacterDie);
            _numberCharacterDie = 0;
        }
    }
}
