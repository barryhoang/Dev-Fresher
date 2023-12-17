using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minh
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Vector3Variable _playerPosition;
        [SerializeField] private Vector2 _spawnRange;
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private float _initalDelay = 1f;
        [SerializeField] private int _amount = 1;
        private float _currentAngle;
        private float _timer;
        private bool _isActive;

        private void Start()
        {
            Timing.RunCoroutine(StartSpawn(), Segment.SlowUpdate,"Spawn");
        }

        private void OnEnable()
        {
            Timing.ResumeCoroutines("Spawn");
        }

        private void OnDisable()
        {
            Timing.PauseCoroutines("Spawn");
        }
        // private IEnumerator Start()
        // {
        //     _timer = _spawnInterval;
        //     yield return new WaitForSeconds(_initalDelay);
        //     _isActive = true;
        // }

        private IEnumerator<float> StartSpawn()
        {
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                yield return Timing.WaitForSeconds(_initalDelay);
                while (true)
                {
                    for (int i = 0; i < _amount; i++)
                    {
                        Spawn();
                    }

                    yield return Timing.WaitForSeconds(_spawnInterval);
                }
            }
        }

        // private void Update()
        // {
        //          if (!_isActive)
        //          {
        //              return;
        //          }
        //  
        //          
        //          _timer += Time.deltaTime;
        //          if (_timer >= _spawnInterval)
        //          {
        //              for(int i=0;i<_amount;i++)
        //              Spawn();
        //              _timer = 0f;
        //          }
        // }

        void Spawn()
        {
            _currentAngle += 180f + Random.Range(-45, 45);
            var angleInRad = _currentAngle * Mathf.Deg2Rad;
            var range = Random.Range(_spawnRange.x, _spawnRange.y);
            var relativePosition = new Vector3(Mathf.Cos(angleInRad) * range, 0f, Mathf.Sin((angleInRad) * range));
            var spawnPosition = _playerPosition.Value + relativePosition;
            Instantiate(_prefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}