using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private ScriptableEventInt _onCollide;

    private void Start()
    {
        _scriptableListEnemy.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CompareTag("Player")) return;
        _onCollide.Raise(10);
        Die();
    }

    private void Die()
    {
        _scriptableListEnemy.Remove(this);
        Destroy(gameObject);
    }
}
