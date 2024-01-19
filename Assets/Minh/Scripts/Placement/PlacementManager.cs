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
        [SerializeField] private ScriptableListPlayer _soapListPlayer;

        private void Start()
        {
            _fightingMap.Init();
            SpawnHeros();
        }

        public void SpawnHeros()
        {
            for (int i = 0; i < _hero.Count; i++)
            {
                Hero Player= SpawnHero(_hero[i], new Vector3(i + 1, i + 1, 0));
                _fightingMap.Value[i + 1, i + 1] = Player;
                Player._placementPosition=new Vector3(i+1,i+1,0);
                Player._playerText.text = "P" + (i + 1).ToString();
            }
        }

        private Hero SpawnHero(Hero Hero, Vector3 HeroPos)
        {
            return Instantiate(Hero, HeroPos, Quaternion.identity);
        }
    }
}