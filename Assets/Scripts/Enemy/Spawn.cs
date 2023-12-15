using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Unity.Mathematics;
using UnityEngine;

public class Spawn : MonoBehaviour
{
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float _delay;
        [SerializeField] private float _initiaDelay = 3f;
        [SerializeField] private int _amount = 1;
        [SerializeField] private Vector3Variable _playerPostion;
        [SerializeField] private float distance = 5f;

        private float _currentAngle;
        
        private void Start()
        { 
                Timing.CallDelayed(_delay, delegate { startSpawn(); });
        }
        public void startSpawn()
        {
                Timing.RunCoroutine(Spawning(),"Spawn");
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
                Vector3 spawnPostion = new Vector3(_playerPostion.Value.x + distance*Mathf.Cos(Mathf.Deg2Rad*_currentAngle),_playerPostion.Value.y,_playerPostion.Value.z+distance*Mathf.Sin(Mathf.Deg2Rad*_currentAngle));
                Instantiate(enemyPrefab,spawnPostion, quaternion.identity, transform);
        }
}
