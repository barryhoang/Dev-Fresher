using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class ButtonMenu : MonoBehaviour
    {
        public void OnClickButtonPlay()
        {
            SceneManager.LoadScene(1);
        }

        public void OnClickButtonExit()
        {
            UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
        }
    }
}
