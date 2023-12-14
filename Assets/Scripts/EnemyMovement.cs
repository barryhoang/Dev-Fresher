using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Vector3Variable _playerPosition;
    
    void Update()
    {
        var direction = (_playerPosition.Value - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }
}
