using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;
using Random = System.Random;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _curTime = 0;
    [SerializeField] private float _receivedDmgDelay = 2;
    
    [SerializeField] private FloatVariable _heroHealth;
    [SerializeField] private FloatVariable _heroMaxHealth;
    [SerializeField] private FloatVariable _heroSpeed;
    
    [SerializeField] private ScriptableListHero _scriptableListHero;
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    
    [SerializeField] private ScriptableEventInt _onHeroDamaged;
    [SerializeField] private ScriptableEventNoParam _onHeroSpawn;

    [SerializeField] private Animator Animator;

    [SerializeField] private Rigidbody2D selectedObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 mousePos;
    
    
    
    private void Awake()
    {
        _onHeroSpawn.Raise();
        _scriptableListHero.Add(this);
        _heroHealth.Value = _heroMaxHealth;
    }
    private void Update()
    {
        Animator.SetFloat("HP", Mathf.Abs(_heroHealth.Value));
        if (_heroHealth <= 0)
        {
            Die();
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePos;
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
    void FixedUpdate()
    {
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePos + offset);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_curTime <= 0 && other.CompareTag("Enemy"))
        {
            _onHeroDamaged.Raise(0);
            _curTime = _receivedDmgDelay;
        }
        else 
        {
            _curTime -= Time.deltaTime;
        }
    }
    
    public void TakeDamage(int damage)
    {
        _heroHealth.Add(-damage);
    }
    public void Die()
    {
        Destroy(gameObject);
        _scriptableListHero.Remove(this);
    }
    public void Move()
    {
        if(gameObject != null)
        {
            var closest = _scriptableListEnemy.GetClosest(transform.position);
            if (closest != null)
            {
                var distance = (transform.position - closest.transform.position).sqrMagnitude;
                if (distance > 1f)
                {
                    Animator.SetBool("isMoving",true);
                    var newPos = transform.position;
                    newPos =  closest.transform.position - transform.position;
                    transform.position += newPos.normalized *_heroSpeed * Time.deltaTime;
                }

                if (distance <= 1f)
                {
                    Animator.SetBool("isMoving",false);
                    //Timing.RunCoroutine(_TweenAttack().CancelWith(closest.gameObject));
                }
            }
        }
    }
    
    private IEnumerator<float> _TweenAttack()
    {
        while (true)
        {
            Tween.PositionX(transform, 1, 1, Ease.Default, 2, CycleMode.Yoyo);
            //Tween.PositionX(transform, -3f, 1, Ease.Default, 2, CycleMode.Yoyo);
            yield return Timing.WaitForOneFrame;
        }
    }
}

