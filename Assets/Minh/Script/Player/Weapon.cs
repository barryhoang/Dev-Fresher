using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private FloatReference _fireRate;
        
        private Transform _ownerTransform;
        private float _timer;

        private void Awake()
        {
            _ownerTransform = transform.parent == null ? transform : transform.parent;
        }

        private void Start()
        {
            Timing.RunCoroutine(CheckForShooting());
        }

       private IEnumerator<float> CheckForShooting()
        {
            while (true)
            {
                if (gameObject != null && gameObject.activeInHierarchy)
                {
                    ShootAtClosestEnemy();
                    yield return Timing.WaitForSeconds(1 / _fireRate);
                }
            }
        }
        private void ShootAtClosestEnemy()
        {
            //find closest enemy;
            var closest = _soapListEnemy.GetClosest(transform.position);
            if (closest != null)
            {
                var direction = closest.transform.position - _ownerTransform.position;
                SpawnProjectile(direction.normalized);
            }
        }

        private void SpawnProjectile(Vector3 directionNormalized)
        {
            var spawnpoint = _ownerTransform.position + directionNormalized * 0.5f;
            spawnpoint.y = 1f;
            var projectile = Instantiate(_projectilePrefab, spawnpoint, Quaternion.identity);
            projectile.Init(directionNormalized);
        }
    }

   
}