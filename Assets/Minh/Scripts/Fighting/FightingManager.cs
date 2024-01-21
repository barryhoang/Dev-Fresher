using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;

namespace Minh
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListPlayer _soapListPlayer;
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableEventNoParam _onLosing;
        [SerializeField] private ScriptableEventNoParam _onWinning;
        [SerializeField] private FightingMapVariable _fightingMap;
       

        private void OnDestroy()
        {
            DeleteEvent();
        }

        private void OnDisable()
        {
            DeleteEvent();
        }

        private void OnEnable()
        {
            AddEvent();
        }
        


        public void OnCombat()
        {
            
            // Timing.RunCoroutine(CheckingWinOrLose().CancelWith(gameObject),"CheckWinLost");
            
            foreach (Player p in _soapListPlayer)
            {
                if (_soapListPlayer.Count == 0) return;
                Timing.RunCoroutine(p.Move().CancelWith(p.gameObject),"move"+p._gameObjectID);
            }

            foreach (Enemy e in _soapListEnemy)
            {
                if (_soapListEnemy.Count == 0) return;

                Timing.RunCoroutine(e.Move().CancelWith(e.gameObject));
            }
        }

        private void OnListNumberChanged(Enemy enemy)
        {
            if (_soapListEnemy.Count == 0)
            {
                if (Application.isPlaying)
                {
                    foreach (Player p in _soapListPlayer)
                    {
                        //Destroy(p.gameObject);
                        Timing.KillCoroutines("move" + p._gameObjectID);


                        p.ResetHero();
                    }

                    _onWinning.Raise();
                    //  _currentLevel.Value++;
                    Debug.Log("Clear");
                }
            }
        }

        private void OnPlayerListChanged(Player player)
        {
            if (_soapListPlayer.Count == 0)
            {
                _onLosing.Raise();
            }
        }

        #region Event

        private void DeleteEvent()
        {
            _soapListEnemy.OnItemRemoved -= OnListNumberChanged;
            _soapListPlayer.OnItemRemoved -= OnPlayerListChanged;
        }

        private void AddEvent()
        {
            _soapListEnemy.OnItemRemoved += OnListNumberChanged;
            _soapListPlayer.OnItemRemoved += OnPlayerListChanged;
        }

        #endregion
      
    }
}