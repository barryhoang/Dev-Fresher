using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListPlayer _soapListPlayer;
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableEventNoParam _onLosing;
        [SerializeField] private ScriptableEventNoParam _onWinning;

        private void OnEnable()
        {
            Timing.RunCoroutine(CheckingWinOrLose());
           
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnCombat()
        {
            foreach (Player p in _soapListPlayer)
            {
                Timing.RunCoroutine(p.Move());
            }
        }
        private IEnumerator<float> CheckingWinOrLose()
        {
            while(true)
            {
                if (_soapListEnemy.Count == 0)
                {
                    _onWinning.Raise();
                }
                else if (_soapListPlayer.Count == 0)
                {
                    _onLosing.Raise();
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}

