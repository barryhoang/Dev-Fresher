using Obvious.Soap;
using UnityEngine;

namespace Entity
{
    [CreateAssetMenu]
    public class EntityData :ScriptableObject
    {
        [SerializeField] private FloatVariable _initHealth;
        [SerializeField] private FloatVariable _initDamage;
        [SerializeField] private FloatVariable _initAttackSpeed;

        public FloatVariable InitHealth
        {
            get => _initHealth;
            set => _initHealth = value;
        }

        public FloatVariable InitDamage
        {
            get => _initDamage;
            set => _initDamage = value;
        }

        public FloatVariable InitAttackSpeed
        {
            get => _initAttackSpeed;
            set => _initAttackSpeed = value;
        }
    }
}
