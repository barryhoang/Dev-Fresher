using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class HealthViewer : MonoBehaviour
        //HealthViewer: Hiển thị thanh máu, 1P-4P theo input là hero hay enemy và % máu hiện tại theo event.
    {
        [SerializeField] private Image fillHealth;
        private float _maxHealth;
        
        public void SetMaxHealth(float maxHealth)
        {
            fillHealth.fillAmount = 1f;
            _maxHealth = maxHealth;
        }

        public void UpdateHealthBar(float currentHealth)
        {
            var fillAmount = currentHealth / _maxHealth;
            fillHealth.fillAmount = fillAmount;
        }
    }
}
