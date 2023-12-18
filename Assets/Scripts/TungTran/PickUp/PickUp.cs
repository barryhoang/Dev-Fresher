using System;
using UnityEngine;

namespace TungTran.PickUp
{
    public abstract class PickUp : MonoBehaviour
    {
        public event Action OnPickUp;
        public virtual void OnTriggerEnter(Collider other)
        {
            OnPickUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
