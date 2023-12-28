using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public event Action OnPickedup;
    protected virtual void OnTriggerEnter(Collider other)
    {
        OnPickedup?.Invoke();
        Destroy(gameObject);
    }
}
