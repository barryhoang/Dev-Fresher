using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class MainMenuManager : MonoBehaviour
    {
        public void OnPlayButton () => SceneManager.LoadScene(sceneBuildIndex: 1);
        
        public void OnQuitButton () => Application.Quit();
    }
}