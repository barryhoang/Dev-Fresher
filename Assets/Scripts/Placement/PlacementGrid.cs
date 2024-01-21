using System;
using System.Collections.Generic;
using System.Linq;
using Map;
using MEC;
using UnityEngine;
using Obvious.Soap;
using Ultilities;
using Units;
using UnityEngine.Tilemaps;

namespace Placement
{
    public class PlacementGrid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventVector2 onMouseDrag;
        [SerializeField] private ScriptableEventVector2 onMouseDown;
        [SerializeField] private ScriptableEventVector2 onMouseUp;
        [SerializeField] private MapVariable mapVariable;
        [SerializeField] private Tilemap highlightMap;
        [SerializeField] private TileBase highlightTile;
        
        private Unit _hero;
        private Vector2 _lastCellPos;
        private Unit _tempHero;
        private bool _dragging;
        
        public ScriptableListGameObject selectableHeroes;

        private void OnEnable()
        {
            onMouseDown.OnRaised += CheckCellPosition;
            onMouseDrag.OnRaised += HeroDragging;
            onMouseUp.OnRaised += SetHeroPosition;
        }

        private IEnumerator<float> CheckCell(Vector2 value)
        {
            var mouPos = value.ToV2Int();
            if (mouPos.x < 0 || mouPos.y < 0 || mouPos.x >= 6 || mouPos.y >= 6)
            {   
                yield break;
            }
            
            _hero = mapVariable.Value[mouPos.x, mouPos.y];
            mapVariable.Value[mouPos.x, mouPos.y] = null;
            _lastCellPos = mouPos;
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 
                Vector2.zero);
            _dragging = true;
            if (hit.collider != null && selectableHeroes.Contains(hit.collider.gameObject))
            {
                _hero = hit.collider.gameObject.GetComponent<Unit>();
                mapVariable.Value[mouPos.x, mouPos.y] = null;
                _lastCellPos = _hero.transform.position;
            }

            yield return Timing.WaitForOneFrame;
        }

        private void CheckCellPosition(Vector2 value) => Timing.RunCoroutine(CheckCell(value));

        private IEnumerator<float> DragHero(Vector2 value)
        {
            if (!_dragging) yield break;
            highlightMap.ClearAllTiles();
            if (_hero != null)
            {
                _hero.transform.position = value;
                var mouPos = value.ToV2Int();
                var highlightCell = highlightMap.WorldToCell((Vector3Int)mouPos);
                highlightMap.SetTile(highlightCell, highlightTile);
            }

            yield return Timing.WaitForOneFrame;
        }

        private void HeroDragging(Vector2 value) => Timing.RunCoroutine(DragHero(value));

        private IEnumerator<float> SetHero(Vector2 value)
        {
            if (!_dragging) yield break;
            _dragging = false;
            var mouPos = value.ToV2Int();
            highlightMap.ClearAllTiles();
            if (_hero == null) yield break;
            
            if (mouPos.x < 0 || mouPos.x >= 6 || mouPos.y < 0 || mouPos.y >= 6 && _hero != null)
            {
                _hero.transform.position = _lastCellPos;
                mapVariable.Value[mouPos.x, mouPos.y] = _hero;
            }
            else if (mapVariable.Value[mouPos.x,mouPos.y] == null && _hero != null)
            {
                _hero.transform.position = (Vector2) mouPos;
                mapVariable.Value[mouPos.x, mouPos.y] = _hero;
            }
            else if (_hero != null)
            {
                _tempHero = mapVariable.Value[mouPos.x, mouPos.y];
                _hero.transform.position = (Vector2)mouPos;
                _tempHero.transform.position = _lastCellPos;
                mapVariable.Value[mouPos.x, mouPos.y] = _hero;
                mapVariable.Value[(int)_lastCellPos.x, (int)_lastCellPos.y] = _tempHero;
            }
            
            _hero = null;
            yield return Timing.WaitForOneFrame;
        }

        private void SetHeroPosition(Vector2 value) => Timing.RunCoroutine(SetHero(value));

        private void OnDestroy()
        {
            onMouseDown.OnRaised -= CheckCellPosition;
            onMouseDrag.OnRaised -= HeroDragging;
            onMouseUp.OnRaised -= SetHeroPosition;
        }
    }
}
