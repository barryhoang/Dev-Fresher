using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class EventPickUp : PickUp
{
    [SerializeField] private ScriptableEventNoParam _onpickUp = null;
    [SerializeField] private BoolVariable _isChess;
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isChess.Value = true;
            _onpickUp.Raise();
            base.OnTriggerEnter(other);
        }

    }
}
