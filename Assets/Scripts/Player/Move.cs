using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Vector3Variable _input;
    [SerializeField] private FloatVariable _moveSpeed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        rb.velocity = _input.Value * _moveSpeed;
    }
}
