using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public enum State
        {
            Placement,
            Fight,
            Victory,
            Defeated
        }
    
        public State currentState;

        private void Awake()
        {
            currentState = State.Placement;
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
                case (State.Defeated):
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
    }
}
