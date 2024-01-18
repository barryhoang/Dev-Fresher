using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using MEC;
using PrimeTween;
using TMPro;
using UnityEngine;

namespace Tung
{
    public class FloatingDamage : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMeshPro;
        public float distance;
        public float time;
        public Vector2 scale;
        private void Start()
        {
            Timing.RunCoroutine(StartInstance().CancelWith(gameObject));
        }

        public void SetText(float damage)
        {
            _textMeshPro.text = damage.ToString(CultureInfo.InvariantCulture);
        }
        
        private IEnumerator<float> StartInstance()
        {
            // Tween.Scale(transform, new Vector3(1.5f,1.5f,0), 0.2f);
            // yield return Timing.WaitForSeconds(0.2f);
            Tween.PositionY(transform, transform.position.y + distance, time);
            Tween.Scale(transform, scale, time);
            yield return Timing.WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
