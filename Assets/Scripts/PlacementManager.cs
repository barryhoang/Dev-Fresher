using System.Collections;
using System.Collections.Generic;
using Maps;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private MapVariable mapVariable;
    [SerializeField] private List<Hero> heroPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        mapVariable.Init();
        SetHeroes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SetHeroes()
    {
        for (int i = 0; i < heroPrefabs.Count; i++)
        {
            Hero heroScript = GetComponent<Hero>();
            mapVariable.Value[i + 1, i + 1] = heroScript;
        }
    }
}
