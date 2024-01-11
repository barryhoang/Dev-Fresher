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
            Vector2Int MousePosInt = new Vector2Int((int) Mathf.Round(mousePos.x), (int) Mathf.Round(mousePos.y));
            Debug.Log(MousePosInt + "MOUSE POSITION ");
            if (_fightingMap.Value[MousePosInt.x, MousePosInt.y] != null)
            {
                MovePlayerPosition(_fightingMap.Value[MousePosInt.x, MousePosInt.y], mousePos, MousePosInt);
            }
            else
            {
                return;
            }
        }

        private void MovePlayerPosition(Hero hero, Vector2 mousePos, Vector2Int MousePosInt)
        {
            hero = _fightingMap.Value[MousePosInt.x, MousePosInt.y];
            Vector2 offset = mousePos - MousePosInt;
            hero.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            hero = _fightingMap.Value[MousePosInt.x, MousePosInt.y];
            Debug.Log(hero.gameObject.name);
            Debug.Log(hero.gameObject.transform.position);
        }

        // Update is called once per frame
        private void OnDestroy()
        {
            _onButtonDown.OnRaised -= CheckHeroPosition;
        }
    }
}