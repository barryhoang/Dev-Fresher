using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;
using Sequence = PrimeTween.Sequence;

namespace Minh
{
    public class Player : CharacterStats
    {
        [SerializeField] private TweenSettings _tweenSettings;
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableListPlayer _soapListPlayer;
        [SerializeField] private ScriptableListPlayer _soapListDeadPlayer;
        [SerializeField] private GridMapVariable _gridMap;
        [SerializeField] private ScriptableEventNoParam _onFightRaise;
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private PlayerPlacement _playerPlacement;
        public PlayerPlacement Playerplacement => _playerPlacement;

        private CharacterState _characterState;
        private HealthBar _healthBarScript;
        private Coroutine _attackCoroutine;
        private Vector3 _attackPosition;
        private string _gameObjectID;
        [SerializeField] public GameObject _gameManagerGameObject;
        public int _health;
        public GameManager _gameManager;
        [SerializeField] private Vector3[] _movePosition;
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
        [SerializeField] private Transform child;
        [SerializeField] private VfxSpawner _vfxSpawner;
        private Vector3 _prevPosition;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar, gameObject.transform, true);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _gameObjectID = gameObject.GetInstanceID().ToString();
            _grid = GameObject.Find("Grid");
            _mainTileMap = _grid.transform.Find("MainTileMap").gameObject;
            _gridManager = _mainTileMap.GetComponent<GridManager>();
            _pathfinding = _mainTileMap.GetComponent<Pathfinding>();
            _grid1 = GameObject.Find("Grid (1)");
            _highlightTilemap = _grid1.transform.Find("HighlightTileMap").gameObject;
            _targetTilemap = _highlightTilemap.GetComponent<Tilemap>();
            child = transform.Find("PlayerSprite");
        }

        public void AddToList()
        {
            _soapListPlayer.Add(this);
        }

        private void Start()
        {
            //Debug.Log("GRID MAP VALUE" + _gridMap.Value[5, 5]);
        }

        public void Init()
        {
            Timing.RunCoroutine(PlayerMove().CancelWith(gameObject));
            Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
        }

        // private void OnTriggerStay2D(Collider2D other)
        // {
        //     if (_characterState == CharacterState.Idle)
        //     {
        //         if (_soapListEnemy != null)
        //         {
        //             var enemy = other.GetComponent<Enemy>();
        //             // Timing.PauseCoroutines("playerMove" + _gameObjectID);
        //             //
        //             // Timing.RunCoroutine(PlayerAttack(enemy).CancelWith(gameObject), "playerAttack" + _gameObjectID);
        //             Debug.Log("Attackkkk");
        //         }
        //     }
        // }

        private void OnEnable()
        {
            _health = characterStats._maxHealth;
            _characterState = CharacterState.Idle;
            Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
            // Timing.RunCoroutine(PlayerMove().CancelWith(gameObject), "playerMove" + _gameObjectID);
            _gameManagerGameObject = GameObject.Find("GameManager");
            _gameManager = _gameManagerGameObject.GetComponent<GameManager>();
            _healthBarScript.HealthBarSize(characterStats._maxHealth.Value, _health);
        }

        private void OnDisable()
        {
            _soapListPlayer.Remove(this);
            _soapListDeadPlayer.Add(this);
        }


        private IEnumerator<float> PlayerMove()
        {
            var closet = _soapListEnemy.GetClosest(transform.position);
            List<PathNode> path = new List<PathNode>();

            if (closet != null)
            {
                path = _pathfinding.FindPath(_currentX, _currentY, (int) closet.transform.position.x,
                    (int) closet.transform.position.y);
            }

            while (true)
            {
                
                if (_gameManager._gameState == GameState.Fighting)
                {
                    _animator.SetBool("PlayerMove",true);
                    _targetTilemap.ClearAllTiles();
                    if (closet == null)
                    {
                        closet = _soapListEnemy.GetClosest(transform.position);
                        
                    }

                    _currentX = (int) this.transform.position.x;
                    _currentY = (int) this.transform.position.y;

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

                        path = _pathfinding.FindPath(_currentX, _currentY, (int) closet.transform.position.x,
                            (int) closet.transform.position.y);


                        if (Vector2.Distance(new Vector2(path[path.Count - 1].xPos, path[path.Count - 1].yPos),
                            transform.position) > 1.8f)
                        {
                            
                            _gridMap.Value[(int) _prevPosition.x, (int) _prevPosition.y] = false;
                            _gridMap.Value[path[0].xPos, path[0].yPos] = true;
                            Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), 0.5f, Ease.Default);
                            yield return Timing.WaitForSeconds(0.5f);
                            _prevPosition = new Vector3(path[0].xPos, path[0].yPos);
                        }
                        else
                        {
                            _animator.SetBool("PlayerMove",false);
                            Timing.RunCoroutine(PlayerAttack(closet).CancelWith(closet.gameObject),
                                "playerAttack" + _gameObjectID);
                            _characterState = CharacterState.Attack;
                            
                            yield return Timing.WaitForSeconds(1 / (float) characterStats._attackRate * 2);
                        }
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }


        // private IEnumerator<float> PlayerMove()
        // {
        //     while (true)
        //     {
        //         
        //     }
        // }

        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    _gridMap.Value[(int) transform.position.x, (int) transform.position.y] = false;
                    gameObject.SetActive(false);
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private void CheckEnemyHealth(Enemy enemy)
        {
            if (enemy._health <= 0 || enemy == null)
            {
                Debug.Log(enemy._health + "ENEMY HEALTH");
                _characterState = CharacterState.Idle;
            }
        }


        private IEnumerator<float> PlayerAttack(Enemy enemy)
        {
            _playerPlacement.SnapToGrid();
            _attackPosition = enemy.gameObject.transform.position;
            _characterState = CharacterState.Attack;
            Vector2 distance = enemy.transform.position - transform.position;
            Vector2 offset = new Vector2();


            if (_characterState == CharacterState.Attack)
            {
                Debug.Log("PLAYER Attacking" + _gameObjectID);
                if (enemy != null)
                {
                    Transform _prevPosition = transform;
                    Vector2 dir = (enemy.transform.position - transform.position).normalized;
                    Vector2 attackPosition = new Vector2(child.transform.position.x + dir.x * 0.3f,
                        dir.y * 0.3f + child.transform.position.y);
                    Tween.Position(child, attackPosition, 1 / (float) characterStats._attackRate, Ease.Default, 2,
                        CycleMode.Yoyo);
                    Debug.Log("ATTACK" + 1 / (float) characterStats._attackRate);
                    Attack(enemy);
                }
            }

            yield return Timing.WaitForOneFrame;
        }

        private void Attack(Enemy enemy)
        {
            if (_characterState == CharacterState.Attack)
            {
                CheckEnemyHealth(enemy);
                Vector2 dir = (enemy.transform.position - transform.position).normalized;
                Vector3 myRotationAngles = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles;
               Debug.Log("PLAYER Degree"+myRotationAngles);
               Debug.Log("PLAYER DIRECTION"+dir);
              
                enemy.TakeDamage(characterStats._damage);
                _vfxSpawner.SpawnAttackVFX(child,myRotationAngles.z);
            }
        }

        // private IEnumerator<float> PlayerMove()
        // {
        //     while (true)
        //     {
        //         if (_gameManager._gameState == GameState.Fighting)
        //         {
        //             if (_characterState == CharacterState.Idle)
        //             {
        //                 if (_soapListEnemy != null)
        //                 {
        //                     var closet = _soapListEnemy.GetClosest(transform.position);
        //                     var direction = (closet.transform.position - transform.position).normalized;
        //                     Debug.Log("Direction"+direction);
        //                     if (closet != null)
        //                     {
        //                         var distance = (closet.transform.position.x-transform.position.x);
        //                        
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
        //
        //         yield return Timing.WaitForOneFrame;
        //     }
        // }
        public void TakeDamage(int damage)

        {
            _health -= damage;
            _healthBarScript.HealthBarSize(characterStats._maxHealth.Value, _health);
        }


        protected override void Move(Vector3 target)
        {
            base.Move(target);
        }
    }
}