using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private FloatReference _speed;
        [SerializeField] private FloatReference _lifeTime;

        public void Init(Vector3 direction)
        {
            transform.forward = direction;
            Timing.RunCoroutine(DestroyProjectile(), Segment.SlowUpdate);
        }

        IEnumerator<float> DestroyProjectile()
        {
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                yield return Timing.WaitForSeconds(_lifeTime);
                Destroy(this);
            }
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            Debug.Log("Hit", enemy);
            enemy.Die();
            Destroy();
        }


        void Destroy() => Destroy(gameObject);
    }
}