using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;

public class VfxSpawner : MonoBehaviour
{
    [SerializeField] private ScriptableListHero _scriptableListHero;
    [SerializeField] private GameObject _spawnVfxPrefab;
    [SerializeField] private GameObject _destroyVfxPrefab;

    private void Awake()
    {
        _scriptableListHero.OnItemAdded += OnHeroSpawned;
        _scriptableListHero.OnItemRemoved += OnHeroDied;
    }
    
    private void OnDestroy()
    {
        _scriptableListHero.OnItemAdded -= OnHeroSpawned;
        _scriptableListHero.OnItemRemoved -= OnHeroDied;
    }

    private void OnHeroDied(Hero obj)
    {
        Instantiate(_spawnVfxPrefab, obj.transform.position, Quaternion.identity);
    }

    private void OnHeroSpawned(Hero obj)
    {
        Instantiate(_destroyVfxPrefab, obj.transform.position, Quaternion.identity);
    }
}
