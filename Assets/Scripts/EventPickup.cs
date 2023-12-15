using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class EventPickup : Pickup
{
    [SerializeField] private ScriptableEventNoParam _onPickedUpEvent = null;
    protected override void OntriggerEnter(Collider other)
    {
        _onPickedUpEvent.Raise();
    }
}
