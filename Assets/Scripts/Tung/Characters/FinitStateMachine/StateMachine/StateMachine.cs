using UnityEngine;

namespace Tung
{
    public class StateMachine
    {
        public State currentState;

        public void InitiateState(State newState)
        {
            currentState = newState;
            currentState.Enter();
        }

        public void ChangeState(State newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}