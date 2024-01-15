using System;
using System.Collections.Generic;
using System.Linq;
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
            
           
        }

        private void Start()
        {
            gameObject.SetActive(false);
           
        }

        public void OnCombat()
        {
            Timing.RunCoroutine(CheckingWinOrLose().CancelWith(gameObject));
            foreach (Player p in _soapListPlayer)
            {
                if (_soapListPlayer.Count == 0) return;
                Timing.RunCoroutine(p.Move().CancelWith(p.gameObject));
            }
            foreach (Enemy e in _soapListEnemy)
            {
                if (_soapListEnemy.Count == 0) return;
                
                Timing.RunCoroutine(e.Move().CancelWith(e.gameObject));
            }
        }
        private IEnumerator<float> CheckingWinOrLose()
        {
            while(true)
            {
                if (_soapListEnemy.Count == 0)
                {
                    _onWinning.Raise();
                    Timing.KillCoroutines();
                    break;
                }
                else if (_soapListPlayer.Count == 0)
                {
                    _onLosing.Raise();
                    Timing.KillCoroutines();
                    break;
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}

