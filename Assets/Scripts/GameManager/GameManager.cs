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
        [SerializeField] private ScriptableEventNoParam _nextLevel;
        [SerializeField] private ScriptableListCharacter _listCharacters;
        [SerializeField] private ScriptableListEnemy _listEnemies;
        [SerializeField] private IntVariable _timeRound;
        [SerializeField] private GameObject _panelNextLevel;
        [SerializeField] private Button _buttonFight;
        [SerializeField] private Button _buttonNextLevel;
        
        private void Start()
        {
            _combat.OnRaised += OnCombat;
            _nextLevel.OnRaised += OnClickButtonNextLevel;
            _buttonFight.onClick.AddListener(OnClickButtonFight);
            _buttonNextLevel.onClick.AddListener(OnClickButtonNextLevel);
        }

        private void OnClickButtonNextLevel()
        {
            //TODO animation nextLevel
            Timing.RunCoroutine(NextLevel().CancelWith(gameObject));
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
                    Timing.KillCoroutines("Combat");
                    Timing.KillCoroutines("Timer");
                    _nextLevel.Raise();
                    yield break;
                }
                
                MoveManager.instance.Move(_listCharacters,_listEnemies);
                AttackManager.instance.AttackSystem(_listCharacters,_listEnemies);
                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> NextLevel()
        {
            _panelNextLevel.SetActive(true);
            yield return Timing.WaitForSeconds(0.4f);
            _timeRound.ResetToInitialValue();
            _panelNextLevel.SetActive(true);
            int random = Random.Range(1, 5);
            SpawnManager.instance.Spawn(random);
            _panelNextLevel.SetActive(false);

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
