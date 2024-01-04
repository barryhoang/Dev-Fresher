using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachines : MonoBehaviour
{
    public Enemy enemy;
    public TurnState currentState;
    [SerializeField] private Button startButton;
    
    
    private void Start()
    {
        currentState = TurnState.IDLE;
        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(StartOnClick);
    }

    private void Update()
    {
        Debug.Log("Enemy Current State: "+ currentState);
        switch (currentState)
        {
            case (TurnState.IDLE):
                break;
            case (TurnState.PLAYING):
                //enemy.Move();
                break;
            case (TurnState.DEAD):
                break;
        }
    }
    
    
    private void StartOnClick()
    {
        currentState = TurnState.PLAYING;
    } 
    
    public enum TurnState
    {
        IDLE,
        PLAYING,
        DEAD
    }
}