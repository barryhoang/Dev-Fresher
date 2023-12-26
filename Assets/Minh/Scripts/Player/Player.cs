using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;

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
        [SerializeField]private Vector3[] _movePosition;
        [SerializeField] private UnityEngine.Grid _grid;
       [SerializeField]private bool[,] _takenPosition=new bool[17,9];


        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar, gameObject.transform, true);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _gameObjectID = gameObject.GetInstanceID().ToString();
        }

        private void Start()
        {
          //  Timing.RunCoroutine(Move());
        }

        public void AddToList()
        {
            _soapListPlayer.Add(this);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_characterState == CharacterState.Idle)
            {
                if (_soapListEnemy != null)
                {
                    var enemy = other.GetComponent<Enemy>();
                    // Timing.PauseCoroutines("playerMove" + _gameObjectID);
                    //
                    // Timing.RunCoroutine(PlayerAttack(enemy).CancelWith(gameObject), "playerAttack" + _gameObjectID);
                    Debug.Log("Attackkkk");
                }
            }
        }

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

        private IEnumerator<float> CheckingMove()
        {
            int k = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Vector3Int cellPosition = _grid.WorldToCell(new Vector2(transform.position.x+i,transform.position.y+j));
                    Vector3Int playerCellPosition = _grid.WorldToCell(transform.position);
                    if (cellPosition != playerCellPosition)
                    {
                        _movePosition[k] = cellPosition;
                        k++;
                    }
                }
            }

            yield return Timing.WaitForOneFrame;

        }

        private IEnumerator<float> Move()
        {
            while (true)
            {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(CheckingMove()));
                var closet = _soapListEnemy.GetClosest(transform.position);
                Vector3 closetGridPos = _grid.WorldToCell(closet.transform.position);
                Vector3 playerCellPosition1 = _grid.WorldToCell(transform.position);
                Vector3 directionNormal = (closetGridPos - playerCellPosition1);
                Vector3 diretionNormalize = directionNormal.normalized;
                Vector3Int directionInt = Vector3Int.RoundToInt(diretionNormalize);
                if(_takenPosition[(int)(transform.position.x+directionInt.x),(int)(transform.position.y+directionInt.y)]==true)
                {
                    if (directionInt.x < directionInt.y)
                    {
                        directionInt.y = directionInt.y - 1;
                    }
                    
                }
                Debug.Log("Direction INt" + directionInt);
                Tween.Position(transform, (transform.position + directionInt), 1f);
                _takenPosition[(int) transform.position.x, (int) transform.position.y] = true;
                _takenPosition[(int) playerCellPosition1.x, (int) playerCellPosition1.y] = false;
                
                yield return Timing.WaitForSeconds(0.3f);
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