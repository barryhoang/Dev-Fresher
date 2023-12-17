using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
        [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;

        private void Start()
        {
            _scriptableListEnemy.Add(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //other.GetComponent<PlayerHealth>().TakeDamage(30);
                //_playerHealth.Add(-30);
                _onEnemyHitPlayer.Raise(30);
                Die();
            }
        }

        public void Die()
        {
            _scriptableListEnemy.Remove(this);
            Destroy(gameObject);
        }
    }
}