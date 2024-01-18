using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unit;
using UnityEngine;

namespace Manager
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListHero scriptableListHero;
        [SerializeField] private ScriptableListEnemy scriptableListEnemy;
        [SerializeField] private ScriptableEventNoParam onFinght;
        [SerializeField] private ScriptableEventNoParam onLose;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private GameManager gameManager;
        
        private void Start()
        {
            onFinght.OnRaised += Check;

        }

        private void Check()
        {
            Timing.RunCoroutine(CheckState());
        }
        
        private IEnumerator<float> CheckState()
        {
            while (true)
            {
                if(scriptableListEnemy.Count==0)
                {
                    onVictory.Raise();
                    gameManager.SetGameState(GameManager.State.Victory);
                    break;
                }
            
                if (scriptableListHero.Count==0)
                {
                    onLose.Raise();
                    gameManager.SetGameState(GameManager.State.Defeated);
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
