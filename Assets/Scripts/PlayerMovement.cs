using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3Variable _inputs;
    [SerializeField] private float _speed = 5f;
    
    void Update()
    {
        transform.position += _inputs.Value * _speed * Time.deltaTime;
    }
}
