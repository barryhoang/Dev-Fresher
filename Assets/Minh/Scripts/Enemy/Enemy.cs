using System;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using MEC;
namespace Minh
{
    public class Enemy : CharacterStats
    {
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableListPlayer _soapListPlayer;

        public GameObject enemyGameObject;
        private string _gameObjectID;
       
        private void Awake()
        {
            _soapListEnemy.Add(this);
            _gameObjectID = enemyGameObject.GetInstanceID().ToString();
            
        }

        private void Start()
        {
            
            Timing.RunCoroutine(EnemyMove(),_gameObjectID);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
           
                Debug.Log("Trigger");
                Timing.PauseCoroutines(_gameObjectID);
           
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Timing.PauseCoroutines(_gameObjectID);
        }
       
        private IEnumerator<float> EnemyMove()
        {
            
            while (true)
            {
                var closet = _soapListPlayer.GetClosest(transform.position);
                Move(closet.transform.position);
               
                yield return Timing.WaitForOneFrame;
            }
        }

        public void TakeDamage(int damage)
        {
           characterStats._health.Value -= damage;
            Debug.Log( characterStats._health.Value+"Health");
        }

       
        protected override void Move(Vector3 target)
        {
            
            base.Move(target);
        }
    }
}