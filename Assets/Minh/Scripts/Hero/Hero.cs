using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;
namespace Minh
{
    
    public class Hero : MonoBehaviour
    {
        [SerializeField] public HeroStat _heroStats;
        [SerializeField] public FightingGrid _fightingGrid;
        [SerializeField] public FightingMapVariable _fightingMapVariable;
        [SerializeField] public Vector2Int current;
        public Vector2Int prevPosition;
        [SerializeField] public TweenSettings _tweenSettings;
        public ScriptableListEnemy _soapListEnemy;
        public ScriptableListPlayer _soapListPlayer;
        private int _health;
        private Hero closet;
        private Vector3 _attackPosition;
        public string _gameObjectID;
        [SerializeField] private Transform child;

        private void Awake()
        {
            _fightingGrid = GameObject.Find("FightingManager").GetComponent<FightingGrid>();
            _gameObjectID = gameObject.GetInstanceID().ToString();
        }

        private void OnEnable()
        {
            _health = _heroStats._maxHealth;
        }

        public virtual void Start()
        {
            Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
           
        }
        public  IEnumerator<float> Move()
        {
            closet = CheckClosest();
            List<PathNode> path = new List<PathNode>();
            while (true)
            {
                //_targetTilemap.ClearAllTiles();
                // Debug.Log(_characterState + "ENEMY STATE");
                closet = CheckClosest();
                current = this.transform.position.ToV2Int();
             
                if (closet != null)
                {
                    if (closet.transform.position.x - this.transform.position.x < 0)
                    {
                        this.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        this.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    Vector2Int target = closet.transform.position.ToV2Int();
                 
                    path = _fightingGrid.GetPath(current.x, current.y, target.x,
                        target.y);

                    if (Vector2.Distance(closet.transform.position, transform.position) > 1.1f)
                    {
                        //_animator.SetBool("EnemyRun", true);
                        Vector2Int deleteTransform = this.transform.position.ToV2Int();
                        _fightingMapVariable.Value[deleteTransform.x, deleteTransform.y] = null;
                        _fightingMapVariable.Value[path[0].xPos, path[0].yPos] = this;
                        Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), 1/_heroStats._speed);
                       // prevPosition = transform.position.ToV2Int();
                         yield return Timing.WaitForSeconds(1/_heroStats._speed);
                    }
                    else
                    {
                        // _animator.SetBool("EnemyRun", false);
                         Timing.RunCoroutine(HeroAttack(closet).CancelWith(closet.gameObject),
                             "enemyAttack" + _gameObjectID);

                        yield return Timing.WaitForSeconds(1 / (float) _heroStats._attackRate * 2);
                    }
                }
            }

        }

        private Hero CheckClosest()
        {
            var hero=new Hero();
            if (this.CompareTag("Player"))
            {
              
                hero=_soapListEnemy.GetClosest(transform.position);
            }
            else if (this.CompareTag("Enemy"))
            {
                
                hero =_soapListPlayer.GetClosest(transform.position);
              
            }
            

            return hero;
        }
        private IEnumerator<float> HeroAttack(Hero hero)
        {
            _attackPosition = hero.gameObject.transform.position;
            Vector2 distance = hero.transform.position - transform.position;
            Vector2 offset = new Vector2();
            
                if (hero != null)
                {
                    Transform _prevPosition = transform;
                    Vector2 dir = (hero.transform.position - transform.position).normalized;
                    Vector2 attackPosition = new Vector2(child.transform.position.x + dir.x * 0.3f,
                        dir.y * 0.3f + child.transform.position.y);
                    Tween.Position(child, attackPosition, 1 / (float) _heroStats._attackRate, Ease.Default, 2,
                        CycleMode.Yoyo);
                    Attack(hero);
                }
            

            yield return Timing.WaitForOneFrame;
        }

        private void Attack(Hero hero)
        {
          
                CheckTargetHealth(hero);
                Vector2 dir = (hero.transform.position - transform.position).normalized;
                Vector3 myRotationAngles = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles;
                hero.TakeDamage((int)_heroStats._damage);
               // _vfxSpawner.SpawnAttackVFX(child, myRotationAngles.z);
        }
        private void CheckTargetHealth(Hero hero)
        {
            if (hero._health <= 0 || hero == null)
            {
                
            }
        }
        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                Debug.Log(_health);
                if (_health <= 0)
                {
                    Debug.Log("bai bai");
                    Vector2Int position = this.transform.position.ToV2Int();
                    _fightingMapVariable.Value[position.x, position.y] = null;
                    Destroy(gameObject);
                }

                yield return Timing.WaitForOneFrame;
            }
         
            // ReSharper disable once IteratorNeverReturns
        }
    }
    
}