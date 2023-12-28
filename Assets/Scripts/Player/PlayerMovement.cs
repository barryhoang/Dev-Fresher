using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3Variable _input;
    [SerializeField] private TransformVariable _playerTransform;
    [SerializeField] private FloatReference _speedMultiplier;
    [SerializeField] private float _speed = 5f;

    private void Awake()
    {
        _playerTransform.Value = transform;
    }

    void Update()
    {
        transform.position += _input.Value * _speed * Time.deltaTime * _speedMultiplier;
    }
}
