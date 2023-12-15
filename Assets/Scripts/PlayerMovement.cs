using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3Variable _inputs;
    [SerializeField] private TransformVariable _playerTransform;
    [SerializeField] public float _speed;
    [SerializeField] private FloatReference _speedMultiplier;

    private void Awake()
    {
        _playerTransform.Value = transform;
    }

    void Update()
    {
        transform.position += _inputs.Value * _speed * _speedMultiplier * Time.deltaTime;
    }
}
