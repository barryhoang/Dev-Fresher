using UnityEngine;
using Obvious.Soap;

namespace Minh
{
    [CreateAssetMenu(fileName = "New Stats", menuName = "ScriptableObject/Stats")]
    public class ScriptableCharacterStats : ScriptableObject
    {
        public IntReference _maxHealth;
        public IntReference _health;
        public IntReference _speed;
        public IntReference _damage;

        
    }
}