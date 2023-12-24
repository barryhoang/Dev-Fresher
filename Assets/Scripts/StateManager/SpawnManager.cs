using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace StateManager
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager instance { get; private set; }
        [SerializeField] private List<Character> _characters;
        [SerializeField] private List<Enemy> _enemies;
        
        private void Awake()
        {
            instance = this;
        }

        public void Spawn(int numberEnemy)
        {
            foreach (var entity in _characters)
            {
                entity.ResetPosAndState();
                entity.gameObject.SetActive(true);
            }

            foreach (var entity in _enemies)
            {
                if(numberEnemy <= 0) return;
                entity.ResetPosAndState();
                entity.gameObject.SetActive(true);
                numberEnemy--;
            }
        }
    }
}
