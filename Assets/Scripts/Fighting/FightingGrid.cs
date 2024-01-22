using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Fighting
{
    public class FightingGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit scriptableListHero;
        [SerializeField] private ScriptableListUnit scriptableListEnemy;
        [SerializeField] private ScriptableEventNoParam onFight;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private ScriptableEventNoParam onDefeated;
        /*[SerializeField] private MapVariable mapVariable;
        [SerializeField] private Tilemap testMap;
        [SerializeField] private TileBase tileBase;*/
        
        private bool _fighting;

        private void OnEnable()
        {
            onFight.OnRaised += Fight;
            onVictory.OnRaised += Victory;
            onDefeated.OnRaised += Defeated;
            //Timing.RunCoroutine(Test().CancelWith(gameObject));
        }

        private void Fight()
        {
            _fighting = true;
            Timing.RunCoroutine(Move().CancelWith(gameObject));
        }

        private void Victory() => _fighting = false;

        private void Defeated() => _fighting = false;

        private IEnumerator<float> Move()
        {
            while (_fighting)
            {
                foreach (var hero in scriptableListHero)
                {
                    if(hero.attacking) continue;
                    var enemy = scriptableListEnemy.GetClosestUnit(gameObject.transform.position);
                    hero.target = enemy;
                    if (!hero.moving)
                    {
                        hero.moving = true;
                        Timing.RunCoroutine(hero.Move(enemy).CancelWith(gameObject));
                    }
                }
                foreach (var enemy in scriptableListEnemy)
                {
                    if(enemy.attacking) continue;
                    var hero = scriptableListHero.GetClosestUnit(gameObject.transform.position);
                    enemy.target = hero;
                    if (!enemy.moving)
                    {
                        enemy.moving = true;
                        Timing.RunCoroutine(enemy.Move(hero).CancelWith(gameObject));
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }

        /*private IEnumerator<float> Test()
        {
            while (true)
            {
                for (int i = 0; i < mapVariable.size.x; i++)
                {
                    for (int j = 0; j < mapVariable.size.y; j++)
                    {
                        if (mapVariable.Value[i, j] != null)
                        {
                            testMap.SetTile(new Vector3Int(i,j),tileBase );
                        }
                        else
                        {
                            testMap.SetTile(new Vector3Int(i,j),null );
                        }
                    }
                }
                yield return Timing.WaitForOneFrame;
            }
        }*/
        
        private void OnDisable()
        {
            onFight.OnRaised -= Fight;
            onVictory.OnRaised -= Victory;
            onDefeated.OnRaised -= Defeated;
        }
    }   
}
