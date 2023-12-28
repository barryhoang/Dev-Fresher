using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private FloatReference _speed;
    [SerializeField] private FloatReference _speedMultiplyer;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.Die();
    }

    private void Start()
    {
        Timing.RunCoroutine(_RotateAround());
    }

    /*private void Update()
    {
        transform.RotateAround(transform.parent.transform.position, Vector3.up, 
            _speed.Value * _speedMultiplyer.Value * Time.deltaTime);
    }*/

    IEnumerator<float> _RotateAround()
    {
        while (true)
        {
            transform.RotateAround(transform.parent.transform.position, Vector3.up, 
                        _speed.Value * _speedMultiplyer.Value * Time.deltaTime);
            yield return Timing.WaitForOneFrame;
        }
    } 
}
