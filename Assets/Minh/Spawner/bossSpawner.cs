using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class bossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private int _amount = 1;
    private float _currentAngle;
    // Start is called before the first frame update
    public void playerOnTrap()
    {
        Timing.CallDelayed(5f, delegate { Timing.RunCoroutine(SpawnEnemy()); });
    }

    IEnumerator<float> SpawnEnemy()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            for (int i = 0; i < _amount; i++)
            {
                Spawn();
            }

            yield return Timing.WaitForOneFrame;
        }
    }
    void Spawn()
    {
        _currentAngle += 180f + Random.Range(-45, 45);
        var angleInRad = _currentAngle * Mathf.Deg2Rad;
        var range = Random.Range(_spawnRange.x, _spawnRange.y);
        var relativePosition=new Vector3(Mathf.Cos(angleInRad)*range,0f,Mathf.Sin((angleInRad)*range));
        var spawnPosition = _playerPosition.Value + relativePosition;
        Instantiate(_prefab, spawnPosition, Quaternion.identity, transform);
    }
}
