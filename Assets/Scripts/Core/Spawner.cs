using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private float _initialDelay = 1f;
    [SerializeField] private int _amount = 1;
    private float _currentAngle;
    private float _timer;
    private bool _isActive;

    private void Start()
    {
        Timing.RunCoroutine(_Start());
    }

    private IEnumerator<float> _Start()
    {
        _timer = _spawnInterval;
        yield return Timing.WaitForSeconds(_initialDelay);
        _isActive = true;
        
        while (_isActive)
        {
            for (int i = 0; i < _amount; i++)
            {
                Spawn();
            }

            yield return Timing.WaitForSeconds(_spawnInterval);
        }
    }

    /*private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _initialDelay)
        {
            for (int i = 0; i < _amount; i++)
            {
                Spawn();
                _timer = 0f;
            }
        }
    }
    */

    void Spawn()
    {
        _currentAngle += 180f + Random.Range(-45, 45);
        var angleInRad = _currentAngle + Mathf.Deg2Rad;
        var range = Random.Range(_spawnRange.x, _spawnRange.y);
        var relativePosition = new Vector3(Mathf.Cos(angleInRad) * range,
            0f,
            Mathf.Sin(angleInRad) * range);
        var spawnPosition = _playerPosition.Value + relativePosition;
        Instantiate(_prefab, spawnPosition, Quaternion.identity, transform);
    }
    
    private void StopSpawning()
    {
        _isActive = false;
    }
}
