using Obvious.Soap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPickUp : Pickup
{
    [SerializeField] private ScriptableEventNoParam _onPickedUpEvent;
    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other)
    {
        _onPickedUpEvent.Raise();
        base.OnTriggerEnter(other);
    }
}
