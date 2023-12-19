using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    [UnityEngine.CreateAssetMenu(fileName = "CharacterData", menuName = "Character", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private FloatVariable _initialHealh;
        [SerializeField] private FloatVariable _initialDamage;
        [SerializeField] private FloatVariable _attackSpeed;
        [SerializeField] private FloatVariable _armor;

        public FloatVariable InitialHealh
        {
            get => _initialHealh;
            set => _initialHealh = value;
        }

        public FloatVariable InitialDamage
        {
            get => _initialDamage;
            set => _initialDamage = value;
        }

        public FloatVariable AttackSpeed
        {
            get => _attackSpeed;
            set => _attackSpeed = value;
        }

        public FloatVariable Armor
        {
            get => _armor;
            set => _armor = value;
        }
    }
}