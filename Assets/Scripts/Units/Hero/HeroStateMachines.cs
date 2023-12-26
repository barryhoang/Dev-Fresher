using UnityEngine;

public class HeroStateMachines : MonoBehaviour
{
    public Hero hero;
    public TurnState currentState;
    
    public enum TurnState
    {
        IDLE,
        MOVING,
        HITTING,
        DEAD
    }

    private void Start()
    {
        currentState = TurnState.IDLE;
    }

    private void Update()
    {
        Debug.Log("Hero Current State: "+ currentState);
        switch (currentState)
        {
            case (TurnState.IDLE):
                currentState = TurnState.MOVING;
                break;
            case (TurnState.MOVING):
                hero.Move();
                break;
            case (TurnState.HITTING):
                break;
            case (TurnState.DEAD):
                break;
        }
    }
}
