using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

namespace Tung
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager instance { get; private set; }
        
        [SerializeField] private ScriptableListCharacter _listSoapCharacter;
        [SerializeField] private ScriptableListEnemy _listSoapEnemies;

        [SerializeField] private List<Character> _listCharacters;
        [SerializeField] private List<Enemy> _listEnemies;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            AddEntity();
        }

        private void AddEntity()
        {
            foreach (var entity in _listCharacters)
            {
                _listSoapCharacter.Add(entity);
            }

            foreach (var entity in _listEnemies)
            {
                _listSoapEnemies.Add(entity);
            }
        }

        public bool CheckEnemiesDie()
        {
            return _listSoapEnemies.IsEmpty;
        }

        public void CheckEntityDie()
        {
            foreach (var entity in _listCharacters)
            {
                if (!entity.gameObject.activeInHierarchy)
                {
                    _listSoapCharacter.Remove(entity);
                }
            }

            foreach (var entity in _listEnemies)
            {
                if (!entity.gameObject.activeInHierarchy)
                {
                    _listSoapEnemies.Remove(entity);
                }
            }
        }
    }
}
