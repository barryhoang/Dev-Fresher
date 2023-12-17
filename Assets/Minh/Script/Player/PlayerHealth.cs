using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private FloatVariable _currentHealth;
        [SerializeField] private FloatVariable _maxHealth;
        [SerializeField] private IntVariable _reviveTime;

        [SerializeField] private ScriptableEventInt _onPlayerDamaged;
        [SerializeField] private ScriptableEventInt _onPlayerHealed;
        [SerializeField] private ScriptableEventNoParam _onPlayerDeath;
        [SerializeField] private ScriptableEventNoParam _onPlayerRevive;

        void Start()
        {
            _currentHealth.Value = _maxHealth;
            _currentHealth.OnValueChanged += OnHealthChaged;
            _currentHealth.MinMax = new Vector2(0, _maxHealth);
            _maxHealth.OnValueChanged += OnMaxHealthChanged;
        }

        private void OnMaxHealthChanged(float newvalue)
        {
            _currentHealth.MinMax = new Vector2(0, newvalue);
            var diff = newvalue - _maxHealth.PreviousValue;
            _currentHealth.Add(diff);
        }

        private void OnDestroy()
        {
            _currentHealth.OnValueChanged -= OnHealthChaged;
            _maxHealth.OnValueChanged -= OnMaxHealthChanged;
        }

        private void OnHealthChaged(float newValue)
        {
            var diff = newValue - _currentHealth.PreviousValue;
            if (diff < 0)
            {
                if (_currentHealth <= 0)
                {
                    _onPlayerDeath.Raise();
                    ;
                }
                else
                {
                    _onPlayerDamaged.Raise(Mathf.Abs(Mathf.RoundToInt(diff)));
                }

                print("Damaged");
            }
            else
            {
                _onPlayerHealed.Raise(Mathf.RoundToInt(diff));
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth.Add(-damage);
        }

        public void PlayerRevive()
        {
            _currentHealth.Value = _maxHealth;
        }
        // Update is called once per frame
        void Update()
        {
        }
    }
}