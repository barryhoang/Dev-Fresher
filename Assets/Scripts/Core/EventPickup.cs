using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class EventPickup : Pickup
{
    [SerializeField] private ScriptableEventNoParam _onPickUpEvent = null;

    protected override void OnTriggerEnter(Collider other)
    {
        _onPickUpEvent.Raise();
        base.OnTriggerEnter(other);
    }
}
