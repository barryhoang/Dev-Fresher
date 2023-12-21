using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using MEC;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private FloatVariable _enemyHealth;
    [SerializeField] private FloatVariable _enemyMaxHealth;
    [SerializeField] private FloatVariable _enemySpeed;
    
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private ScriptableListHero _scriptableListHero;

    [SerializeField] private ScriptableEventInt _onCollide;
    [SerializeField] private ScriptableEventInt _onEnemyDamaged;
    [SerializeField] private ScriptableEventInt _onEnemyHeal;
    [SerializeField] private ScriptableEventNoParam _onEnemyDeath;
    [SerializeField] private ScriptableEventNoParam _onEnemySpawn;

    public Animator Animator;

    private void Start()
    {
        _onEnemySpawn.Raise();
        _scriptableListEnemy.Add(this);
        _enemyHealth.Value = _enemyMaxHealth;
        _enemyHealth.OnValueChanged += OnHealthChanged;
        Timing.RunCoroutine(_Move().CancelWith(gameObject));
    }
    
    private void Update()
    {
        Animator.SetFloat("HP",Mathf.Abs(_enemyHealth.Value));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onCollide.Raise(0);
        }
    }

    private void OnDestroy()
    {
        _enemyHealth.OnValueChanged -= OnHealthChanged;
    }
    
    private void Die()
    {
        Destroy(this.gameObject);
    }
    
    public void TakeDamamge(int damage)
    {
        _enemyHealth.Add(-damage);
    }

    private void OnHealthChanged(float value)
    {
        var diff = value - _enemyHealth;
        if (diff >= 0)
        {
            if (_enemyHealth <= 0)
            {
                _onEnemyDeath.Raise();
                Die();
            }
            else
            {
                _onEnemyDamaged.Raise(Mathf.Abs(Mathf.RoundToInt(diff)));
            }
        }
        else
        {
            _onEnemyHeal.Raise(Mathf.RoundToInt(diff));
        }
    }
    
    public void IsNotMoving()
    {
        Animator.SetBool("isMoving",false);
    }
    
    private IEnumerator<float> _Move()
    {
        yield return Timing.WaitForOneFrame;
        var closest = _scriptableListHero.GetClosest(transform.position);
        if (closest != null)
        {
            var distance = Vector2.Distance(transform.position, closest.transform.position);
            while (distance > 1f)
            {
                distance = Vector2.Distance(transform.position, closest.transform.position);
                var position = transform.position;
                var dir = closest.transform.position - position;
                position += _enemySpeed * dir.normalized * Time.deltaTime;
                transform.position = position;
                yield return Timing.WaitForOneFrame;
            }
        }
    }
 }