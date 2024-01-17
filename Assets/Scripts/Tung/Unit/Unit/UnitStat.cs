using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [CreateAssetMenu]
    public class UnitStat : ScriptableObject
    {
        [SerializeField] private FloatVariable _maxHealth;
        [SerializeField] private FloatVariable _currentHealth;
        [SerializeField] private FloatVariable _initDamage;
        [SerializeField] private FloatVariable _initSpeed;
        [SerializeField] private FloatVariable attackSpeed;

        public FloatVariable MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public FloatVariable Damage
        {
            get => _initDamage;
            set => _initDamage = value;
        }

        public FloatVariable Speed
        {
            get => _initSpeed;
            set => _initSpeed = value;
        }

        public FloatVariable AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed = value;
        }
    }
}
