using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class FloatVariablePickup : Pickup
    {
        [SerializeField] protected FloatVariable _floatVariable;

        [SerializeField] protected FloatReference _value;

        [SerializeField] protected FloatReference _multiplier;

        // Start is called before the first frame update
        protected override void OnTriggerEnter(Collider other)
        {
            _floatVariable.Add(_value * _multiplier);
            base.OnTriggerEnter(other);
        }
    }
}