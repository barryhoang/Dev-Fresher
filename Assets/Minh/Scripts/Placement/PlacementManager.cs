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
        [SerializeField] private List<GameObject> _hero;

        private void Start()
        {
            for (int i = 0; i < _hero.Count; i++)
            {
                GameObject hero=Instantiate(_hero[i], new Vector3(i + 1, i + 1, 0), Quaternion.identity);
                Hero heroScript = GetComponent<Hero>();
                _fightingMap.Value[i+1, i+1]=heroScript;
            }
        }
    }
}
