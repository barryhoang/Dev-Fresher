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
        }

        private void OnDestroy()
        {
            _soapListEnemy.Remove(this);
        }

        private void Update()
        {
            Debug.Log(_gameManager._gameState);
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
            while (true)
            {
                
                _targetTilemap.ClearAllTiles();
                var closet = _soapListPlayer.GetClosest(transform.position);
                _currentX = (int) this.transform.position.x;
                _currentY = (int) this.transform.position.y;
                
                List<PathNode> path = _pathfinding.FindPath(_currentX, _currentY, (int) closet.transform.position.x,
                    (int) closet.transform.position.y,"enemy");
                 
                Debug.Log(transform.position);
                
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
                   _gridManager.Set((int) _prevPosition.x, (int) _prevPosition.y, 0);
                   _gridManager.Set(path[0].xPos, path[0].yPos, 2);
                    Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), _tweenSettings);
                  
                }
                
               
                Debug.Log(Vector2.Distance(closet.transform.position,transform.position));
                yield return Timing.WaitForSeconds(_tweenSettings.duration);
                
                
                
               
                yield return Timing.WaitForSeconds(0.3f);
               // _gridManager.Set(path[0].xPos, path[0].yPos, 0);
            }
        }
        // private IEnumerator<float> PlayerMove()
        // {
        //     while (true)
        //     {
        //         
        //         _targetTilemap.ClearAllTiles();
        //         var closet = _soapListEnemy.GetClosest(transform.position);
        //         _currentX = (int) transform.position.x;
        //         _currentY = (int) transform.position.y;
        //         List<PathNode> path = _pathfinding.FindPath(_currentX, _currentY, (int) closet.transform.position.x,
        //             (int) closet.transform.position.y);
        //         
        //         // if (path != null)
        //         // {
        //         //     for (int i = 0; i < path.Count - 1; i++)
        //         //     {
        //         //Tween.Position(transform, new Vector3(path[i].xPos, path[i].yPos, 0), _tweenSettings);
        //         //transform.position = new Vector3(path[i].xPos, path[i].yPos, 0);
        //         // yield return Timing.WaitUntilDone(
        //         //     Timing.RunCoroutine(playerMovement(new Vector3(path[i].xPos, path[i].yPos, 0))));
        //         
        //         if (Vector2.Distance(closet.transform.position, transform.position) > 1.45f)
        //         {
        //             _prevPosition = transform.position;
        //             _gridManager.Set((int) _prevPosition.x, (int) _prevPosition.y, 0);
        //             _gridManager.Set(path[0].xPos, path[0].yPos, 3);
        //             Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), _tweenSettings);
        //             
        //         }
        //     
        //        
        //         Debug.Log(Vector2.Distance(closet.transform.position,transform.position));
        //         yield return Timing.WaitForSeconds(_tweenSettings.duration);
        //       
        //
        //         yield return Timing.WaitForSeconds(0.3f);
        //     }
        // }
        
        // private IEnumerator<float> EnemyAttack(Player player)
        // {
        //     _enemyPlacement.SetEnemyCenter();
        //     _attackPosition = player.gameObject.transform.position;
        //     _characterState = CharacterState.Attack;
        //     while (true)
        //     {
        //         Debug.Log("Attacking");
        //         if (_characterState == CharacterState.Attack)
        //         {
        //             Tween.Position(transform, _attackPosition, _tweenSettings);
        //             Attack(player);
        //         }
        //         yield return Timing.WaitForSeconds(1f / characterStats._attackRate);
        //     }
        // }
        //
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
        // private void Attack(Player player)
        // {
        //     if (_characterState == CharacterState.Attack)
        //     {
        //         player.TakeDamage(characterStats._damage);
        //         if (player._health <= 0)
        //         {
        //             _characterState = CharacterState.Idle;
        //             Timing.KillCoroutines("playerAttack");
        //             Timing.ResumeCoroutines("playerMove" + _gameObjectID);
        //         }
        //     }
        // }

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