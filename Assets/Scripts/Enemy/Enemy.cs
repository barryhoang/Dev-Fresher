using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;

    private void Start()
    {
        _scriptableListEnemy.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die();
            _onEnemyHitPlayer.Raise(30);
        }
    }

    public void Die()
    {
        _scriptableListEnemy.Remove(this);
        Destroy(gameObject);
    }
}
