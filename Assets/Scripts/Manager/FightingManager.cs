using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
using Unit;
using UnityEngine;

namespace Manager
{
    public class FightingManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListHero scriptableListHero;
        [SerializeField] private ScriptableListEnemy scriptableListEnemy;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private ScriptableEventNoParam onLose;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Pathfinding pathfinding;

        private BaseUnit _baseUnit;
        private bool _attacking;
        
        private void Start()
        {
            pathfinding.Init();
            onFight.OnRaised += Check;
            onFight.OnRaised += Fight;
        }

        private void OnDisable()
        {
            onFight.OnRaised -= Check;
            onFight.OnRaised -= Fight;
        }

        private void Check()
        {
            Timing.RunCoroutine(CheckCond());
        }

        private void Fight()
        {
            _attacking = true;
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }
        
        private IEnumerator<float> CheckCond()
        {
            while (true)
            {
                if(scriptableListEnemy.Count==0)
                {
                    onVictory.Raise();
                    gameManager.SetGameState(GameManager.State.Victory);
                    break;
                }
            
                if (scriptableListHero.Count==0)
                {
                    onLose.Raise();
                    gameManager.SetGameState(GameManager.State.Defeated);
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> Move()
        {
            while (_attacking)
            {
                foreach (var hero in scriptableListHero)
                {
                    if (hero.attacking) continue;
                    var target = scriptableListEnemy.GetClosest(transform.position);
                    if (!hero.moving)
                    {
                        hero.moving = true;
                        Timing.RunCoroutine(hero.Move(target).CancelWith(gameObject));
                    }
                    else
                    {
                        BaseUnit.Target = target;
                    }
                }
                
                foreach (var enemy in scriptableListEnemy)
                {
                    if (enemy.attacking) continue;
                    var target = scriptableListHero.GetClosest(transform.position);
                    if (!enemy.moving)
                    {
                        enemy.moving = true;
                        Timing.RunCoroutine(enemy.Move(target).CancelWith(gameObject));
                    }
                    else
                    {
                        BaseUnit.Target = target;
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
