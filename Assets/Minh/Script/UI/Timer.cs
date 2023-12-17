using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace Minh
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private IntReference _levelTime;
        [SerializeField] private ScriptableEventNoParam _onLevelUp;
        private float _elapsedtime;

        public void Start()
        {
            Timing.RunCoroutine(StartTimer(), Segment.SlowUpdate, "startTimer");
        }

        public void OnEnable()
        {
            Timing.ResumeCoroutines("startTimer");
        }

        public void OnDisable()
        {
            Timing.PauseCoroutines("startTimer");
        }

        IEnumerator<float> StartTimer()
        {
            while (true)
            {
                if (gameObject != null && gameObject.activeInHierarchy)
                {
                    if (_elapsedtime < _levelTime)
                    {
                        _elapsedtime += Timing.DeltaTime;
                        var minutes = Mathf.FloorToInt(_elapsedtime / 60);
                        var seconds = Mathf.FloorToInt(_elapsedtime % 60);
                        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    }
                    else
                    {
                        _onLevelUp.Raise();
                    }

                    yield return 0f;
                }
            }
        }
    }
}