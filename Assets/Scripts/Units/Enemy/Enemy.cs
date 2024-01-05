using System.Collections.Generic;
using MEC;
using UnityEngine;
using Obvious.Soap;
using PrimeTween;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float curTime;
    [SerializeField] private float receivedDmgDelay = 1;
    [SerializeField] private FloatVariable enemyHealth;
    [SerializeField] private FloatVariable enemyMaxHealth;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableListGameObject listEnemy;
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private ScriptableEventInt onEnemyDamaged;
    [SerializeField] private EnemyStateMachines ESM;
    [SerializeField] private Animator animator;
    [SerializeField] private Pathfinding pathfinding;

    public List<PathNode> temp;
    private int currentX;
    private int currentY;
    private int targetPosX;
    private int targetPosY;
    
    private static readonly int Hp = Animator.StringToHash("HP");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    
    
    private void Awake()
    {
        scriptableListEnemy.Add(this);
        listEnemy.Add(gameObject);
        enemyHealth.Value = enemyMaxHealth;
        animator.SetFloat(Hp,Mathf.Abs(enemyHealth.Value));
    }

    private void Start()
    {
        Timing.RunCoroutine(TweenMove().CancelWith(gameObject));
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
            Timing.RunCoroutine(TweenAttack().CancelWith(gameObject));
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
    
    IEnumerator<float> TweenMove()
    {
        while (true)
        {
            if (gameObject != null && ESM.currentState == EnemyStateMachines.TurnState.PLAYING)
            {
                var closest = scriptableListHero.GetClosest(transform.position);
                if (closest != null)
                {
                    currentX = (int) transform.position.x;
                    currentY = (int) transform.position.y;
                    targetPosX = (int) closest.gameObject.transform.position.x;
                    targetPosY = (int) closest.gameObject.transform.position.y;
                    List<PathNode> path = pathfinding.FindPath(currentX, currentY, targetPosX, targetPosY);
                    temp = path;
                    var distance = (transform.position - closest.transform.position).sqrMagnitude;
                    if (distance > 1f && path != null)
                    {
                        animator.SetBool(IsMoving, true);
                        Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), 0.5f);
                        yield return Timing.WaitForSeconds(1);
                    }
                    if (distance <= 1f)
                    { animator.SetBool(IsMoving, false); }
                }
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    IEnumerator<float> TweenAttack()
    {
        Tween.PositionX(transform, transform.position.x-0.3f, 0.5f, Ease.Default, 2, CycleMode.Yoyo);
        yield return Timing.WaitForOneFrame;
    }
 }
