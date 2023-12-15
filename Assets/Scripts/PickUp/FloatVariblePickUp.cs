using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class FloatVariblePickUp : PickUp
{
    [SerializeField] private FloatVariable _floatVariable;
    [SerializeField] private FloatReference _value;
    [SerializeField] private FloatReference _multipe;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _floatVariable.Add(_value * _multipe);
            base.OnTriggerEnter(other);
        }
    }
}
