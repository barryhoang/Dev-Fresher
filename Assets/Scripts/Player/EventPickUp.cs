using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class EventPickUp : PickUp
{
    [SerializeField] private ScriptableEventNoParam _onpickUp = null;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            _onpickUp.Raise();
            base.OnTriggerEnter(other);

        }
    }
}
