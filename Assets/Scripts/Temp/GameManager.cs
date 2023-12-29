using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;

    void Awake() => Instance = this;

    private void Start() => ChangeState(GameState.SpawningPhase);

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.SpawningPhase :
                Debug.Log("State = Spawn");
                break;
            case GameState.MovingPhase :
                Debug.Log("State = Moving");
                break;
            case GameState.HittingPhase:
                Debug.Log("State = Hitting");
                break;
            case GameState.EndingPhase:
                Debug.Log("State = Ending");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
    }
}

public enum GameState
{
    SpawningPhase = 0,
    MovingPhase = 1,
    HittingPhase = 2,
    EndingPhase = 3,
}
