using UnityEngine;
using Obvious.Soap;
using PrimeTween;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float curTime = 0;
    [SerializeField] private float receivedDmgDelay = 1;
    
    [SerializeField] private FloatVariable enemyHealth;
    [SerializeField] private FloatVariable enemyMaxHealth;
    [SerializeField] private FloatVariable enemySpeed;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private ScriptableEventInt onEnemyDamaged;
    
    [SerializeField] private EnemyStateMachines ESM;
    [SerializeField] private Animator animator;
    
    private int Hp = Animator.StringToHash("HP");
    private int IsMoving = Animator.StringToHash("isMoving");
    
    
    private void Awake()
    {
        scriptableListEnemy.Add(this);
        enemyHealth.Value = enemyMaxHealth;
        animator.SetFloat(Hp,Mathf.Abs(enemyHealth.Value));
    }
    
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            animator.SetFloat(Hp, 0);
            Die();
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (curTime <= 0 && other.CompareTag("Player"))
        {
            onEnemyDamaged.Raise(0);
            curTime = receivedDmgDelay;
            TweenAttack();
        }
        else 
        {
            curTime -= Time.deltaTime;
        }
    }
    

    public void TakeDamamge(int damage)
    {
        enemyHealth.Add(-damage);
    }
    
    public void Die()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        scriptableListEnemy.Remove(this);
        ESM.currentState = EnemyStateMachines.TurnState.DEAD;
    }
    
    public void Move()
    {
        if(gameObject != null)
        {
            var closest = scriptableListHero.GetClosest(transform.position);
            if (closest != null)
            {
                var distance = (transform.position - closest.transform.position).sqrMagnitude;
                if (distance > 1f)
                {
                    animator.SetBool(IsMoving,true);
                    var newPos = transform.position;
                    newPos =  closest.transform.position - transform.position;
                    transform.position += newPos.normalized *enemySpeed * Time.deltaTime;
                }

                if (distance <= 1f)
                {
                    animator.SetBool(IsMoving,false);
                }
            }
        }
    }

    public void TweenAttack()
    {
        Tween.PositionX(transform, transform.position.x-0.3f, 0.5f, Ease.Default, 2, CycleMode.Yoyo);
    }
 }
