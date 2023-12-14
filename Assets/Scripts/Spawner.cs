using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private int _amount = 1;
    [SerializeField] private float _initialDelay = 1;
    private float _currentAngle;
    private float _timer;
    private bool _isActive;

    private IEnumerator Start()
    {
        _timer = _spawnInterval;
        yield return new WaitForSeconds(_initialDelay); 
        _isActive = true;
    }

    private void Update()
    {
        if (!_isActive) return;
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            for(int i = 0; i < _amount; i ++)
                Spawn();
            _timer = 0f;
        }
    }

    void Spawn()
    {
        _currentAngle += 180f + Random.Range(-45, 45);
        var angleInRad = _currentAngle * Mathf.Deg2Rad;
        var range = Random.Range(_spawnRange.x, _spawnRange.y);
        var relativePosition = new Vector3(Mathf.Cos(angleInRad)*range,0f,Mathf.Sin(angleInRad)*range);
        var spawnPosition = _playerPosition.Value + relativePosition;
        Instantiate(_prefab, spawnPosition, Quaternion.identity, transform);
        
    }
}