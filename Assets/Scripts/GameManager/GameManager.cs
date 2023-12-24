using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using MEC;
using Obvious.Soap;
using StateManager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _combat;
        [SerializeField] private ScriptableListCharacter _listCharacters;
        [SerializeField] private ScriptableListEnemy _listEnemies;
        [SerializeField] private IntVariable _timeRound;
        [SerializeField] private Button _buttonFight;
        [SerializeField] private Button _buttonNextLevel;
        
        
        
        private void Start()
        {
            _combat.OnRaised += OnCombat;
            _buttonFight.onClick.AddListener(OnClickButtonFight);
            _buttonNextLevel.onClick.AddListener(OnClickButtonNextLevel);
        }

        private void OnClickButtonNextLevel()
        {
            int random = Random.Range(1, 5);
            //TODO animation nextLevel
            _timeRound.ResetToInitialValue();
            SpawnManager.instance.Spawn(random);
        }

        private void OnCombat()
        {
            Timing.RunCoroutine(CountDownTimer().CancelWith(gameObject), "Timer");
            Timing.RunCoroutine(Combat().CancelWith(gameObject), "Combat");
        }

        private void OnClickButtonFight()
        {
            _combat.Raise();
        }

        private IEnumerator<float> Combat()
        {
            while (true)
            {
                if (_listCharacters.IsEmpty || _listEnemies.IsEmpty)
                {
                    Timing.KillCoroutines("Timer");
                    yield break;
                }
                
                MoveManager.instance.Move(_listCharacters,_listEnemies);
                AttackManager.instance.AttackSystem(_listCharacters,_listEnemies);
                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> CountDownTimer()
        {
            while (_timeRound >= 0)
            {
                yield return Timing.WaitForSeconds(1f);
                _timeRound.Value--;
            }
        }

        private void OnDisable()
        {
            _combat.OnRaised -= OnCombat;
            Timing.KillCoroutines("Kill");
            Timing.KillCoroutines("Combat");
        }
    }
}
