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

        private Vector3 _prevPosition;

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
        }

        private void Start()
        {
            Timing.RunCoroutine(PlayerMove());
        }

        public void AddToList()
        {
            _soapListPlayer.Add(this);
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
            // Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
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

        private void Update()
        {
        }


        private IEnumerator<float> PlayerMove()
        {
            while (true)
            {
                _targetTilemap.ClearAllTiles();
                var closet = _soapListEnemy.GetClosest(transform.position);
                _currentX = (int) transform.position.x;
                _currentY = (int) transform.position.y;
                
                List<PathNode> path2 = _pathfinding.FindPath(_currentX, _currentY, (int) closet.transform.position.x,
                    (int) closet.transform.position.y,"player");
               
                // if (path != null)
                // {
                //     for (int i = 0; i < path.Count - 1; i++)
                //     {
                //Tween.Position(transform, new Vector3(path[i].xPos, path[i].yPos, 0), _tweenSettings);
                //transform.position = new Vector3(path[i].xPos, path[i].yPos, 0);
                // yield return Timing.WaitUntilDone(
                //     Timing.RunCoroutine(playerMovement(new Vector3(path[i].xPos, path[i].yPos, 0))));

                if (Vector2.Distance(closet.transform.position, transform.position) > 1.45f)
                {
                    _prevPosition = transform.position;
                   // _gridManager.Set((int) _prevPosition.x, (int) _prevPosition.y, 0);
                    Tween.Position(transform, new Vector3(path2[0].xPos, path2[0].yPos, 0), _tweenSettings);
                    _gridManager.Set(path2[0].xPos, path2[0].yPos, 3);
                    yield return Timing.WaitForSeconds(_tweenSettings.duration);
                }

                

                Debug.Log(Vector2.Distance(closet.transform.position, transform.position));


                yield return Timing.WaitForSeconds(0.3f);
               // _gridManager.Set(path2[0].xPos, path2[0].yPos, 0);
            }
        }

        // private IEnumerator<float> CheckHealth()
        // {
        //     while (true)
        //     {
        //         if (_health <= 0)
        //         {
        //             gameObject.SetActive(false);
        //         }
        //         yield return Timing.WaitForOneFrame;
        //     }
        // }
        //
        // private void CheckEnemyHealth(Enemy enemy)
        // {
        //     if (enemy._health <= 0 || enemy == null)
        //     {
        //         Timing.KillCoroutines("playerAttack" + _gameObjectID);
        //         _characterState = CharacterState.Idle;
        //         Timing.ResumeCoroutines("playerMove" + _gameObjectID);
        //     }
        // }


        // private IEnumerator<float> PlayerAttack(Enemy enemy)
        // {
        //    
        //     _playerPlacement.SnapToGrid();
        //     _attackPosition = enemy.gameObject.transform.position;
        //     _characterState = CharacterState.Attack;
        //     Vector2 distance = enemy.transform.position - transform.position;
        //     Vector2 offset=new Vector2();
        //     if (distance.x > 0)
        //     {
        //         offset.x = 0.5f;
        //     }
        //     else if(distance.x<0)
        //     {
        //         offset.y = -0.5f;
        //     }
        //     if (distance.y<0)
        //     {
        //         offset.y = 0.5f;
        //     }
        //     else if(distance.y>0)
        //     {
        //         offset.y = -0.5f;
        //     }
        //     while (true)
        //     {
        //        
        //         CheckEnemyHealth(enemy);
        //         if (_characterState == CharacterState.Attack)
        //         {
        //             Debug.Log("Attack" + _gameObjectID);
        //             if (enemy != null)
        //             {
        //                 Tween.Position(transform, (Vector2)transform.position+distance-offset, _tweenSettings);
        //             }
        //             Attack(enemy);
        //         }
        //         yield return Timing.WaitForSeconds(1f / characterStats._attackRate);
        //     }
        // }

        // private void Attack(Enemy enemy)
        // {
        //     if (_characterState == CharacterState.Attack)
        //     {
        //         Debug.Log("Attacking" + _gameObjectID);
        //         CheckEnemyHealth(enemy);
        //         enemy.TakeDamage(characterStats._damage);
        //     }
        // }
        //
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