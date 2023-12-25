using System;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using MEC;
using PrimeTween;

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

        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar, gameObject.transform, true);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _gameManagerGameObject=GameObject.Find("GameManager");
             _gameManager = _gameManagerGameObject.GetComponent<GameManager>();
             _gameObjectID = enemyGameObject.GetInstanceID().ToString();
            _health = characterStats._maxHealth;
        }
        
        private void Start()
        {
           
            _characterState = CharacterState.Idle;
            Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
            Timing.RunCoroutine(EnemyMove().CancelWith(gameObject),"enemyMove"+ _gameObjectID);
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
                Timing.PauseCoroutines("enemyMove"+_gameObjectID);
                Timing.RunCoroutine(EnemyAttack(player).CancelWith(gameObject), "enemyAttack"+_gameObjectID);
                _distance = transform.position;
                Debug.Log("Attackkkk");
                _characterState = CharacterState.Attack;
            }


            // Debug.Log("Trigger");
            // Timing.PauseCoroutines("enemyMove"+_gameObjectID);
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            Timing.PauseCoroutines(_gameObjectID);
        }


        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    Die();
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        public void OnFight()
        {
           
        }

        private IEnumerator<float> EnemyAttack(Player player)
        {
            _attackPosition = player.gameObject.transform.position;
            _characterState = CharacterState.Attack;
            while (true)
            {
                Debug.Log("Attacking");
                if (_characterState == CharacterState.Attack)
                {
                    Tween.Position(transform, _attackPosition, _tweenSettings);
                    Attack(player);
                }

                yield return Timing.WaitForSeconds(1f / characterStats._attackRate);
            }
        }
       
        private IEnumerator<float> EnemyMove()
        {
        
            while (true)
            {
                if (_gameManager._gameState == GameState.Fighting)
                {
                    if (_characterState == CharacterState.Idle)
                    {
                        if (_soapListPlayer != null)
                        {
                            var closet = _soapListPlayer.GetClosest(transform.position);
                            if (closet != null)
                            {
                                Move(closet.transform.position);
                                _attackPosition = closet.transform.position;
                                
                            }
                        }
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }

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