using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;

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


        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar, gameObject.transform, true);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _gameObjectID = gameObject.GetInstanceID().ToString();
        }

        private void Start()
        {
            CheckingMove();
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
                    Timing.PauseCoroutines("playerMove" + _gameObjectID);
                    
                    Timing.RunCoroutine(PlayerAttack(enemy).CancelWith(gameObject), "playerAttack" + _gameObjectID);
                    Debug.Log("Attackkkk");
                }
            }
        }

        private void OnEnable()
        {
            _health = characterStats._maxHealth;
            _characterState = CharacterState.Idle;
            Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
            Timing.RunCoroutine(PlayerMove().CancelWith(gameObject), "playerMove" + _gameObjectID);
            _gameManagerGameObject = GameObject.Find("GameManager");
            _gameManager = _gameManagerGameObject.GetComponent<GameManager>();
            _healthBarScript.HealthBarSize(characterStats._maxHealth.Value, _health);
        }

        private void OnDisable()
        {
            _soapListPlayer.Remove(this);
            _soapListDeadPlayer.Add(this);
        }
        
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     _characterState = CharacterState.Idle;
        //     Timing.ResumeCoroutines("playerMove" + _gameObjectID);
        // }
        private void CheckingMove()
        {
            int k = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Vector3Int cellPosition = _playerPlacement.grid.WorldToCell(new Vector2(transform.position.x+i,transform.position.y+j));
                    Vector3Int playerCellPosition = _playerPlacement.grid.WorldToCell(transform.position);
                    if (cellPosition != playerCellPosition)
                    {
                        _movePosition[k] = cellPosition;
                        k++;
                    }
                    
                }
            }
        }

        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    gameObject.SetActive(false);
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private void CheckEnemyHealth(Enemy enemy)
        {
            if (enemy._health <= 0 || enemy == null)
            {
                Timing.KillCoroutines("playerAttack" + _gameObjectID);
                _characterState = CharacterState.Idle;
                Timing.ResumeCoroutines("playerMove" + _gameObjectID);
            }
        }


        private IEnumerator<float> PlayerAttack(Enemy enemy)
        {
           
            _playerPlacement.SnapToGrid();
            _attackPosition = enemy.gameObject.transform.position;
            _characterState = CharacterState.Attack;
            Vector2 distance = enemy.transform.position - transform.position;
            Vector2 offset=new Vector2();
            if (distance.x > 0)
            {
                offset.x = 0.5f;
            }
            else if(distance.x<0)
            {
                offset.y = -0.5f;
            }
            if (distance.y<0)
            {
                offset.y = 0.5f;
            }
            else if(distance.y>0)
            {
                offset.y = -0.5f;
            }
            while (true)
            {
               
                CheckEnemyHealth(enemy);
                if (_characterState == CharacterState.Attack)
                {
                    Debug.Log("Attack" + _gameObjectID);
                    if (enemy != null)
                    {
                        Tween.Position(transform, (Vector2)transform.position+distance-offset, _tweenSettings);
                    }
                    Attack(enemy);
                }
                yield return Timing.WaitForSeconds(1f / characterStats._attackRate);
            }
        }

        private void Attack(Enemy enemy)
        {
            if (_characterState == CharacterState.Attack)
            {
                Debug.Log("Attacking" + _gameObjectID);
                CheckEnemyHealth(enemy);
                enemy.TakeDamage(characterStats._damage);
            }
        }

        private IEnumerator<float> PlayerMove()
        {
            while (true)
            {
                if (_gameManager._gameState == GameState.Fighting)
                {
                    if (_characterState == CharacterState.Idle)
                    {
                        if (_soapListEnemy != null)
                        {
                            var closet = _soapListEnemy.GetClosest(transform.position);
                            if (closet != null)
                            {
                                var distance = (closet.transform.position.x-transform.position.x);
                               
                                if (distance <= 0)
                                {
                                    transform.localScale=new Vector3(-1,1,1);
                                }
                                else
                                {
                                    transform.localScale=new Vector3(1,1,1);
                                }
                                Move(closet.transform.position);
                                _attackPosition = closet.transform.position;
                            }
                        }
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }

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