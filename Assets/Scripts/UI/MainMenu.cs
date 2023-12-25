using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _exit;

        private void Start()
        {
            _play.onClick.AddListener(Play);
            _exit.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
            Debug.Log("Quit");
        }

        private void Play()
        {
            SceneManager.LoadScene(1);
        }
    }
}
