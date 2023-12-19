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
        private Vector3 distance;
        private CharacterState _characterState;
       
        private Coroutine _attackCoroutine;
        private Vector3 _attackPosition;

        private void Awake()
        {
            _soapListPlayer.Add(this);
            _characterState = CharacterState.Idle;
        }

        private void Start()
        {
            
            Timing.RunCoroutine(PlayerMove(), "playerMove");
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


        protected override void Move(Vector3 target)
        {
            base.Move(target);
        }
    }
}