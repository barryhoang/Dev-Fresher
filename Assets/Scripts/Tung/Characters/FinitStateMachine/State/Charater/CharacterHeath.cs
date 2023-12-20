using System;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class CharacterHeath : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private FloatVariable _currentHealth;
        [SerializeField] private AnimationController _animationController;

        private void Start()
        {
            _currentHealth.Value = _characterData.InitialHealh;
            _currentHealth.MinMax = new Vector2(0, _characterData.InitialDamage);
            _currentHealth.OnValueChanged += OnCurrentHealthChange;
            _characterData.InitialHealh.OnValueChanged += OnMaxHealthChange;
        }
        private void OnMaxHealthChange(float newValue)
        {
            _currentHealth.MinMax = new Vector2(0, newValue);
            var diff = newValue - _characterData.InitialHealh.PreviousValue;
            _currentHealth.Add(diff);
        }

        private void OnCurrentHealthChange(float newValue)
        {
//             var diff = newValue - _currentHealth.PreviousValue;
//            if (diff < 0)
//            {
//                if (_currentHealth <= 0)
//                {
//                    _animationController.SetAnimator(NameAnimation.DEATH,true);
//                }
//                else
//                {
//                    _animationController.SetAnimator(NameAnimation.HURT,true);
//                }
//            }
//            else
//            {
//             
//            }
        }

        public void TakeDamage(FloatVariable damage)
        {
            _currentHealth.Add(-damage);
        }
        
        
    }
}
