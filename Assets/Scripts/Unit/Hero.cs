using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
using PrimeTween;
using UnityEngine;

namespace Unit
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private FloatVariable health;
        [SerializeField] private FloatVariable maxHealth;
        [SerializeField] private IntVariable heroDamage;
        [SerializeField] private ScriptableListHero scriptableListHero;
        [SerializeField] private ScriptableListEnemy scriptableListEnemy;
        [SerializeField] private Animator animator;
        [SerializeField] private Pathfinding pathFinding;
        [SerializeField] private PlacementGrid placementGrid;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private GameObject hpBar;
        
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
            scriptableListHero.Add(this);
            placementGrid.selectableHeroes.Add(gameObject);
            health.Value = maxHealth;
            animator.SetBool(Dead, false);
            animator.SetBool(Moving, false);
        }

        private void Start()
        {
            onFight.OnRaised += Move;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_curTime <= 0 && other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(heroDamage);
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
            health.Add(-damage);
            if (health.Value <= 0)
            {
                health.Value = 0;
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            scriptableListHero.Remove(this);
            animator.SetBool(Dead, true);
            onFight.OnRaised -= Move;
            hpBar.SetActive(false);
        }

        private IEnumerator<float> TweenMove()
        {
            while (true)
            {
                if (health.Value > 0)
                {
                    var closest = scriptableListEnemy.GetClosest(transform.position);
                    if (closest != null)
                    {
                        var heroPos = transform.position;
                        _currentX = (int) heroPos.x;
                        _currentY = (int) heroPos.y;
                        var enemyPos = closest.gameObject.transform.position;
                        _targetPosX = (int) enemyPos.x;
                        _targetPosY = (int) enemyPos.y;
                        var path = pathFinding.FindPath(_currentX, _currentY, _targetPosX, _targetPosY);
                        temp = path;
                        var distance = (heroPos - closest.transform.position).sqrMagnitude;
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
            var heroPos = transform;
            Tween.PositionX(heroPos, heroPos.position.x + 0.3f, 0.5f, Ease.Default, 2, CycleMode.Yoyo);
            yield return Timing.WaitForOneFrame;
        }

        private void Move()
        {
            Timing.RunCoroutine(TweenMove().CancelWith(gameObject));
        }

        private void OnDestroy()
        {
            onFight.OnRaised -= Move;
        }
    }
}