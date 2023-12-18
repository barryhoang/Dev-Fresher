using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using Random = UnityEngine.Random;
using MEC;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private int _amount = 1;
    [SerializeField] private float _initialDelay = 1;
    
    private float _currentAngle;
    private float _timer;
    private bool _isActive;

    void Start()
    {
        Timing.RunCoroutine(_spawn(), Segment.Update);
    }

    private IEnumerator<float> _spawn()
    {
        yield return Timing.WaitForSeconds(_initialDelay); 
        _isActive = true;
        while (_isActive)
        {
            for(var i = 0; i < _amount; i ++)
                Spawn();
            yield return Timing.WaitForSeconds(_initialDelay); 
        }
    }

    private void Spawn()
    {
        _currentAngle += 180f + Random.Range(-45, 45);
        var angleInRad = _currentAngle * Mathf.Deg2Rad;
        var range = Random.Range(_spawnRange.x, _spawnRange.y);
        var relativePosition = new Vector3(Mathf.Cos(angleInRad)*range,0f,Mathf.Sin(angleInRad)*range);
        var spawnPosition = _playerPosition.Value + relativePosition;
        Instantiate(_prefab, spawnPosition, Quaternion.identity, transform);
        
    }
}
