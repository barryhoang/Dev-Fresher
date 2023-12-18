using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using MEC;

namespace Minh
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Vector3Variable _playerPosition;

        private void Start()
        {
            Timing.RunCoroutine(EnemyMove());
        }

        private IEnumerator<float> EnemyMove()
        {
            while (true)
            {
                var transform1 = transform;
                var position = transform1.position;
                var direction = (_playerPosition.Value - position).normalized;
                position += direction * (_speed * Time.deltaTime);
                transform1.position = position;
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}