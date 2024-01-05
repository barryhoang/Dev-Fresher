using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;

public class Hero : MonoBehaviour
{
    [SerializeField] private float curTime;
    [SerializeField] private float receivedDmgDelay = 1;
    [SerializeField] private FloatVariable heroHealth;
    [SerializeField] private FloatVariable heroMaxHealth;
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private ScriptableListGameObject listHero;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableEventInt onHeroDamaged;
    [SerializeField] private HeroStateMachines HSM;
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
        scriptableListHero.Add(this);
        listHero.Add(gameObject);
        heroHealth.Value = heroMaxHealth;
        animator.SetFloat(Hp, Mathf.Abs(heroHealth.Value));
    }

    private void Start()
    {
        Timing.RunCoroutine(TweenMove().CancelWith(gameObject));
    }

    private void Update()
    {
        if (heroHealth <= 0)
        {
            animator.SetFloat(Hp, 0);
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (curTime <= 0 && other.CompareTag("Enemy"))
        {
            onHeroDamaged.Raise(0);
            curTime = receivedDmgDelay;
            Timing.RunCoroutine(TweenAttack().CancelWith(gameObject));
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        heroHealth.Add(-damage);
    }

    public void Die()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        scriptableListHero.Remove(this);
        HSM.currentState = HeroStateMachines.TurnState.DEAD;
    }

    IEnumerator<float> TweenMove()
    {
        while (true)
        {
            if (gameObject != null && HSM.currentState == HeroStateMachines.TurnState.PLAYING)
            {
                var closest = scriptableListEnemy.GetClosest(transform.position);
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
                    if (distance <= 1f) { animator.SetBool(IsMoving, false); }
                }
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    IEnumerator<float> TweenAttack()
    {
        Tween.PositionX(transform, transform.position.x + 0.3f, 0.5f, Ease.Default, 2, CycleMode.Yoyo);
        yield return Timing.WaitForOneFrame;
    }
}