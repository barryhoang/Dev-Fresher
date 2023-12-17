using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using Obvious.Soap.Example;

public class Shield : MonoBehaviour
{
    [SerializeField] FloatReference _speed;
    [SerializeField] FloatReference _speedMultiplier;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.Die();
    }

    void Update()
    {
        transform.RotateAround(transform.parent.transform.position,Vector3.up, _speed.Value*_speedMultiplier.Value*Time.deltaTime);
    }
}
