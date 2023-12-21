
using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public FloatVariable floatVariable;
        public FloatReference maxValue;

        private void Start()
        {
            floatVariable.OnValueChanged += Refresh;
        }

        private void OnDestroy()
        {
            floatVariable.OnValueChanged -= Refresh;
        }

        private void Refresh(float currentValue)
        {
            _image.fillAmount = floatVariable.Value / maxValue.Value;
        }
    }
}
