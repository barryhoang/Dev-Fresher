using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class vfx_Spawn : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy_1 _listEnemy;
    [SerializeField] private GameObject _vfxSpawn;
    [SerializeField] private GameObject _vfxDestroy;
    [SerializeField] private GameObject _exp = null;

    private void Awake()
    {
        _listEnemy.OnItemAdded += OnEnemySpawn;
        _listEnemy.OnItemAdded += OnEnemyDestroy;
    }

    private void OnDestroy()
    {
        _listEnemy.OnItemAdded -= OnEnemySpawn;
        _listEnemy.OnItemAdded -= OnEnemyDestroy;
    }

    private void OnEnemyDestroy(Enemy_1 obj)
    {
        Instantiate(_vfxDestroy, obj.transform.position, Quaternion.identity);
    }
    private void OnEnemySpawn(Enemy_1 obj)
    {
        Instantiate(_vfxSpawn, obj.transform.position, Quaternion.identity);
    }
}
