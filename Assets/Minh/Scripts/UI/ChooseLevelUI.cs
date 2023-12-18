using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minh
{
    public class ChooseLevelUI : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _openMainMenuUI;
        
        public void StartGame()
        {
            SceneManager.LoadScene("BattleScene");
        }

        public void ExitToMainMenu()
        {
            _openMainMenuUI.Raise();
        }
    }
}