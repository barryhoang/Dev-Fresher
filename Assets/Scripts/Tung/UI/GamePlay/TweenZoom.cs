using System.Collections.Generic;
using MEC;
using PrimeTween;
using UnityEngine;

namespace Tung
{
    public class TweenZoom : MonoBehaviour
    {
        public Vector3 scale;
        public float duration;
        private Tween _tween;
        private void OnDisable()
        {
            transform.localScale = new Vector3(0,0,0);
            _tween.Stop();
        }

        public IEnumerator<float> Zoom()
        {
            _tween = Tween.Scale(transform, scale, duration);
            yield return Timing.WaitForSeconds(duration);
        }
    }
}
