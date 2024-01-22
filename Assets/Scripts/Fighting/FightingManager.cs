using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Units;
using UnityEngine;

namespace Fighting
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit scriptableListEnemy;
        [SerializeField] private ScriptableListUnit scriptableListHero;
        [SerializeField] private ScriptableEventUnit onDead;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private ScriptableEventNoParam onDefeated;

        private List<Unit> _enemy;

        private void OnEnable() 
        {
            onDead.OnRaised += Dead;
            onFight.OnRaised += CheckCondition;
        }

        private void CheckCondition() => Timing.RunCoroutine(CheckCond().CancelWith(gameObject));

        private void Dead(Unit unit)
        {
            foreach (var hero in scriptableListHero)
            {
                if (hero != unit)continue;
                scriptableListHero.Remove(hero);
                return;
            }
            foreach (var enemy in scriptableListEnemy)
            {
                if (enemy != unit)continue;
                scriptableListEnemy.Remove(enemy);
                return;
            }
        }
        
        private IEnumerator<float> CheckCond()
        {
            while (true)
            {
                if (scriptableListEnemy.IsEmpty)
                {
                    onVictory.Raise();
                    break;
                }

                if (scriptableListHero.IsEmpty)
                {
                    onDefeated.Raise();
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
        }
        
        private void OnDisable() 
        {
            onDead.OnRaised -= Dead;
            onFight.OnRaised -= CheckCondition;
        }
    }
}
