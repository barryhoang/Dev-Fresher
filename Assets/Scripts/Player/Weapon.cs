using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
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
        Timing.RunCoroutine(_ShootCoroutine());
    }
    
    /*private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f/ _fireRate)
        {
            ShootAtClosestEnemy();
            _timer = 0f;
        }
    }*/
    
    private IEnumerator<float> _ShootCoroutine()
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(1f / _fireRate.Value);

            ShootAtClosestEnemy();
        }
    }

    private void ShootAtClosestEnemy()
    {
        //find closest enemy
        var closest = _scriptableListEnemy.GetClosest(transform.position);
        if (closest != null)
        {
            var direction = closest.transform.position - _ownerTransform.position;
            SpawnProjectile(direction.normalized);
        }
    }

    private void SpawnProjectile(Vector3 directionNormalized)
    {
        var spawnPoint = _ownerTransform.position + directionNormalized * 0.5f;
        spawnPoint.y = 1f;
        var projectile = Instantiate(_projectilePrefab, spawnPoint, Quaternion.identity);
        projectile.Init(directionNormalized);
    }
}
