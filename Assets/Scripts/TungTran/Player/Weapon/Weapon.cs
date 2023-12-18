using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TungTran.Enemy;
using UnityEngine;

namespace TungTran.Player.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemyOne _listEnemy;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private FloatReference fireRate;

        private void Start()
        {
            if (transform.parent == null)
            {
                Debug.Log("null");
            }

            Timing.RunCoroutine(FireRateBullet());

        }
        private IEnumerator<float> FireRateBullet()
        {
            while (true)
            {
                UpdateShootWeapon();
                yield return Timing.WaitForSeconds(1f/fireRate);
            }
        }

        private void UpdateShootWeapon()
        {
            var closet = _listEnemy.GetClosest(transform.position);
            if (closet != null)
            {
                var direction = closet.transform.position - transform.parent.position;
                direction.Normalize();
                SpawnBullet(direction);
            }
        }

        private void SpawnBullet(Vector3 dir)
        {
            var position = transform.parent.position;
            var spawnPosition = position + dir * 0.5f;
            spawnPosition.y = position.y;
            var bullet = Instantiate(_bullet, spawnPosition, Quaternion.identity);
            bullet.Init(dir);
        }
    }
}
