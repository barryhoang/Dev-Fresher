using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class FloatVariablePickup : Pickup
{
    [SerializeField] protected FloatVariable _floatVariable;

    [SerializeField] protected FloatReference _value;

    [SerializeField] protected FloatReference _multiplier;
    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other)
    {
        _floatVariable.Add(_value*_multiplier);
        base.OnTriggerEnter(other);
    }
}
