using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit _listSoapEnemies;
        [SerializeField] private ScriptableEventUnit _eventDieUnit;
        [SerializeField] private ScriptableListUnit _listSoapCharacter;
        [SerializeField] private ScriptableEventNoParam _onFinghting;
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventNoParam _onLose;
        private List<Unit> _enemies;

        private void OnEnable()
        {
            _onFinghting.OnRaised += Check;
            _eventDieUnit.OnRaised += SetDie;
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
            Timing.RunCoroutine(CheckWinOrLose().CancelWith(gameObject));
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

                if (_listSoapCharacter.IsEmpty)
                {
                    _onLose.Raise();
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
