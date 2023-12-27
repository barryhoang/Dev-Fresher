using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachines : MonoBehaviour
{
    public Enemy enemy;
    public TurnState currentState;
    public Button StartButton;
    
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
        Button button = StartButton.GetComponent<Button>();
        button.onClick.AddListener(StartOnClick);
    }

    private void Update()
    {
        Debug.Log("Enemy Current State: "+ currentState);
        switch (currentState)
        {
            case (TurnState.IDLE):
                break;
            case (TurnState.MOVING):
                Destroy(StartButton);
                break;
            case (TurnState.HITTING):
                break;
            case (TurnState.DEAD):
                break;
        }
    }
    private void StartOnClick()
    {
        currentState = TurnState.MOVING;
    }
}