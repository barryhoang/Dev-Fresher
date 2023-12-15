using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obvious.Soap.Example
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeOut : CacheComponent<CanvasGroup>
    {
        [SerializeField] private float _duration = 0.5f;

        private Coroutine _coroutine = null;

        public void Activate()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Cr_FadeOut());
        }

        public void ActivateManyTime()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Cr_FadeOut3Time());
        }

        private IEnumerator Cr_FadeOut()
        {
            var timer = 0f;
            _component.alpha = 1f;
            while (timer <= _duration)
            {
                _component.alpha = Mathf.Lerp(1f, 0f, timer / _duration);
                timer += Time.deltaTime;
                yield return null;
            }

            _component.alpha = 0;
        }

        private IEnumerator Cr_FadeOut3Time()
        {
            for (int i = 0; i < 2; i++)
            {
                var timer = 0f;
                _component.alpha = 1f;
                while (timer <= _duration)
                {
                    _component.alpha = Mathf.Lerp(1f, 0f, timer / _duration);
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
           

            _component.alpha = 0;
        }
    }
}