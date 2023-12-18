using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TungTran.Enemy;
using UnityEngine;

namespace TungTran.Player.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private FloatReference _speed;
        [SerializeField] private FloatReference _life;

        private void Start()
        {
            Destroy(gameObject,_life);
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }

        private IEnumerator<float> Move()
        {
            while (true)
            {
                var transform1 = transform;
                transform1.position += transform1.forward * _speed * Time.deltaTime;
                yield return 0;
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyOne>().Die();
                Destroy(gameObject);
            }
        }
        
        public void Init(Vector3 dir)
        {
            transform.forward = dir;
        }
    }
}
