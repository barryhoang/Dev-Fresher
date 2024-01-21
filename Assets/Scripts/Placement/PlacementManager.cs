using System.Collections.Generic;
using Map;
using Obvious.Soap;
using Ultilities;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Placement
{
    public class PlacementManager : MonoBehaviour
    {
        [SerializeField] private ScriptableListUnit scriptableListHero;
        [SerializeField] private MapVariable mapVariable;
        [SerializeField] private List<Unit> heroes;
        [SerializeField] private GameObject placementGrid;
        [SerializeField] private ScriptableEventNoParam onFight; 
        [SerializeField] private Button fightButton;
        
        private readonly Vector2Int[] _heroPos = new Vector2Int[4];

        private void Awake()
        {
            SpawnUnit();
        }

        private void OnEnable()
        {
            fightButton.gameObject.SetActive(true);
            fightButton.onClick.AddListener(Fight);
            //ResetUnits();
        }

        private void SpawnUnit()
        {
            for (var i = 0; i < heroes.Count; i++)
            {
                scriptableListHero.Add(heroes[i]);
                var pos = heroes[i].transform.position.ToV2Int();
                mapVariable.Value[pos.x,pos.y] = heroes[i];
                _heroPos[i] = pos;
            }
        }

        private void Fight()
        {
            onFight.Raise();
            placementGrid.SetActive(false);
            gameObject.SetActive(false);
        }
        
        private void ResetUnits()        
        {
            for (var i = 0; i < scriptableListHero.Count; i++)
            {   
                scriptableListHero[i].gameObject.SetActive(false);
                var pos = scriptableListHero[i].transform.position.ToV2Int();
                mapVariable.Value[pos.x,pos.y] = null;
                heroes[i].transform.position =(Vector2) _heroPos[i];
                mapVariable.Value[_heroPos[i].x, _heroPos[i].y] = heroes[i];
                scriptableListHero[i].gameObject.SetActive(true);
            }
        }
    }
}
