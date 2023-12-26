using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class BattleStateMachines : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction battleStates;
    public List<GameObject> Heroes = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    private void Start()
    {
        battleStates = PerformAction.WAIT;
        Heroes.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

    }

    private void Update()
    {
        switch (battleStates)
        {
            case(PerformAction.WAIT):
                break;
            case(PerformAction.TAKEACTION):
                break;
            case (PerformAction.PERFORMACTION):
                break;
        }
    }
}
