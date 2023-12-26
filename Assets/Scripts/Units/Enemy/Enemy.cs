using UnityEngine;
using Obvious.Soap;
using PrimeTween;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private float _curTime = 0;
    [SerializeField] private float _receivedDmgDelay = 1;
    
    [SerializeField] private FloatVariable _enemyHealth;
    [SerializeField] private FloatVariable _enemyMaxHealth;
    [SerializeField] private FloatVariable _enemySpeed;
    
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private ScriptableListHero _scriptableListHero;

    [SerializeField] private ScriptableEventInt _onEnemyDamaged;
    [SerializeField] private ScriptableEventNoParam _onEnemySpawn;
    
    [SerializeField] private Animator Animator;


    private void Awake()
    {
        _onEnemySpawn.Raise();
        _scriptableListEnemy.Add(this);
        _enemyHealth.Value = _enemyMaxHealth;
    }
    private void Update()
    {
        Animator.SetFloat("HP",Mathf.Abs(_enemyHealth.Value));
        if (_enemyHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_curTime <= 0 && other.CompareTag("Player"))
        {
            _onEnemyDamaged.Raise(0);
            _curTime = _receivedDmgDelay;
        }
        else 
        {
            _curTime -= Time.deltaTime;
        }
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
                    //TweenAttack();
                }
            }
        }
    }

    private void TweenAttack()
    {
        Vector3 _initialPos = transform.position;
        Tween.PositionX(transform, (_initialPos.x), 1, Ease.Default, 2, CycleMode.Yoyo);
    }
 }
