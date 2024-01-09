using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minh
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("BattleScene");
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void OpenDiscord()
        {
            Application.OpenURL("https://www.youtube.com/watch?v=xvFZjo5PgG0");
        }
    }
}