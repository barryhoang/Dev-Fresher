
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using System.Collections.Generic;

namespace Tung
{
    public class FinishRoundViewer : MonoBehaviour
    {
        [SerializeField] private ScriptableEventNoParam _onWin;
        [SerializeField] private ScriptableEventNoParam _onReset;
        [SerializeField] private ScriptableEventNoParam _onLose;
        [SerializeField] private ScriptableEventNoParam _clickWin;
        [SerializeField] private ScriptableEventNoParam _clickReset;
        [SerializeField] private ScriptableEventNoParam _clickLose;
        [SerializeField] private Button _buttonWin;
        [SerializeField] private Button _buttonLose;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private TweenZoom _panelWin;
        [SerializeField] private TweenZoom _panelReset;
        [SerializeField] private TweenZoom _panelLose;
        [SerializeField] private AniImage _aniImage;

        private void Start()
        {
            _buttonLose.onClick.AddListener(ClickLose);
            _buttonWin.onClick.AddListener(ClickWin);
            _buttonReset.onClick.AddListener(ClickReset);
            _onWin.OnRaised += RunWin;
            _onReset.OnRaised += RunReset;
            _onLose.OnRaised += RunLose;
        }

        private void OnDisable()
        {
            _onWin.OnRaised -= RunWin;
            _onReset.OnRaised -= RunReset;
            _onLose.OnRaised -= RunLose;
        }
        private void ClickWin() => Timing.RunCoroutine(TimingAniWin().CancelWith(gameObject));
        private void ClickReset() => Timing.RunCoroutine(TimingAniReset().CancelWith(gameObject));
        private void ClickLose() => _clickLose.Raise();
        private void RunWin() => _panelWin.gameObject.SetActive(true);
        public void RunReset() => _panelReset.gameObject.SetActive(true);
        private void RunLose() => _panelLose.gameObject.SetActive(true);
        private IEnumerator<float> TimingAniLose()
        {
            _panelLose.gameObject.SetActive(false);
            _aniImage.gameObject.SetActive(true);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_aniImage.IncreaseFillAmount().CancelWith(gameObject)));

            yield return Timing.WaitForSeconds(0.2f);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_aniImage.DecreaseFillAmount().CancelWith(gameObject)));
            _aniImage.gameObject.SetActive(false);
        }
        private IEnumerator<float> TimingAniReset()
        {
            _panelReset.gameObject.SetActive(false);
            _aniImage.gameObject.SetActive(true);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_aniImage.IncreaseFillAmount().CancelWith(gameObject)));
            _clickReset.Raise();
            yield return Timing.WaitForSeconds(0.2f);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_aniImage.DecreaseFillAmount().CancelWith(gameObject)));
            _aniImage.gameObject.SetActive(false);
        }
        private IEnumerator<float> TimingAniWin()
        {
            _panelWin.gameObject.SetActive(false);
            _aniImage.gameObject.SetActive(true);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_aniImage.IncreaseFillAmount().CancelWith(gameObject)));
            _clickWin.Raise();
            yield return Timing.WaitForSeconds(0.2f);
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_aniImage.DecreaseFillAmount().CancelWith(gameObject)));
            _aniImage.gameObject.SetActive(false);
        }


    }
}


