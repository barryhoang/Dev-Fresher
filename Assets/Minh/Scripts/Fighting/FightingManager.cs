using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
   [SerializeField] private ScriptableListPlayer _soapListPlayer;
   [SerializeField] private ScriptableListEnemy _soapListEnemy;
   [SerializeField] private ScriptableEventNoParam _onLosing;
   [SerializeField] private ScriptableEventNoParam _onWinning;

   private void Start()
   {
      Timing.RunCoroutine(CheckingWinOrLose());
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
      }
   }
}
