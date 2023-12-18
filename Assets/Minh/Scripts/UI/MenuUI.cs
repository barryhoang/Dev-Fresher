using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minh
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _eventOpenChooseLevel;
        public void StartGame()
        {
            SceneManager.LoadScene("BattleScene");

        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}