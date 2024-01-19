using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class HealthViewer : MonoBehaviour
    {
        [SerializeField] private Image _fillHealth;
        [SerializeField] private FloatingDamage _vfxDamage;
        private float _maxHealth;
        

        public void SetMaxHealth(float maxHealth)
        {
            _fillHealth.fillAmount = 1f;
            _maxHealth = maxHealth;
        }
        
        public void UpdateHealthBar(float currentHealth)
        {
            var fillAmount = currentHealth / _maxHealth;
            _fillHealth.fillAmount = fillAmount;
        }

        public void VfxDamage(float damage)
        {
            _vfxDamage.SetText(damage);
            Instantiate(_vfxDamage, transform.position,Quaternion.identity);
        }
    }
}
