using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Obvious.Soap;

public class HeroStateMachines : MonoBehaviour
{
    private BattleStateMachines BSM;
    public Hero hero; 
    
    /*[SerializeField] private ScriptableListHero _scriptableListHero;
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;*/
    public enum TurnState
    {
        IDLE,
        MOVING,
        HITTING,
        DEAD
    }

    public TurnState currentState;

    private void Start()
    {
        currentState = TurnState.IDLE;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachines>();
        Vector2 startPos = transform.position;
    }

    private void Update()
    {
        Debug.Log("currentState"+ currentState);
        switch (currentState)
        {
            case (TurnState.IDLE):
                ChooseAction();
                currentState = TurnState.MOVING;
                break;
            case (TurnState.MOVING):
                break;
            case (TurnState.HITTING):
                break;
            case (TurnState.DEAD):
                break;
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = hero.name;
        myAttack.AttackGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.Heroes[Random.Range(0, BSM.Heroes.Count)];
        BSM.CollectAction(myAttack);
    }
}
