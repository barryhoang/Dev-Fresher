using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatVariable _currentHealth;
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField] private ScriptableEventInt _onPlayerDamaged;
    [SerializeField] private ScriptableEventInt _onPlayerHealed;
    [SerializeField] private ScriptableEventNoParam _onPlayerDeath;

    private void Start()
    {
        _currentHealth.Value = _maxHealth;
        _currentHealth.OnValueChanged += OnHealthChanged;
        _currentHealth.MinMax = new Vector2(0,_maxHealth);
        _maxHealth.OnValueChanged += OnMaxHealthChanged;
    }
    
    private void OnDestroy()
    {
        _currentHealth.OnValueChanged -= OnHealthChanged;
        _maxHealth.OnValueChanged -= OnMaxHealthChanged;
    }
    
    private void OnMaxHealthChanged(float newValue)
    {
        _currentHealth.MinMax = new Vector2(0,newValue);
        var diff = newValue - _maxHealth.PreviousValue;
        _currentHealth.Add(diff);
    }

    private void OnHealthChanged(float newValue)
    {
        var diff = newValue - _currentHealth.PreviousValue;
        if (diff<0)
        {
            if(_currentHealth <=0)
            {
                _onPlayerDeath.Raise();
            }
            else
            {
                _onPlayerDamaged.Raise(Mathf.Abs(Mathf.RoundToInt(diff)));
            }
            
        }else {_onPlayerHealed.Raise(Mathf.RoundToInt((diff)));} 
    }

    public void TakeDamage(int damage)
    {
        _currentHealth.Add(-damage);
    }
}
