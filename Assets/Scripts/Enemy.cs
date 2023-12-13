using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private FloatVariable _playerHealth;
    //[SerializeField] private ScriptableEventInt _onEnemyHitPlayer;

    private void Start()
    {
        _scriptableListEnemy.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(10);
            Die();
            //_playerHealth.Add(-30);
            //_onEnemyHitPlayer.Raise(30);
        }
    }

    private void Die()
    {
        _scriptableListEnemy.Remove(this);
        Destroy(gameObject);
    }
}
