using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class VfxSpawner : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private GameObject _spawnVfxPrefab;
    [SerializeField] private GameObject _destroyVfxPrefab;
    [SerializeField] private GameObject _expVfxPrefab;

    private void Awake()
    {
        _scriptableListEnemy.OnItemAdded += OnEnemySpawned;
        _scriptableListEnemy.OnItemRemoved += OnEnemyDied;
    }

    private void OnDestroy()
    {
        _scriptableListEnemy.OnItemAdded -= OnEnemySpawned;
        _scriptableListEnemy.OnItemRemoved -= OnEnemyDied;
    }

    private void OnEnemySpawned(Enemy obj)
    {
        Instantiate(_spawnVfxPrefab, obj.transform.position, Quaternion.identity);
    }
        
    private void OnEnemyDied(Enemy obj)
    {
        Instantiate(_destroyVfxPrefab, obj.transform.position, Quaternion.identity);
        Instantiate(_expVfxPrefab, obj.transform.position, Quaternion.identity);
    }
}
