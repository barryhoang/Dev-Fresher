using System.Collections;
using System.Collections.Generic;
using Maps;
using Units.Hero;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private MapVariable mapVariable;
    [SerializeField] private List<Hero> heroPrefabs;
    private ScriptableListHero _scriptableListHero;
    
    void Start()
    {
        foreach (var hero in heroPrefabs)
        {
            _scriptableListHero.Add(hero);
            mapVariable.Value[(int) hero.transform.position.x, (int) hero.transform.position.y] = hero ;
        }
    }
    
    
}
