using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TungTran.Player.Player;
using UnityEngine;

namespace TungTran.Enemy
{
    public class EnemyOne : MonoBehaviour
    {
        [SerializeField] private FloatVariable _moveSpeed;
        [SerializeField] private ScriptableListEnemyOne _listEnemy;
        [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;
        [SerializeField] private Vector3Variable _posPlayer;
        private Move _player;
        public int damage;
    
        private void Start()
        {
            _listEnemy.Add(this);
            _player = GameObject.FindWithTag("Player").GetComponent<Move>();
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }

        private IEnumerator<float> Move()
        {
            while (true)
            {
                var position = transform.position;
                var dir = _posPlayer.Value - position;
                dir.Normalize();
                position += dir*_moveSpeed*Time.deltaTime;
                transform.position = position;
                yield return 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _onEnemyHitPlayer.Raise(damage);
            _player.KnockBack(transform.position);
            Die();
        }

        public void Die()
        {
            _listEnemy.Remove(this);
            Destroy(gameObject);
        }
    }
}
