using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;
using MEC;
using UnityEditor.Playables;
using UnityEngine.Diagnostics;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3Variable _playerPosition;

    /*public IEnumerator<Vector3> _EMUpdate()
    {
        var direction = (_playerPosition.Value - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
        yield return transform.position;
    }*/
    
    void Update()
    {
        var direction = (_playerPosition.Value - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }
}
