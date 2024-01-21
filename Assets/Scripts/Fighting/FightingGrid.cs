using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Units;
using UnityEngine;

namespace Fighting
{
    public class FightingGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit scriptableListHero;
        [SerializeField] private ScriptableListUnit scriptableListEnemy;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private ScriptableEventNoParam onVictory;

        public bool fighting;

        private void OnEnable()
        {
            onFight.OnRaised += Fight;
            onVictory.OnRaised += Victory;
        }

        private void Fight()
        {
            fighting = true;
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }

        private void Victory() => fighting = false;

        private IEnumerator<float> Move()
        {
            while (fighting)
            {
                foreach (var hero in scriptableListHero)
                {
                    if(hero.attacking) continue;
                    var enemy = scriptableListEnemy.GetClosestUnit(hero.transform.position);
                    if (!hero.moving)
                    {
                        hero.moving = true;
                        Timing.RunCoroutine(hero.Move(enemy).CancelWith(gameObject));
                    }
                    else
                    {
                        hero.target = enemy;
                    }
                }
                foreach (var enemy in scriptableListEnemy)
                {
                    if(enemy.attacking) continue;
                    var hero = scriptableListHero.GetClosestUnit(enemy.transform.position);
                    if (!enemy.moving)
                    {
                        enemy.moving = true;
                        Timing.RunCoroutine(enemy.Move(hero).CancelWith(gameObject),"Move");
                    }
                    else
                    {
                        enemy.target = hero;
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        private void OnDisable()
        {
            onFight.OnRaised -= Fight;
            onVictory.OnRaised += Victory;
        }
    }   
}
