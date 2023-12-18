using Obvious.Soap;
using UnityEngine;

namespace TungTran.PickUp
{
    public class EventPickUp : PickUp
    {
        [SerializeField] private ScriptableEventNoParam _onpickUp = null;
        [SerializeField] private BoolVariable _isChess;
    
        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isChess.Value = true;
                _onpickUp.Raise();
                base.OnTriggerEnter(other);
            }

        }
    }
}
