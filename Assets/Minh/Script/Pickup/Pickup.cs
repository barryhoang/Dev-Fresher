using System;
using UnityEngine;

namespace Minh
{
    public abstract class Pickup : MonoBehaviour
    {
        public Action OnPickedup;

        protected virtual void OnTriggerEnter(Collider other)
        {
            OnPickedup?.Invoke();
            Destroy(gameObject);
        }
    }
}