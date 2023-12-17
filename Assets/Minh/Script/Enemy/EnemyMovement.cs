using Obvious.Soap;
using UnityEngine;

namespace Minh
{
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
            var transform1 = transform;
            var position = transform1.position;
            var direction = (_playerPosition.Value - position).normalized;
            position += direction * (_speed * Time.deltaTime);
            transform1.position = position;
        }
    }
}