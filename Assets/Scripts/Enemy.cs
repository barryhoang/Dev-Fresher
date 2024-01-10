using System.Collections.Generic;
using MEC;
using UnityEngine;
using Obvious.Soap;
using PrimeTween;
using Units.Enemy;
using Units.Hero;

public class Enemy : MonoBehaviour
{
    [SerializeField] private FloatVariable health;
    [SerializeField] private FloatVariable maxHealth;
    [SerializeField] private IntVariable enemyDamage;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private Pathfinding pathFinding;

    public List<PathNode> temp;
    private int _currentX;
    private int _currentY;
    private int _targetPosX;
    private int _targetPosY;
    private float _curTime;
    private const float ReceivedDmgDelay = 1;
    
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Moving = Animator.StringToHash("Moving");


    private void Awake()
    {
        scriptableListEnemy.Add(this);
        health.Value = maxHealth;
        animator.SetBool(Dead, false);
        animator.SetBool(Moving, false);
    }

    private void Start()
    {
        //Timing.RunCoroutine(TweenMove().CancelWith(gameObject));
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_curTime <= 0 && other.CompareTag("Player"))
        {
            var hero = other.GetComponent<Hero>();
            hero.TakeDamage(enemyDamage);
            _curTime = ReceivedDmgDelay;
            Timing.RunCoroutine(TweenAttack().CancelWith(gameObject));
        }
        else 
        {
            _curTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + "take damage");
        health.Add(-damage);
        if (!(health.Value < 0)) return;
        health.Value = 0;
        Die();
    }

    private void Die()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        scriptableListEnemy.Remove(this);
        animator.SetBool(Dead, true);
    }

    private IEnumerator<float> TweenMove()
    {
        while (true)
        {
            if (gameObject != null && gameManager.currentState == GameManager.State.Fight)
            {
                var closest = scriptableListHero.GetClosest(transform.position);
                if (closest != null)
                {
                    var enemyPos = transform.position;
                    _currentX = (int) enemyPos.x;
                    _currentY = (int) enemyPos.y;
                    var heroPos = closest.gameObject.transform.position;
                    _targetPosX = (int) heroPos.x;
                    _targetPosY = (int) heroPos.y;
                    var path = pathFinding.FindPath(_currentX, _currentY, _targetPosX, _targetPosY);
                    temp = path;
                    var distance = (enemyPos - closest.transform.position).sqrMagnitude;
                    if (distance > 1f && path != null)
                    {
                        animator.SetBool(Moving, true);
                        Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), 0.5f);
                        yield return Timing.WaitForSeconds(1);
                    }

                    if (distance <= 1f) animator.SetBool(Moving, false);
                }
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    private IEnumerator<float> TweenAttack()
    {
        var enemyPos = transform;
        Tween.PositionX(enemyPos, enemyPos.position.x-0.3f, 0.5f, Ease.Default, 2, CycleMode.Yoyo);
        yield return Timing.WaitForOneFrame;
    }
 }
