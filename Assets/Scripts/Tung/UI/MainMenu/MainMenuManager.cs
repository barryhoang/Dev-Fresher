using System.Collections;
using System.Collections.Generic;
using MEC;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tung
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button _exit;
        [SerializeField] private Button _play;
        [SerializeField] private Transform _tile;
        public float endValue;
        private void Start()
        {
            _play.onClick.AddListener(OnClickPlay);
            _exit.onClick.AddListener(OnClickExit);
            Tween.Scale(_tile, 1, 0.2f);
            Tween.Scale(transform, endValue, 0.5f);
        }

        public void OnClickPlay()
        {
            SceneManager.LoadScene(1);
        }

        public void OnClickExit()
        {
            Application.Quit();
        }
    }
}
