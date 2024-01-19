using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tung
{
    public class GamePlayUi : MonoBehaviour
    {
        [SerializeField] private TweenZoom _win;
        [SerializeField] private TweenZoom _lose;
        [SerializeField] private Button _buttonWin;
        [SerializeField] private Button _buttonLose;
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventNoParam _onLose;
        private Tween _tween;
        
        private void Start()
        {
            _buttonWin.onClick.AddListener(NextLevel);
            _buttonLose.onClick.AddListener(Exit);
            _onWin.OnRaised += RunWin;
            _onLose.OnRaised += RunLose;
        }

        private void OnDisable()
        {
            
            _onWin.OnRaised -= RunWin;
            _onLose.OnRaised -= RunLose;
        }

        private void Exit() => SceneManager.LoadScene(0);
        private void NextLevel() => Debug.Log("Next Level");
        private void RunWin() =>Timing.RunCoroutine(OnWin().CancelWith(gameObject));
        private void RunLose() => Timing.RunCoroutine(OnLose().CancelWith(gameObject));

        private IEnumerator<float> OnWin()
        {
            _win.gameObject.SetActive(true);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_win.Zoom().CancelWith(gameObject)));
            _buttonWin.gameObject.SetActive(true);
        }

        private  IEnumerator<float>  OnLose()
        {
            _lose.gameObject.SetActive(true);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_lose.Zoom().CancelWith(gameObject)));
            _buttonLose.gameObject.SetActive(true);
        }
    }
}
