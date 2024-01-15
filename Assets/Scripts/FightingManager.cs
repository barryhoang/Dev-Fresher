using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableEventNoParam onLose;
    [SerializeField] private ScriptableEventNoParam onVictory;

    private void Start()
    {
        Timing.RunCoroutine(CheckCont());
    }

    private IEnumerator<float> CheckCont()
    {
        while (true)
        {
            if (scriptableListEnemy.Count==0)
            {
                onVictory.Raise();
            }
            else if (scriptableListHero.Count==0)
            {
                onLose.Raise();
            }
        }
    }
}
