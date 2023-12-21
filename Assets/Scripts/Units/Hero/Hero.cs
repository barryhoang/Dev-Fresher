using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    
    [SerializeField] private ScriptableEventInt _onCollide;
    [SerializeField] private ScriptableEventInt _onHeroDamaged;
    [SerializeField] private ScriptableEventInt _onHeroHeal;
    [SerializeField] private ScriptableEventNoParam _onHeroDeath;
    [SerializeField] private ScriptableEventNoParam _onHeroSpawn;
    [SerializeField] private ScriptableEventNoParam _onHitting;
    
    public Animator Animator;
    private void Start()
    {
        _onHeroSpawn.Raise();
        _scriptableListHero.Add(this);
        _heroHealth.Value = _heroMaxHealth;
        _heroHealth.OnValueChanged += OnHealthChanged;
        Timing.RunCoroutine(_Move().CancelWith(gameObject));
    }

    private void Update()
    {
        Animator.SetFloat("HP",Mathf.Abs(_heroHealth.Value));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _onCollide.Raise(0);
        }
    }
    
    private void OnDestroy()
    {
        _heroHealth.OnValueChanged -= OnHealthChanged;
    }
    
    private void Die()
    {
        Destroy(this.gameObject);
    }
    
    private void OnHealthChanged(float value)
    {
        var diff = value - _heroHealth;
        if (diff >= 0)
        {
            if (_heroHealth <= 0)
            {
                _onHeroDeath.Raise();
                Die();
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
    
    public void TakeDamamge(int damage)
    {
        _heroHealth.Add(-damage);
    }
    
    public void IsNotMoving()
    {
        Animator.SetBool("isMoving",false);
    }
    
    private IEnumerator<float> _Move()
    {
        if(gameObject != null && gameObject.activeInHierarchy)
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

                while (distance <= 1f)
                {
                    _onHitting.Raise();
                }
            }
        }
    }
}

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
