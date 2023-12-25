using System.Collections;
using System.Collections.Generic;
using Entity;
using MEC;
using UnityEngine;

namespace StateManager
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager instance { get; private set; }
        [SerializeField] private ScriptableListCharacter _listSoapcharacters;
        [SerializeField] private ScriptableListEnemy _listSoapEnemy;
        [SerializeField] private List<Character> _character;
        [SerializeField] private List<Enemy> _enemies;
        
        private void Awake()
        {
            instance = this;
        }

        public void Spawn(int numberEnemy)
        {
            foreach (var entity in _character)
            {
                _listSoapcharacters.Add(entity);
                entity.ResetPosAndState();
                entity.gameObject.SetActive(true);
            }

            foreach (var entity in _enemies)
            {
                if (numberEnemy >= 0)
                {
                    _listSoapEnemy.Add(entity);
                    entity.ResetPosAndState();
                    entity.gameObject.SetActive(true);
                    numberEnemy--;
                }
            }
        }

        public void CheckEntityDie()
        {
            foreach (var entity in _character)
            {
                if (!entity.gameObject.activeInHierarchy)
                {
                    _listSoapcharacters.Remove(entity);
                }
            }
            foreach (var entity in _enemies)
            {
                if (!entity.gameObject.activeInHierarchy)
                {
                    _listSoapEnemy.Remove(entity);
                }
            }
        }

        public bool CheckAll()
        {
            return _listSoapcharacters.IsEmpty || _listSoapEnemy.IsEmpty;
        }
    }
}
