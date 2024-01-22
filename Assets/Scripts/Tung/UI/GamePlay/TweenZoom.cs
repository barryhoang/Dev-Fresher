using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class TweenZoom : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public Vector3 scale;
        public float duration;

        private void OnEnable()
        {
            Timing.RunCoroutine(Zoom().CancelWith(gameObject));
        }
        private void OnDisable()
        {
            _button.gameObject.SetActive(false);
        }

        public IEnumerator<float> Zoom()
        {
            transform.localScale = new Vector3(0, 0, 0);
            Tween.Scale(transform, scale, duration);
            yield return Timing.WaitForSeconds(duration);
            _button.gameObject.SetActive(true);
        }
    }
}
