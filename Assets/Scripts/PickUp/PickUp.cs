using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public event Action OnPickUp;
        public virtual void OnTriggerEnter(Collider other)
        {
            OnPickUp?.Invoke();
            Destroy(gameObject);
        }
}
