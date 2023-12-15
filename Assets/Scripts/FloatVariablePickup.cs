using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using Unity.VisualScripting;

public class FloatVariablePickup : Pickup
{
    [SerializeField] private FloatVariable _floatVariable;
    [SerializeField] private FloatReference _value;
    [SerializeField] private FloatReference _multiplier;

    protected override void OntriggerEnter(Collider other)
    {
        _floatVariable.Add(_value *_multiplier);
            Debug.Log('A');
            base.OnTriggerEnter(other);
    }
}
