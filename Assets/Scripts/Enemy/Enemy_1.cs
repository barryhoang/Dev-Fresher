using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    [SerializeField] private FloatVariable _moveSpeed;
    [SerializeField] private ScriptableListEnemy_1 _listEnemy;
    [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;
    [SerializeField] private Vector3Variable _posPlayer;

     public int dame;
    
    private void Start()
    {
        _listEnemy.Add(this);
       
    }

    private void Update()
    {
        Vector3 dir = _posPlayer.Value - transform.position;
        dir.Normalize();
        transform.position += dir*_moveSpeed*Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _onEnemyHitPlayer.Raise(30);
            other.GetComponent<Move>().knockBack(transform.position);
            Die();
        }
    }

    public void Die()
    {
        _listEnemy.Remove(this);
        Destroy(gameObject);
    }
}
