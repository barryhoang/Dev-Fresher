﻿using System;
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
        [SerializeField] private ScriptableEventNoParam _onFightRaise;
        [SerializeField] private GameObject _healthBar;


        private CharacterState _characterState;
        private HealthBar _healthBarScript;
        private Coroutine _attackCoroutine;
        private Vector3 _attackPosition;
        private string _gameObjectID;
        [SerializeField] public GameObject _gameManagerGameObject;
        public int _health;
        public GameManager _gameManager;

        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar, this.transform.parent, true);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _soapListPlayer.Add(this);
            _gameObjectID = gameObject.GetInstanceID().ToString();
        }

        private void Start()
        {
            _health = characterStats._maxHealth;
            _characterState = CharacterState.Idle;
            Timing.RunCoroutine(CheckHealth().CancelWith(gameObject));
            Timing.RunCoroutine(PlayerMove().CancelWith(gameObject), "playerMove" + _gameObjectID);
       
            _gameManagerGameObject=GameObject.Find("GameManager");
            _gameManager = _gameManagerGameObject.GetComponent<GameManager>();
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (_characterState == CharacterState.Idle)
            {
                if (_soapListEnemy != null)
                {
                    var enemy = other.GetComponent<Enemy>();
                    Timing.PauseCoroutines("playerMove" + _gameObjectID);
                    _characterState = CharacterState.Attack;
                    Timing.RunCoroutine(PlayerAttack(enemy),
                        "playerAttack" + _gameObjectID);
                    Debug.Log("Attackkkk");
                }
            }
        }

        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     _characterState = CharacterState.Idle;
        //     Timing.ResumeCoroutines("playerMove" + _gameObjectID);
        // }

        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    Destroy(gameObject);
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private void CheckEnemyHealth(Enemy enemy)
        {
            if (enemy._health <= 0||enemy==null)
            {
                Timing.KillCoroutines("playerAttack"+_gameObjectID);
                _characterState = CharacterState.Idle;
                Timing.ResumeCoroutines("playerMove" + _gameObjectID);
            }
        }
        
        
         private IEnumerator<float> PlayerAttack(Enemy enemy)
        {
            _attackPosition = enemy.gameObject.transform.position;
            
            while (true)
            {
                CheckEnemyHealth(enemy);
                if (_characterState == CharacterState.Attack)
                {
                    Debug.Log("Attack"+_gameObjectID);
                    Tween.Position(transform, _attackPosition, _tweenSettings);
                    Attack(enemy);
                }

                yield return Timing.WaitForSeconds(1f / characterStats._attackRate);
            }
        }

        private void Attack(Enemy enemy)
        {
            if (_characterState == CharacterState.Attack)
            {
                Debug.Log("Attacking"+_gameObjectID);
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
                                Debug.Log("Moving");
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