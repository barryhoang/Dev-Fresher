using UnityEngine;

namespace Minh
{
    public class VfxSpawner : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy _soapListEnemy;
        [SerializeField] private GameObject _spawnVfxPrefab;
        [SerializeField] private GameObject _destroyVfxPrefab;
        [SerializeField] private GameObject _expPickUpPrefab;

        private void Awake()
        {
            _soapListEnemy.OnItemAdded += OnEnemySpawned;
            _soapListEnemy.OnItemRemoved += OnEnemyDied;
        }

        private void OnDestroy()
        {
            _soapListEnemy.OnItemAdded -= OnEnemySpawned;
            _soapListEnemy.OnItemRemoved -= OnEnemyDied;
        }

        private void OnEnemyDied(Enemy obj)
        {
            Instantiate(_spawnVfxPrefab, obj.transform.position, Quaternion.identity);
            Instantiate(_expPickUpPrefab, obj.transform.position, Quaternion.identity);
        }

        private void OnEnemySpawned(Enemy obj)
        {
            Instantiate(_destroyVfxPrefab, obj.transform.position, Quaternion.identity);
        }
    }
}