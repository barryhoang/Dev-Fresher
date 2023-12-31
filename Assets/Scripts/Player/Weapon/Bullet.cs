
using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private FloatReference _speed;
    [SerializeField] private FloatReference _life;

    public void Init(Vector3 dir)
    {
        transform.forward = dir;
    }

    public void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

     void Destroy() => Destroy(gameObject, _life);
    
}
