using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using MEC;
using Unity.VisualScripting;
using PrimeTween;
using Sequence = PrimeTween.Sequence;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private FloatVariable _enemyHealth;
    [SerializeField] private FloatVariable _enemyMaxHealth;
    [SerializeField] private FloatVariable _enemySpeed;
    
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private ScriptableListHero _scriptableListHero;

    [SerializeField] private ScriptableEventInt _onEnemyDamaged;
    [SerializeField] private ScriptableEventNoParam _onEnemyDeath;
    [SerializeField] private ScriptableEventNoParam _onEnemySpawn;

    public Animator Animator;
    public static Enemy Instance;
    private float _curTime = 0;
    private const float nextDamage = 1;

    private void Awake()
    {
        _onEnemySpawn.Raise();
        Instance = this;
        _scriptableListEnemy.Add(this);
        _enemyHealth.Value = _enemyMaxHealth;
        _enemyHealth.OnValueChanged += OnHealthChanged;
    }

    private void Update()
    {
        Move();
        Animator.SetFloat("HP",Mathf.Abs(_enemyHealth.Value));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onEnemyDamaged.Raise(0);
        }
    }
    
    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (_curTime <= 0 && other.CompareTag("Player"))
        {
            _onEnemyDamaged.Raise(0);
            _curTime = nextDamage;
        }
        else 
        {
            _curTime -= Time.deltaTime;
        }
    }*/

    private void OnHealthChanged(float value)
    {
        var diff = value - _enemyHealth;
        if (diff < 0)
        {
            if (_enemyHealth <= 0)
            {
                _onEnemyDeath.Raise();
            }
            else
            {
                _onEnemyDamaged.Raise(Mathf.Abs(Mathf.RoundToInt(diff)));
            }
        }
        /*else
        {
            _onEnemyHeal.Raise(Mathf.RoundToInt(diff));
        }*/
    }
    
    public void TakeDamamge(int damage)
    {
        _enemyHealth.Add(-damage);
    }
    
    public void Die()
    {
        Destroy(gameObject);
        _scriptableListEnemy.Remove(this);
    }

    public void TweenAttack()
    {
        Vector3 _initialPos = transform.position;
        Vector3 _targetPos = new Vector3((_initialPos.x)+1f,0f,0f);
        float endValue = _targetPos.x - 1f;
        float duration = 0.5f;
        Sequence.Create(cycles: 10, CycleMode.Yoyo).Chain(Tween.PositionX(transform, endValue, duration));
    }
    
    public void Move()
    {
        if(gameObject != null)
        {
            var closest = _scriptableListHero.GetClosest(transform.position);
            if (closest != null)
            {
                var distance = (transform.position - closest.transform.position).sqrMagnitude;
                if (distance > 1f)
                {
                    Animator.SetBool("isMoving",true);
                    var newPos = transform.position;
                    newPos =  closest.transform.position - transform.position;
                    transform.position += newPos.normalized *_enemySpeed * Time.deltaTime;
                }

                if (distance <= 1f)
                {
                    Animator.SetBool("isMoving",false);
                }
            }
        }
    }
 }
