using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private FloatVariable _heroHealth;
    [SerializeField] private FloatVariable _heroMaxHealth;

    [SerializeField] private ScriptableListHero _scriptableListHero;

    [SerializeField] private ScriptableEventInt _onHeroDamaged;
    [SerializeField] private ScriptableEventInt _onHeroHeal;
    [SerializeField] private ScriptableEventNoParam _onHeroDeath;
    [SerializeField] private ScriptableEventNoParam _onHeroSpawn;
    
    private void Start()
    {
        _onHeroSpawn.Raise();
        _scriptableListHero.Add(this);
        _heroHealth.Value = _heroMaxHealth;
        _heroHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        var diff = value - _heroHealth;
        if (diff < 0)
        {
            if (_heroHealth <= 0)
            {
                _onHeroDeath.Raise();
            }
            else
            {
                _onHeroDamaged.Raise(Mathf.Abs(Mathf.RoundToInt(diff)));
            }
        }
        else
        {
                _onHeroHeal.Raise(Mathf.RoundToInt(diff));
        }
    }
}
