using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private FloatReference _speed;
    [SerializeField] private FloatReference _lifeTime;

    public void Init(Vector3 direction)
    {
        transform.forward = direction;
        Invoke(nameof(Destroy), _lifeTime);
    } 
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.Die();
        Destroy();
    }

    void Destroy() => Destroy(gameObject);
}
