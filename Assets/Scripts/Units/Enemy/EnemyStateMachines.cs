using UnityEngine;

public class EnemyStateMachines : MonoBehaviour
{
    public Enemy enemy;
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
        Debug.Log("Enemy Current State: "+ currentState);
        switch (currentState)
        {
            case (TurnState.IDLE):
                currentState = TurnState.MOVING;
                break;
            case (TurnState.MOVING):
                enemy.Move();
                break;
            case (TurnState.HITTING):
                break;
            case (TurnState.DEAD):
                break;
        }
    }
}