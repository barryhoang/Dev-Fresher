using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using MEC;

public class Hero : MonoBehaviour
{
    [SerializeField] private FloatVariable _heroHealth;
    [SerializeField] private FloatVariable _heroMaxHealth;
    [SerializeField] private FloatVariable _heroSpeed;

    [SerializeField] private ScriptableListHero _scriptableListHero;
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;

    [SerializeField] private ScriptableEventInt _onHeroDamaged;
    [SerializeField] private ScriptableEventInt _onHeroHeal;
    [SerializeField] private ScriptableEventNoParam _onHeroDeath;
    [SerializeField] private ScriptableEventNoParam _onHeroSpawn;
    
    private bool isMove;
    private void Start()
    {
        _onHeroSpawn.Raise();
        _scriptableListHero.Add(this);
        _heroHealth.Value = _heroMaxHealth;
        _heroHealth.OnValueChanged += OnHealthChanged;
        Timing.RunCoroutine(_Move());
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

    private IEnumerator<float> _Move()
    {
        yield return Timing.WaitForOneFrame;
        var closest = _scriptableListEnemy.GetClosest(transform.position);
        if (closest != null)
        {
            var distance = Vector2.Distance(transform.position, closest.transform.position);
            while (distance > 1f)
            {
                distance = Vector2.Distance(transform.position, closest.transform.position);
                var position = transform.position;
                var dir = closest.transform.position - position;
                position += _heroSpeed * dir.normalized * Time.deltaTime;
                transform.position = position;
                yield return Timing.WaitForOneFrame;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _heroHealth.Add(-damage);
    }
}
