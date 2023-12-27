using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3Variable _input;
    [SerializeField] private float _speed = 5.0f;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += _input.Value * _speed * Time.deltaTime;
    }
}
