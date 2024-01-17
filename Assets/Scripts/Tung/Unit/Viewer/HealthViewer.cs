using UnityEngine;

namespace Tung
{
    public class HealthViewer : MonoBehaviour
    {
        public Transform currentHealthViewer;
        public Transform fillHealthViewer;
        private float _maxHealth;

        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
        }
        
        public void UpdateHealthBar(float currentHealth)
        {
            var fillAmount = currentHealth / _maxHealth;
            var maxFillAmount = currentHealthViewer.transform.localScale.x;
            fillHealthViewer.transform.localScale =  new Vector3(fillAmount / maxFillAmount,0.07f,1f);
        }
    }
}
