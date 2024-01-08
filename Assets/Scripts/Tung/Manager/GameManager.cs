using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button _buttonFight;
        [SerializeField] private ScriptableEventNoParam _fight;
        [SerializeField] private IntVariable _timeRound;
        [SerializeField] private GameObject _gridSize;

        private void Start()
        {
            _buttonFight.onClick.AddListener(OnClickButtonFight);
            _fight.OnRaised += OnFight;
        }

        private void OnFight()
        {
            Timing.RunCoroutine(CountDownTimer().CancelWith(gameObject));
            Timing.RunCoroutine(Combat().CancelWith(gameObject));
            _buttonFight.gameObject.SetActive(false);
        }

        private IEnumerator<float> Combat()
        {
            _gridSize.SetActive(false);
            while (true)
            {
                if (SpawnManager.instance.CheckEnemiesDie())
                {
                    yield break;
                }
                else
                {
                    MoveManager.instance.Move();
                    AttackManager.instance.AttackSystem();
                    SpawnManager.instance.CheckEntityDie();
                }
                yield return Timing.WaitForOneFrame;
            }

        }

        private IEnumerator<float> Test()
        {
            while (true)
            {
                GridMapManager.instance.UpdateGrid();
                yield return Timing.WaitForOneFrame;
            }
        }
        private void OnClickButtonFight()
        {
            _fight.Raise();
        }

        private void OnDestroy()
        {
            _fight.OnRaised -= OnFight;
        }

        private IEnumerator<float> CountDownTimer()
        {
            while (_timeRound > 0)
            {
                yield return Timing.WaitForSeconds(1f);
                _timeRound.Value--;
            }
        }
    }
}
