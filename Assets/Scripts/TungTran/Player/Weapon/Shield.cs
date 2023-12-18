using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using TungTran.Enemy;
using UnityEngine;

namespace TungTran.Player.Weapon
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private FloatReference _speed;
        [SerializeField] private FloatReference _speedMultipier;
        [SerializeField] private TransformVariable _playerTranform;
        [SerializeField] private float distance = 5f;

        private float _angle = 0f;
        private float _offsetAngle = 45f;

        private void Start()
        {
            Timing.RunCoroutine(MoveShield().CancelWith(gameObject));
        }

        private void Update()
        {
            
        }

        private IEnumerator<float> MoveShield()
        {
            while (true)
            {
                var position = _playerTranform.Value.position;
                var newPosition = new Vector3(
                    position.x + distance * Mathf.Cos(Mathf.Deg2Rad * (_angle + _offsetAngle)),
                    position.y,
                    position.z + distance * Mathf.Sin(Mathf.Deg2Rad * (_angle + _offsetAngle))
                );
                transform.position = newPosition;
                _angle += _speed * _speedMultipier * Time.deltaTime;
                if (_angle >= 360f)
                {
                    _angle -= 360f;
                }
                yield return 0;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<EnemyOne>();
                enemy.Die();
            }
        }
       
    }
}
