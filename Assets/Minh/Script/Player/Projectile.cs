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

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            Debug.Log("Hit", enemy);
            enemy.Die();
            Destroy();
        }

        private void Start()
        {
            Timing.RunCoroutine(FireProjectile());
        }

        public void Init(Vector3 direction)
        {
            transform.forward = direction;
            Timing.RunCoroutine(DestroyProjectile(), Segment.SlowUpdate);
        }

        private IEnumerator<float> FireProjectile()
        {
            var transform1 = transform;
            transform1.position += transform1.forward * _speed * Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }

        private IEnumerator<float> DestroyProjectile()
        {
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                yield return Timing.WaitForSeconds(_lifeTime);
                Destroy(this);
            }
        }

        void Destroy() => Destroy(gameObject);
    }
}