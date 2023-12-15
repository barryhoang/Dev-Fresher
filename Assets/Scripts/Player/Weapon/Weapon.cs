using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy_1 _listEnemy;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private FloatReference fireRate;

    private float startTime;

    private void Start()
    {
        if (transform.parent == null)
        {
            Debug.Log("null");
        }
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= startTime + 1f/fireRate)
        {
            startTime = Time.time;
            UpdateShootWeapon();
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
        var spawnPostion = transform.parent.position + dir * 0.5f;
        spawnPostion.y = transform.parent.position.y;
        var bullet = Instantiate(_bullet, spawnPostion, Quaternion.identity);
        bullet.Init(dir);
    }
}
