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
        
        private Vector3 distance;
        private CharacterState _characterState;
        private HealthBar _healthBarScript;
        public GameObject enemyGameObject;
        private string _gameObjectID;
        private Vector3 _attackPosition;
       
        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _soapListEnemy.Add(this);
            _gameObjectID = enemyGameObject.GetInstanceID().ToString();
            characterStats._health.Value = characterStats._maxHealth.Value;
            
        }

        private void OnDisable()
        {
            _soapListEnemy.Remove(this);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
           
                if (_characterState == CharacterState.Idle)
                {
                    var player = other.GetComponent<Player>();
                    Timing.PauseCoroutines("playerMove");
                    Timing.RunCoroutine(PlayerAttack(player), "playerAttack");
                    distance = transform.position;
                    Debug.Log("Attackkkk");
                    _characterState = CharacterState.Attack;
                }

          
                Debug.Log("Trigger");
                Timing.PauseCoroutines(_gameObjectID);
           
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Timing.PauseCoroutines(_gameObjectID);
        }

        public void OnFight()
        {
            Timing.RunCoroutine(EnemyMove(),_gameObjectID);
        }
        private IEnumerator<float> PlayerAttack(Player player)
        {
            while (true)
            {
                Tween.Position(transform, _attackPosition, _tweenSettings)
                    .OnComplete(() => Tween.Position(transform, distance, _tweenSettings));
                Attack(player);
            
                yield return Timing.WaitForSeconds(2f);
                
            }
        }

        private IEnumerator<float> EnemyMove()
        {
            
            while (true)
            {
                var closet = _soapListPlayer.GetClosest(transform.position);
                Move(closet.transform.position);
                _attackPosition = closet.transform.position;
                yield return Timing.WaitForOneFrame;
            }
        }

        private void Attack(Player player)
        {
            player.TakeDamage(characterStats._damage);
        }
        public void TakeDamage(int damage)
        {
           characterStats._health.Value -= damage;
            
            _healthBarScript.HealthBarSize(characterStats._maxHealth.Value,characterStats._health.Value);
        }

       
       
        protected override void Move(Vector3 target)
        {
            
            base.Move(target);
        }
    }
}