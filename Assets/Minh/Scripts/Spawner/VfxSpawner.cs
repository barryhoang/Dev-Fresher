using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class VfxSpawner : MonoBehaviour
    {
        [SerializeField] private ScriptableListPlayer _scriptableListPlayer;

        [SerializeField] private ScriptableListEnemy _scriptableListEnemy;

        [SerializeField] private GameObject _attackVfxPrefab;
        // Start is called before the first frame update
        public void SpawnAttackVFX(Transform transform)
        {
            Instantiate(_attackVfxPrefab, transform.position, Quaternion.identity);
        }
    } 
}

