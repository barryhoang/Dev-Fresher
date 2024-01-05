using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachines : MonoBehaviour
{
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
        Debug.Log("Hero Current State: "+ currentState);
        switch (currentState)
        {
            case (TurnState.IDLE):
                break;
            case (TurnState.PLAYING):
                Destroy(startButton);
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
