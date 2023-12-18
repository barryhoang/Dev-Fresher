using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private FloatReference _speed;
    [SerializeField] private FloatReference _lifetime;

    private void Start()
    {
        Invoke(nameof(Destroy), _lifetime);
    }

    void Update()
    {
        var bulletTransform = transform;
        bulletTransform.position += bulletTransform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.Die();
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    
    public void Init(Vector3 direction)
    {
        transform.forward = direction;
    }

}