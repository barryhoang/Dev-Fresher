using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class EventPickUp : Pickup
    {
        [SerializeField] private ScriptableEventNoParam _onPickedUpEvent;


        protected override void OnTriggerEnter(Collider other)
        {
            _onPickedUpEvent.Raise();
            base.OnTriggerEnter(other);
        }
    }
}