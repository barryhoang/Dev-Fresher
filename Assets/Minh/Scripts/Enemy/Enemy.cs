using System;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using MEC;
using PrimeTween;
using UnityEngine.Tilemaps;

namespace Minh
{
    public class Enemy : CharacterStats
    {
        [SerializeField] private TweenSettings _tweenSettings;
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableListPlayer _soapListPlayer;
        [SerializeField] private ScriptableEventNoParam _onFight;
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private GameObject _healthBar;

        private Vector3 _distance;
        public CharacterState _characterState;
        private HealthBar _healthBarScript;
        public GameObject enemyGameObject;
        private string _gameObjectID;
        private Vector3 _attackPosition;
        [SerializeField] public GameObject _gameManagerGameObject;
        [SerializeField] public int _health;
        public GameManager _gameManager;
        [SerializeField] private EnemyPlacement _enemyPlacement;
        [SerializeField] private GameObject _grid;
        [SerializeField] private GameObject _mainTileMap;
        [SerializeField] private GameObject _grid1;
        [SerializeField] private GameObject _highlightTilemap;
        [SerializeField] private Tilemap _targetTilemap;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private TileBase _hightlightTile;
        [SerializeField] private Pathfinding _pathfinding;
        [SerializeField] private int _currentX = 0;
        [SerializeField] private int _currentY = 0;
        [SerializeField] private Transform hit;
        private bool _isAttacking;

        private Vector3 _prevPosition;


        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar, gameObject.transform, true);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _gameManagerGameObject = GameObject.Find("GameManager");
            _gameManager = _gameManagerGameObject.GetComponent<GameManager>();
            _gameObjectID = enemyGameObject.GetInstanceID().ToString();
            _health = characterStats._maxHealth;
            _grid = GameObject.Find("Grid");
            _mainTileMap = _grid.transform.Find("MainTileMap").gameObject;
            _gridManager = _mainTileMap.GetComponent<GridManager>();
            _pathfinding = _mainTileMap.GetComponent<Pathfinding>();
            _grid1 = GameObject.Find("Grid (1)");
            _highlightTilemap = _grid1.transform.Find("HighlightTileMap").gameObject;
            _targetTilemap = _highlightTilemap.GetComponent<Tilemap>();
        }

        private void Start()
        {
            _characterState = CharacterState.Idle;
            // Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
            // Timing.RunCoroutine(EnemyMove().CancelWith(gameObject), "enemyMove" + _gameObjectID);
            Timing.RunCoroutine(EnemyMove());
            hit = transform.Find("Hit");
        }

        private void OnDestroy()
        {
            _soapListEnemy.Remove(this);
        }

        private void Update()
        {
        }

        public void AddToList()
        {
            _soapListEnemy.Add(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_characterState == CharacterState.Idle)
            {
                var player = other.GetComponent<Player>();
                // Timing.PauseCoroutines("enemyMove" + _gameObjectID);
                //    Timing.RunCoroutine(EnemyAttack(player).CancelWith(gameObject), "enemyAttack" + _gameObjectID);
                _distance = transform.position;
                Debug.Log("Attackkkk");
                _characterState = CharacterState.Attack;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Timing.PauseCoroutines(_gameObjectID);
        }

        // private IEnumerator<float> CheckHealth()
        // {
        //     while (true)
        //     {
        //         if (_health <= 0)
        //         {
        //             Die();
        //         }
        //         yield return Timing.WaitForOneFrame;
        //     }
        // }

        public void OnFight()
        {
        }

        private IEnumerator<float> EnemyMove()
        {
            var closet = _soapListPlayer.GetClosest(transform.position);
            while (true)
            {
                _targetTilemap.ClearAllTiles();
                
                
                    closet = _soapListPlayer.GetClosest(transform.position);
                

                _currentX = (int) this.transform.position.x;
                _currentY = (int) this.transform.position.y;
                List<PathNode> path = _pathfinding.FindPath(_currentX, _currentY, (int) closet.transform.position.x,
                    (int) closet.transform.position.y);

                if (Vector2.Distance(closet.transform.position, transform.position) > 1.8f)
                {
                    _gridMap.Value[(int)_prevPosition.x, (int)_prevPosition.y] = false;
                    _prevPosition = new Vector3(path[0].xPos, path[0].yPos, 0);
                    _gridMap.Value[path[0].xPos, path[0].yPos] = true;
                    Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), _tweenSettings);
                    _prevPosition = transform.position;
                }
                else
                {
                    Timing.RunCoroutine(EnemyAttack(closet).CancelWith(gameObject), "enemyAttack" + _gameObjectID);
                    _isAttacking = true;
                    yield return Timing.WaitForSeconds(1 / (float)characterStats._attackRate*2);
                }
             
                yield return Timing.WaitForSeconds(_tweenSettings.duration);
            }
        }

        private IEnumerator<float> EnemyAttack(Player player)
        {
            _enemyPlacement.SetEnemyCenter();
            _attackPosition = player.gameObject.transform.position;
            _characterState = CharacterState.Attack;


            if (_characterState == CharacterState.Attack)
            {
                Debug.Log("ENEMY Attacking");
                Vector2 dir = (player.transform.position - transform.position).normalized;
                Vector2 attackPosition = new Vector2(hit.transform.position.x + dir.x * 0.3f,
                    dir.y * 0.3f + hit.transform.position.y);
                Tween.Position(hit, attackPosition, 1 / (float)characterStats._attackRate, Ease.Default, 2, CycleMode.Yoyo);
                Attack(player);
            }
            yield return Timing.WaitForOneFrame;
        }

        // private IEnumerator<float> EnemyMove()
        // {
        //     while (true)
        //     {
        //         if (_gameManager._gameState == GameState.Fighting)
        //         {
        //             if (_characterState == CharacterState.Idle)
        //             {
        //                 if (_soapListPlayer != null)
        //                 {
        //                     var closet = _soapListPlayer.GetClosest(transform.position);
        //                     if (closet != null)
        //                     {
        //                         var distance = (closet.transform.position.x-transform.position.x);
        //                         if (distance <= 0)
        //                         {
        //                             transform.localScale=new Vector3(-1,1,1);
        //                         }
        //                         else
        //                         {
        //                             transform.localScale=new Vector3(1,1,1);
        //                         }
        //                         Move(closet.transform.position);
        //                         _attackPosition = closet.transform.position;
        //                     }
        //                 }
        //             }
        //         }
        //         yield return Timing.WaitForOneFrame;
        //     }
        // }
        //
        private void Attack(Player player)
        {
            if (_characterState == CharacterState.Attack)
            {
                player.TakeDamage(characterStats._damage);
                if (player._health <= 0)
                {
                    _characterState = CharacterState.Idle;
                    Timing.KillCoroutines("playerAttack");
                    Timing.ResumeCoroutines("playerMove" + _gameObjectID);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBarScript.HealthBarSize(characterStats._maxHealth.Value, _health);
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        protected override void Move(Vector3 target)
        {
            base.Move(target);
        }
    }
}