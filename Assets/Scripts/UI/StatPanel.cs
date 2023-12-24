using Entity;
using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private TextMeshProUGUI _attackText;

        public void SetText(EntityData entityData)
        {
            _healthText.text = "Health: " + entityData.InitHealth.Value;
            _damageText.text = "Damage: " + entityData.InitDamage.Value;
            _attackText.text = "AttackSpeed: " + entityData.InitAttackSpeed.Value;
        }
    }
}
