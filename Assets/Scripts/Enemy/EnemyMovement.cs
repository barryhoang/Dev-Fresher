using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3Variable _playerPosition;

    // Update is called once per frame
    /*void Update()
    {
        var direction = (_playerPosition.Value - transform.position).normalized;
        transform.position += direction * Time.deltaTime * _speed;
    }*/

    private void Start()
    {
        Timing.RunCoroutine(_enemyMovement().CancelWith(gameObject));
    }

    IEnumerator<float> _enemyMovement()
    {
        while (true)
        {
            var direction = (_playerPosition.Value - transform.position).normalized;
            transform.position += direction * Time.deltaTime * _speed;
            yield return Timing.WaitForOneFrame;
        }
    }
}
