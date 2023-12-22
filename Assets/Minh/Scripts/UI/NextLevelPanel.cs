using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minh
{
    public class NextLevelPanel : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _nextLevel;
        [SerializeField] private ScriptableEventNoParam _allPlayerRevive;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;
        public void SpawnNextLevel()
        {
            _nextLevel.Raise();
        }
        
        public void PlayerDied()
        {
            _button.onClick.AddListener(() =>
            {
                Debug.Log("Restart");
                _allPlayerRevive.Raise();
            });
           
            _buttonText.text = "Retry";
        }

        public void NextLevel()
        {
            _button.onClick.AddListener(() =>
            {
                _nextLevel.Raise();
            });
            _buttonText.text = "Next Level";
        }

        public void RemoveListener()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}

