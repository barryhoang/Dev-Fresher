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
    [SerializeField] private ScriptableEventNoParam _onEnemyHittingHero;
    [SerializeField] private ScriptableEventNoParam _onEnemyDeath;
    [SerializeField] private ScriptableEventNoParam _onEnemySpawn;

    public Animator Animator;
    public static Enemy Instance;

    private void Awake()
    {
        _onEnemySpawn.Raise();
        Instance = this;
        _scriptableListEnemy.Add(this);
        _enemyHealth.Value = _enemyMaxHealth;
        _enemyHealth.OnValueChanged += OnHealthChanged;
    }

    private void Start()
    {
        Timing.RunCoroutine(_Move().CancelWith(gameObject));
        Timing.RunCoroutine(_Col().CancelWith(gameObject));
    }
    
    private void Update()
    {
        Animator.SetFloat("HP",Mathf.Abs(_enemyHealth.Value));
        if (GameManager.Instance.gameState == GameState.HittingPhase)
        {
            _onEnemyHittingHero.Raise();
        }
    }
    
    
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.gameState = GameState.HittingPhase;
            Debug.Log("Collide");
            _onEnemyDamaged.Raise(0);
        }
    }*/

    private void OnDestroy()
    {
        _enemyHealth.OnValueChanged -= OnHealthChanged;
    }

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
        //Tween.PositionX(transform, endValue, duration, Ease.InOutSine);
        Sequence.Create(cycles: 10, CycleMode.Yoyo).Chain(Tween.PositionX(transform, endValue, duration));
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
                Animator.SetBool("isMoving",true);
                GameManager.Instance.gameState = GameState.MovingPhase;
                distance = Vector2.Distance(transform.position, closest.transform.position);
                var position = transform.position;
                var dir = closest.transform.position - position;
                position += _enemySpeed * dir.normalized * Time.deltaTime;
                transform.position = position;
                yield return Timing.WaitForOneFrame;
            }
            while (distance <= 1f)
            {
                //GameManager.Instance.gameState = GameState.HittingPhase;
                Animator.SetBool("isMoving",false);
            }
        }
    }
    
    private IEnumerator<float> _Col()
    {
        Collider[] col = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        int i = 0;
        while (i < col.Length)
        { 
            _onEnemyDamaged.Raise(0); 
            i++;
            yield return Timing.WaitForOneFrame;
        }
    }
 }
