using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.Mathematics;
using UnityEngine;

namespace TungTran.Wave
{
        public class Spawn : MonoBehaviour
        {
                [SerializeField] private GameObject enemyPrefab;
                [SerializeField] private float _delay;
                [SerializeField] private float _initiaDelay = 3f;
                [SerializeField] private int _amount = 1;
                [SerializeField] private Vector3Variable _playerPostion;
                [SerializeField] private float _distance = 5f;

                private float _currentAngle;
        
                private void Start()
                {
                        Timing.CallDelayed(_delay, StartSpawn);
                }
                public void StartSpawn()
                {
                        Timing.RunCoroutine(Spawning().CancelWith(gameObject));
                }
                public void StopSpawn()
                {
                        Timing.KillCoroutines(Spawning());
                }
                public IEnumerator<float> Spawning()
                {
                        while (true)
                        {
                                for (int i = 0; i < _amount; i++)
                                        Spawner();
                                yield return  Timing.WaitForSeconds(_initiaDelay);
                        }
                        
                }
                private void Spawner()
                {
                        _currentAngle = UnityEngine.Random.Range(0,361); 
                        var spawnPosition = new Vector3(_playerPostion.Value.x + _distance*Mathf.Cos(Mathf.Deg2Rad*_currentAngle),_playerPostion.Value.y,_playerPostion.Value.z+_distance*Mathf.Sin(Mathf.Deg2Rad*_currentAngle));
                        Instantiate(enemyPrefab,spawnPosition, quaternion.identity, transform);
                }
        }
}
