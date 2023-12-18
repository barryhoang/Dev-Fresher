using TungTran.Enemy;
using UnityEngine;

namespace TungTran.Wave
{
    public class VfxSpawn : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemyOne _listEnemy;
        [SerializeField] private GameObject _vfxSpawn;
        [SerializeField] private GameObject _vfxDestroy;
        [SerializeField] private GameObject _exp;

        private void Awake()
        {
        
            _listEnemy.OnItemAdded += OnEnemySpawn;
            _listEnemy.OnItemRemoved += OnEnemyDestroy;
        }

        private void OnDestroy()
        {
            _listEnemy.OnItemAdded -= OnEnemySpawn;
            _listEnemy.OnItemRemoved -= OnEnemyDestroy;
        }


        private void OnEnemySpawn(EnemyOne obj)
        {
            Instantiate(_vfxSpawn, obj.transform.position, Quaternion.identity);
        }

        private void OnEnemyDestroy(EnemyOne obj)
        {
            var position = obj.transform.position;
            Instantiate(_vfxDestroy, position, Quaternion.identity);
            Instantiate(_exp, position, Quaternion.identity);
        }
    }
}