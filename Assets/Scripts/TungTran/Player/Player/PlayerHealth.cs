using Obvious.Soap;
using TungTran.Enemy;
using UnityEngine;

namespace TungTran.Player.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private FloatVariable _currentHealth;
        [SerializeField] private TransformVariable _player;
        [SerializeField] private ScriptableEventInt _onHitPlayer;
        [SerializeField] private ScriptableEventInt _onPlayerHealthing;
        [SerializeField] private ScriptableEventNoParam _onPlayerDeath;
    

        private void Start()
        {
            _player.Value = transform;
            _currentHealth.Value = _playerData.InitiateHealth;
            _currentHealth.OnValueChanged += OnHealthChanged;
            _currentHealth.MinMax = new Vector2(0, _playerData.InitiateHealth);
            _playerData.InitiateHealth.OnValueChanged += OnMaxHealthChange;
        }

        private void OnMaxHealthChange(float value)
        {
            _playerData.InitiateHealth.Value = Mathf.RoundToInt(_playerData.InitiateHealth.Value);
            _currentHealth.MinMax = new Vector2(0,_playerData.InitiateHealth.Value);
            var diff = value - _playerData.InitiateHealth.PreviousValue;
            _currentHealth.Add(diff);
        }
    
        private void OnDestroy()
        {
            _currentHealth.OnValueChanged -= OnHealthChanged;
            _currentHealth.OnValueChanged -= OnMaxHealthChange;
        }

        public void OnHealthChanged(float value)
        {
            var temp = value - _currentHealth.PreviousValue;
            if (temp < 0)
            {
                if (_currentHealth <= 0)
                {
                    Time.timeScale = 0f;
                    _onPlayerDeath.Raise();
                }
                else
                {
                    _onHitPlayer.Raise(Mathf.Abs(Mathf.RoundToInt(temp))); 
                }
            }
            else
            {
                _onPlayerHealthing.Raise(Mathf.Abs(Mathf.RoundToInt(temp)));
            }
        }

        public void ResetHealthPlayer()
        {
            _currentHealth.Value = _playerData.InitiateHealth.Value;
        }
        
        public void TakeDame(float damage)
        {
            _currentHealth.Add(-damage);
        }
    }
}
