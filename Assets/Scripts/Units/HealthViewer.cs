using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class HealthViewer : MonoBehaviour
    {
        [SerializeField] private Image fillHp;
        private float _maxHp;

        public void SetMaxHp(float maxHp)
        {
            fillHp.fillAmount = 1f;
            _maxHp = maxHp;
        }

        public void UpdateHp(float currentHp)
        {
            var fillAmount = currentHp / _maxHp;
            fillHp.fillAmount = fillAmount;
        }
    }
}
