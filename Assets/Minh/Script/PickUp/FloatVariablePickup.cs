using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class FloatVariablePickup : Pickup
{
    [SerializeField] private FloatVariable _floatVariable;

    [SerializeField] private FloatReference _value;
    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other)
    {
        _floatVariable.Add(_value);
        base.OnTriggerEnter(other);
    }
}
