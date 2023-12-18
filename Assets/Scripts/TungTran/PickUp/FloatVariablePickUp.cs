using Obvious.Soap;
using UnityEngine;

namespace TungTran.PickUp
{
    public class FloatVariablePickUp : PickUp
    {
        [SerializeField] private FloatVariable _floatVariable;
        [SerializeField] private FloatReference _value;
        [SerializeField] private FloatReference _multiple;
        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _floatVariable.Add(_value * _multiple);
                base.OnTriggerEnter(other);
            }
        }
    }
}
