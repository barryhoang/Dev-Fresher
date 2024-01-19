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
        private List<PathNode> path;
        public string _gameObjectID;
        [SerializeField] public Transform child;
        [SerializeField] private HealthViewer _healthViewer;
        [SerializeField] public TextMesh _playerText;
        [SerializeField] private HeroViewer _heroViewer;
        [SerializeField] public Vector2 _placementPosition;

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
            _healthViewer.HealthBarSize(_heroStats._maxHealth, _health);
        }

        public IEnumerator<float> Move()
        {
            closet = CheckClosest();
            while (true)
            {
                closet = CheckClosest();
                current = this.transform.position.ToV2Int();

                if (closet != null)
                {
                    if (closet.transform.position.x - this.transform.position.x < 0)
                    {
                        this.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        this.transform.localScale = new Vector3(1, 1, 1);
                    }

                    Vector2Int target = closet.transform.position.ToV2Int();

                    path = _fightingGrid.GetPath(current.x, current.y, target.x,
                        target.y);
                    if (path != null)
                    {
                        if (Vector2.Distance(closet.transform.position, transform.position) > 1f)
                        {
                            if (closet.path != null)
                            {
                                if (Vector2.Distance(new Vector2(closet.path[0].xPos, closet.path[0].yPos),
                                    transform.position) > 1f)
                                {
                                    Vector2Int deleteTransform = this.transform.position.ToV2Int();
                                    MoveHero();
                                    yield return Timing.WaitForSeconds(1 / _heroStats._speed);
                                }
                            }
                            else
                            {
                                MoveHero();
                                yield return Timing.WaitForSeconds(1 / _heroStats._speed);
                            }
                        }
                        else
                        {
                            Timing.RunCoroutine(HeroAttack(closet).CancelWith(closet.gameObject));
                            path = null;
                            yield return Timing.WaitForSeconds(1 / (float) _heroStats._attackRate * 2);
                        }
                    }

                    yield return Timing.WaitForOneFrame;
                }
                else
                {
                    _heroViewer.StopMoving();
                    yield break;
                }
            }
        }

        private Hero CheckClosest()
        {
            var hero = new Hero();
            if (this.CompareTag("Player"))
            {
                hero = _soapListEnemy.GetClosest(transform.position);
            }
            else if (this.CompareTag("Enemy"))
            {
                hero = _soapListPlayer.GetClosest(transform.position);
            }
            return hero;
        }

        private void MoveHero()
        {
            Vector2Int deleteTransform = this.transform.position.ToV2Int();
            _fightingMapVariable.Value[deleteTransform.x, deleteTransform.y] = null;
            _fightingMapVariable.Value[path[0].xPos, path[0].yPos] = this;
            _heroViewer.MoveHero(transform, new Vector3(path[0].xPos, path[0].yPos, 0),
                1 / _heroStats._speed);
        }

        private IEnumerator<float> HeroAttack(Hero hero)
        {
            _attackPosition = hero.gameObject.transform.position;
            if (hero != null)
            {
                Vector2 dir = (hero.transform.position - transform.position).normalized;
                Vector2 attackPosition = new Vector2(child.transform.position.x + dir.x * 0.3f,
                    dir.y * 0.3f + child.transform.position.y);
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(
                    _heroViewer.HeroAttack(hero, child, attackPosition, 1 / (float) _heroStats._attackRate)
                        .CancelWith(gameObject), "attack" + _gameObjectID));
                hero.TakeDamage((int) _heroStats._damage);
            }
            yield return Timing.WaitForOneFrame;
        }

        public void ResetHero()
        {
            Timing.PauseCoroutines("attack" + _gameObjectID);
            Timing.PauseCoroutines("move" + _gameObjectID);
            Vector2Int position = this.transform.position.ToV2Int();
            _fightingMapVariable.Value[position.x, position.y] = null;
            Tween.StopAll(transform);
            Tween.StopAll(child);
            _health = _heroStats._maxHealth;
            var _placementPositionInt = _placementPosition.ToV2Int();
            this.transform.position = _placementPosition;
            _fightingMapVariable.Value[_placementPositionInt.x, _placementPositionInt.y] = this;
            _heroViewer.StopMoving();
            child.localPosition = new Vector3(0, 0, 0);
        }

        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    Vector2Int position = this.transform.position.ToV2Int();
                    _fightingMapVariable.Value[position.x, position.y] = null;
                    Destroy(gameObject);
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}