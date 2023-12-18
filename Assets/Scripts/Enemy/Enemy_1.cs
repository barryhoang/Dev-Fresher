using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    [SerializeField] private FloatVariable _moveSpeed;
    [SerializeField] private ScriptableListEnemy_1 _listEnemy;
    [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;
    [SerializeField] private Vector3Variable _posPlayer;
    private Move _player;
     public int dame;
    
    private void Start()
    {
        _listEnemy.Add(this);
        _player = GameObject.FindWithTag("Player").GetComponent<Move>();
        Timing.RunCoroutine(Move().CancelWith(gameObject));
    }

    private void Update()
    {
    }

    private IEnumerator<float> Move()
    {
        while (true)
        {
            var position = transform.position;
            var dir = _posPlayer.Value - position;
            dir.Normalize();
            position += dir*_moveSpeed*Time.deltaTime;
            transform.position = position;
            yield return 0;
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onEnemyHitPlayer.Raise(30);
            _player.KnockBack(transform.position);
            Die();
        }
    }

    public void Die()
    {
        _listEnemy.Remove(this);
        Destroy(gameObject);
    }
}
