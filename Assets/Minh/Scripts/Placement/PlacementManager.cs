using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Obvious.Soap;
using UnityEngine.UIElements;

namespace Minh
{
    public class PlacementManager : MonoBehaviour
    {
        [SerializeField] private FightingMapVariable _fightingMap;
        [SerializeField] private List<Hero> _hero;

        private void Start()
        {
            _fightingMap.Init();
            SpawnHeros();
        }

        private void SpawnHeros()
        {
            for (int i = 0; i < _hero.Count; i++)
            {
                _fightingMap.Value[i + 1, i + 1] = SpawnHero(_hero[i], new Vector3(i + 1, i + 1, 0));
            }
        }

        private Hero SpawnHero(Hero Hero, Vector3 HeroPos)
        {
            return Instantiate(Hero, HeroPos, Quaternion.identity);
        }
    }
}