using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class EventPickup : Pickup
{
    [SerializeField] private ScriptableEventNoParam _onPickedUpEvent = null;
    protected override void OnTriggerEnter(Collider other)
    {
        _onPickedUpEvent.Raise();
        base.OnTriggerEnter(other);
    }
}
