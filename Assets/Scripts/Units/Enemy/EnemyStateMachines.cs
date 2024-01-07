using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

namespace Units.Enemy
{
    public class EnemyStateMachines : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        public TurnState currentState;
        
        private void Start()
        {
            currentState = TurnState.Idle;
            var button = startButton.GetComponent<Button>();
            button.onClick.AddListener(StartOnClick);
            Timing.RunCoroutine(DebuggingState().CancelWith(gameObject));
        }

        private void Update()
        {
            switch (currentState)
            {
                case (TurnState.Idle):
                    break;
                case (TurnState.Playing):
                    break;
                case (TurnState.Hitting):
                    break;
                case (TurnState.Dead):
                    break;
                default:
                    currentState = TurnState.Idle;
                    break;
            }
        }
        
        private IEnumerator<float> DebuggingState()
        {
            Debug.Log("Enemy Current State: "+ currentState);
            yield return Timing.WaitForSeconds(3);
        }
    
        private void StartOnClick()
        {
            currentState = TurnState.Playing;
        } 
    
        public enum TurnState
        {
            Idle,
            Playing,
            Hitting,
            Dead
        }
    }
}