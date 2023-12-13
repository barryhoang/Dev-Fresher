
using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using Obvious.Soap.Example;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Vector3Variable _playerPostion;
        [SerializeField] private Vector2 _spawnRange;
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private float _initiaDelay = 1f;
        [SerializeField] private int _amount = 1;

        private float _currentAngle;
        private float _timer;
        private bool _isActive;

        private IEnumerator Start()
        {
                _timer = _spawnInterval;
                yield return new WaitForSeconds(_initiaDelay);
                _isActive = true;
        }

        private void Update()
        {
                if (!_isActive)
                        return;
                _timer += Time.deltaTime;
                if (_timer >= _spawnInterval)
                {
                        for (int i = 0; i < _amount; i++)
                                Spawner();
                        _timer = 0f;
                }
        }

        private void Spawner()
        {
                _currentAngle = 180f + Random.Range(-45, 45);
                var angleRad = _currentAngle * Mathf.Deg2Rad;
                var range = Random.Range(_spawnRange.x, _spawnRange.y);
                var raletivePosition = new Vector3(Mathf.Cos(angleRad) * range,0f,  Mathf.Sin(angleRad) * range);
                var spawnPostion = _playerPostion.Value + raletivePosition;
                Instantiate(enemyPrefab, spawnPostion, quaternion.identity, transform);
        }
}
