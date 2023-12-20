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
        [SerializeField] private ScriptableEventNoParam _onFightRaise;
        [SerializeField] private GameObject _healthBar;
        
        private Vector3 distance;
        private CharacterState _characterState;
        private HealthBar _healthBarScript;
        private Coroutine _attackCoroutine;
        private Vector3 _attackPosition;

        private void Awake()
        {
            GameObject _healthbar1 = Instantiate(_healthBar);
            _healthBarScript = _healthbar1.GetComponent<HealthBar>();
            _healthBarScript.Init(gameObject);
            _soapListPlayer.Add(this);
           
            
        }

        private void Start()
        {
            characterStats._health.Value = characterStats._maxHealth;
            _characterState = CharacterState.Idle;
            _soapListEnemy.OnItemRemoved += OnListNumberChanged;
        }

        private void OnDestroy()
        {
            _soapListEnemy.OnItemRemoved -= OnListNumberChanged;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_characterState == CharacterState.Idle)
            {
                var enemy = other.GetComponent<Enemy>();
                Timing.PauseCoroutines("playerMove");
                Timing.RunCoroutine(PlayerAttack(enemy), "playerAttack");
                distance = transform.position;
                Debug.Log("Attackkkk");
                _characterState = CharacterState.Attack;
            }

        }
       
        
        public void OnFight()
        {
            Timing.RunCoroutine(PlayerMove(), "playerMove");
        }
        private IEnumerator<float> PlayerAttack(Enemy enemy)
        {
            while (true)
            {
                Tween.Position(transform, _attackPosition, _tweenSettings)
                    .OnComplete(() => Tween.Position(transform, distance, _tweenSettings));
                Attack(enemy);
            
                yield return Timing.WaitForSeconds(2f);
                
            }
        }

        private void Attack(Enemy enemy)
        {
            enemy.TakeDamage(characterStats._damage);
        }
        

        private IEnumerator<float> PlayerMove()
        {
            while (true)
            {
                var closet = _soapListEnemy.GetClosest(transform.position);
                Move(closet.transform.position);
                _attackPosition = closet.transform.position;
                yield return Timing.WaitForOneFrame;
            }
        }
        public void TakeDamage(int damage)
        {
            characterStats._health.Value -= damage;
            Debug.Log( characterStats._health.Value+"Health");
            _healthBarScript.HealthBarSize(characterStats._maxHealth.Value,characterStats._health.Value);
        }
        public void OnListNumberChanged(Enemy enemy)
        {
            if (_soapListEnemy.Count == 0)
            {
                Debug.Log("Clear");
            }
        }

        protected override void Move(Vector3 target)
        {
            base.Move(target);
        }
    }
}