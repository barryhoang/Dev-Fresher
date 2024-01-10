using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Minh
{
    public class PlacementGrid : MonoBehaviour
    {
        [SerializeField] private Tilemap _playerPlacementGrid;
        [SerializeField] private ScriptableListHero _soapListHero;
        [SerializeField] private ScriptableEventVector2 _onButtonDown;

        [SerializeField] private FightingMapVariable _fightingMap;
        // Start is called before the first frame update
        void Start()
        {
            _onButtonDown.OnRaised += CheckHeroPosition;
        }

        private void CheckHeroPosition(Vector2 mousePos)
        {
            Vector2Int MousePosInt= new  Vector2Int((int)Mathf.Round(mousePos.x),(int)Mathf.Round(mousePos.y));
            if (_fightingMap.Value[MousePosInt.x, MousePosInt.y] != null)
            {
                Hero z = _fightingMap.Value[MousePosInt.x, MousePosInt.y];
                Vector2 offset = mousePos - MousePosInt;
               Debug.Log(z.gameObject.name);
            }
        }

        // Update is called once per frame
        private void OnDestroy()
        {
            _onButtonDown.OnRaised -= CheckHeroPosition;
        }
    }
}

