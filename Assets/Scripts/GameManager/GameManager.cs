using System.Collections.Generic;
using Entity;
using MEC;
using Obvious.Soap;
using StateManager;
using UnityEngine;
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

        private bool isCombat;


        private void Start()
        {
            SpawnManager.instance.Spawn(4);
            _buttonFight.onClick.AddListener(OnClickButtonFight);
            _buttonNextLevel.onClick.AddListener(OnClickButtonNextLevel);
            _combat.OnRaised += OnCombat;
            _nextLevel.OnRaised += OnClickButtonNextLevel;
        }
        

        private void OnClickButtonNextLevel()
        {
            //TODO animation nextLevel
        }

        private void OnCombat()
        {
            isCombat = true;
            Timing.RunCoroutine(CountDownTimer().CancelWith(gameObject), "Timer");
            Timing.RunCoroutine(NextLevel().CancelWith(gameObject),"NextLevel");
        }

        private void OnClickButtonFight()
        {
            _combat.Raise();
        }

        private IEnumerator<float> Combat()
        {
            while (isCombat)
            {
                if (SpawnManager.instance.CheckAll())
                {
                    isCombat = false;
                }
                else
                {
                    SpawnManager.instance.CheckEntityDie();
                    MoveManager.instance.Move(_listCharacters,_listEnemies);
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(AttackManager.instance.AttackSystem(_listCharacters, _listEnemies).CancelWith(gameObject)));
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> NextLevel()
        {
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(Combat().CancelWith(gameObject)));
            _panelNextLevel.SetActive(true);
            yield return Timing.WaitForSeconds(1f);
            int random = Random.Range(1, 5);
            SpawnManager.instance.Spawn(random);
            _timeRound.ResetToInitialValue();
            _panelNextLevel.SetActive(false);
            
        }
        
        private IEnumerator<float> CountDownTimer()
        {
            while (isCombat && _timeRound.Value > 0)
            {
                
                yield return Timing.WaitForSeconds(1f);
                _timeRound.Value--;
            }
        }

        private void OnDisable()
        {
            _combat.OnRaised -= OnCombat;
            _nextLevel.OnRaised -= OnClickButtonNextLevel;
        }
    }
}
