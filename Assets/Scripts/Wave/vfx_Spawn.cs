using Enemy;
using Obvious.Soap;
using UnityEngine;

namespace Wave
{
    public class vfx_Spawn : MonoBehaviour
    {
        [SerializeField] private ScriptableListEnemy_1 _listEnemy;
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


        private void OnEnemySpawn(Enemy_1 obj)
        {
            Instantiate(_vfxSpawn, obj.transform.position, Quaternion.identity);
        }

        private void OnEnemyDestroy(Enemy_1 obj)
        {
            Instantiate(_vfxDestroy, obj.transform.position, Quaternion.identity);
            Instantiate(_exp, obj.transform.position, Quaternion.identity);
        }
    }
}