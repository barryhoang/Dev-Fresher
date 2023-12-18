using UnityEngine;
using Obvious.Soap;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3Variable _playerPosition;
    
    void Update()
    {
        var newpos = transform;
        var position = newpos.position;
        var direction = (_playerPosition.Value - position).normalized;
        position += direction * (_speed * Time.deltaTime);
        newpos.position = position;
    }
}
