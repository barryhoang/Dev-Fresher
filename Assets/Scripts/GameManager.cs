using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button fightButton;
    public enum State
    {
        Placement,
        Fight,
        Victory,
        Lose
    }
    
    public State currentState;

    private void Awake()
    {
        currentState = State.Placement;
        fightButton.gameObject.SetActive(true);
        var button = fightButton.GetComponent<Button>();
        button.onClick.AddListener(StartOnClick);
    }

    private void Update()
    {
        switch (currentState)
        {
            case (State.Placement):
                Debug.Log("Placement State");
                break;
            case (State.Fight):
                Debug.Log("Fight State");
                break;
            case (State.Victory):
                Debug.Log("Victory State");
                break;
            case (State.Lose):
                Debug.Log("Lose State");
                break;
            default:
                currentState = State.Placement;
                break;
        }
    }

    public void SetGameState(State newState)
    {
        currentState = newState;
    }

    private void StartOnClick()
    {
        currentState = State.Fight;
    }
}
