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
    void Start()
    {
        _currentHealth.Value = _maxHealth;
        _currentHealth.OnValueChanged += OnHealthChanged;
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
            //print("Damaged");
            
        }else {_onPlayerHealed.Raise(Mathf.RoundToInt((diff)));} 
        //print("Healed");
    }

    public void TakeDamage(int damage)
    {
        _currentHealth.Add(-damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
