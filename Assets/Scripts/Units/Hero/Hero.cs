using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Obvious.Soap;
using UnityEngine;
using MEC;
using PrimeTween;

public class Hero : MonoBehaviour
{
    [SerializeField] private FloatVariable _heroHealth;
    [SerializeField] private FloatVariable _heroMaxHealth;
    [SerializeField] private FloatVariable _heroSpeed;

    [SerializeField] private ScriptableListHero _scriptableListHero;
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    
    [SerializeField] private ScriptableEventInt _onHeroDamaged;
    [SerializeField] private ScriptableEventNoParam _onHeroHittingEnemy;
    [SerializeField] private ScriptableEventNoParam _onHeroDeath;
    [SerializeField] private ScriptableEventNoParam _onHeroSpawn;
    
    public Animator Animator;
    public static Hero Instance;

    private void Awake()
    {
        Instance = this;
        _onHeroSpawn.Raise();
        _scriptableListHero.Add(this);
        _heroHealth.Value = _heroMaxHealth;
        _heroHealth.OnValueChanged += OnHealthChanged;
    }

    private void Start()
    {
        Timing.RunCoroutine(_Move().CancelWith(gameObject));
        Timing.RunCoroutine(_Col().CancelWith(gameObject));
    }

    private void Update()
    {
        Animator.SetFloat("HP", Mathf.Abs(_heroHealth.Value));
        if (GameManager.Instance.gameState == GameState.HittingPhase)
        {
            _onHeroHittingEnemy.Raise();
        }
    }

    
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.gameState = GameState.HittingPhase;
            Debug.Log("Collide");
            _onHeroDamaged.Raise(0);
        }
    }*/
    
    private void OnDestroy()
    {
        _heroHealth.OnValueChanged -= OnHealthChanged;
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
        /*else
        {
                _onHeroHeal.Raise(Mathf.RoundToInt(diff));
        }*/
    }
    
    public void TakeDamamge(int damage)
    {
        _heroHealth.Add(-damage);
    }

    public void Die()
    {
        Destroy(gameObject);
        _scriptableListHero.Remove(this);
    }
    
    public void TweenAttack()
         {
             Vector3 _initialPos = transform.position;
             Vector3 _targetPos = new Vector3((_initialPos.x)+1f,0f,0f);
             float endValue = _targetPos.x - 1f;
             float duration = 1f;
             Sequence.Create(cycles: 10, CycleMode.Yoyo).Chain(Tween.PositionX(transform, endValue, duration));
         }
    
    private IEnumerator<float> _Move()
    {
        if(gameObject != null)
        {
            yield return Timing.WaitForOneFrame;
            var closest = _scriptableListEnemy.GetClosest(transform.position);
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
                    position += _heroSpeed * dir.normalized * Time.deltaTime;
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
    }

    private IEnumerator<float> _Col()
    {
        Collider[] col = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        int i = 0;
        while (i < col.Length)
        { 
            _onHeroDamaged.Raise(0); 
            i++;
            yield return Timing.WaitForOneFrame;
        }
    }
}
/*public void DetectE()
    {
        Collider[] col = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        int i = 0;
        while (i < col.Length)
        {
            _onHeroDamaged.Raise(0);
            i++;
        }
    }*/
/*private void testingTween()
{
    GameObject objToAnimate = gameObject;

    // Set the initial position (optional)
    objToAnimate.transform.position = new Vector3(0f, 0f, 0f);

    // Set the target position
    Vector3 targetPosition = new Vector3(5f, 2f, 0f);

    // Set the duration of the tween animation
    float duration = 1.5f;

    // Use DOTween to move the object to the target position over the specified duration
    objToAnimate.transform.DOMove(targetPosition, duration)
        .SetEase(Ease.OutQuad) // Set the ease type (optional)
        .OnComplete(AnimationComplete);
}*/
