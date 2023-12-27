using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3Variable _playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = (_playerPosition.Value - transform.position).normalized;
        transform.position += direction * Time.deltaTime * _speed;
    }
}
