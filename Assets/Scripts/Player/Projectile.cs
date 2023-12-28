using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
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
    
    private void Start()
    {
        Timing.RunCoroutine(_MoveProjectile().CancelWith(gameObject));
    }
    
    /*private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }*/

    private IEnumerator<float> _MoveProjectile()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _lifeTime)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.Die();
        Destroy();
    }

    void Destroy() => Destroy(gameObject);
}
