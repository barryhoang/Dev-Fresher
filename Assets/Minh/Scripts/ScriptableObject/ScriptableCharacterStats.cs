using UnityEngine;
using Obvious.Soap;

namespace Minh
{
    [CreateAssetMenu(fileName = "New Stats", menuName = "ScriptableObject/Stats")]
    public class ScriptableCharacterStats : ScriptableObject
    {
        public IntVariable _attackRate;
        public IntVariable _maxHealth;
        public IntVariable _health;
        public IntVariable _speed;
        public IntVariable _damage;

        
    }
}