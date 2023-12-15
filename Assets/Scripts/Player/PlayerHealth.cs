
using System;
using Obvious.Soap;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatVariable _currentHealth;
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField] private TransformVariable _player;
    [SerializeField] private ScriptableEventInt _onHitPlayer;
    [SerializeField] private ScriptableEventInt _onPlayerHealthing;
    [SerializeField] private ScriptableEventNoParam _onPlayerDeath;
    
    private void Start()
    {
        _player.Value = transform;
        _currentHealth.Value = _maxHealth;
        _currentHealth.OnValueChanged += OnHealthChanged;
        _currentHealth.MinMax = new Vector2(0, _maxHealth);
        _maxHealth.OnValueChanged += OnMaxHealthChange;
    }

    private void OnMaxHealthChange(float value)
    {
        _maxHealth.Value = Mathf.RoundToInt(_maxHealth.Value);
        _currentHealth.MinMax = new Vector2(0,_maxHealth.Value);
        var diff = value - _maxHealth.PreviousValue;
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
    
    public void TakeDame(float Damage)
    {
        _currentHealth.Add(-Damage);
    }
}
