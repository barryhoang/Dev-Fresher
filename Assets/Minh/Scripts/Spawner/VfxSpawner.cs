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
        [SerializeField] private GameObject _EnemyVfxPrefab;
        // Start is called before the first frame update
        public void SpawnAttackVFX(Transform position, float degree)
        {
            Instantiate(_attackVfxPrefab, position.position, Quaternion.Euler(0,0,degree-90));
        }
        public void EnemySpawnAttackVFX(Transform position, float degree)
        {
            Instantiate(_EnemyVfxPrefab, position.position, Quaternion.Euler(0,-0,degree-90));
        }
    } 
}

