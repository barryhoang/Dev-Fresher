using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minh
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _onPlayerRevive;
        [SerializeField] protected Button _retryButton;
        [SerializeField] private IntVariable _reviveTime;
        [SerializeField] private TextMeshProUGUI _dieTimeLeft;
        [SerializeField] private TextMeshProUGUI _retryText;
        
        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
        
        private void OnEnable()
        {
            if (_reviveTime > 0)
            {
                _retryText.text = "Retry";
                _retryButton.onClick.AddListener(Retry);
                _dieTimeLeft.text = "You have " + (_reviveTime.Value-1) + " more life";
            }
            else
            {
                _dieTimeLeft.text = "You Died";
                _retryText.text = "Restart";
            }
       
            Time.timeScale = 0f;
        }

        public void FinishLevel()
        {
            _retryText.text = "Next Level";
            _retryButton.onClick.AddListener(() =>
                {
                    Debug.Log("PassLevel");
                }
            );
            _dieTimeLeft.text = "Congratulation";
        }
        private void Retry()
        {
            if (_reviveTime > 0)
            {
                _reviveTime.Add(-1);
                _onPlayerRevive.Raise();
                OnDisable();
               
                gameObject.SetActive(false);
            }
        
        }
    }
}
