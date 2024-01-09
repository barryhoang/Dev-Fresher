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
        [SerializeField] private Button _start;
        [SerializeField] private Button _exit;
        [SerializeField] private Button _play;
        [SerializeField] private Transform _tile;
        [SerializeField] private Transform _camera;

        public float endValue;
        public Transform posTarget;
        private void Start()
        {
            _play.onClick.AddListener(OnClickPlay);
            _exit.onClick.AddListener(OnClickExit);
            Timing.RunCoroutine(StartGame().CancelWith(gameObject));
            _start.onClick.AddListener(() => Timing.RunCoroutine(OnClickStart().CancelWith(gameObject)));
            Debug.Log("ABCD");
        }

        private IEnumerator<float> StartGame()
        {
            Tween.PositionX(_camera, 0, 2.5f);
            yield return Timing.WaitForSeconds(2f);
            Tween.Scale(_tile, 1, 0.2f);
            _start.gameObject.SetActive(true);
        }
        
        public void OnClickPlay()
        {
            SceneManager.LoadScene(1);
        }
        
        public void OnClickExit()
        {
            Application.Quit();
        }
        private IEnumerator<float> OnClickStart()
        {
            _start.gameObject.SetActive(false);   
            Tween.PositionX(transform,posTarget.position.x , 0.5f);
            yield return Timing.WaitForSeconds(0.5f);
            Tween.Scale(transform, endValue, 0.5f);
        }
    }
}
