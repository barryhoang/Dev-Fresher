using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;

        private void Start()
        {
            _soapListEnemy.Add(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            //other.GetComponent<PlayerHealth>().TakeDamage(30);
            //_playerHealth.Add(-30);
            _onEnemyHitPlayer.Raise(30);
            Die();
        }

        public void Die()
        {
            _soapListEnemy.Remove(this);
            Destroy(gameObject);
        }
    }
}