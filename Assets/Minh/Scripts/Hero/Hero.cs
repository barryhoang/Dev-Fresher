using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
namespace Minh
{
    
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroStat _heroStats;
        [SerializeField] private FightingGrid _fightingGrid;
        [SerializeField] private FightingMapVariable _fightingMapVariable;
        private int _health;
        public void TakeDamage(int damage)
        {
            _health -= damage;
           
        }
        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    var position = transform.position.ToV2Int();
                    _fightingMapVariable.Value[position.x, position.y] = null;
                    gameObject.SetActive(false);
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
    
}