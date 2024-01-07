using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using Units.Hero;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps
{
    public class GridControl : MonoBehaviour
    {
        [SerializeField] private TileBase highlightTile;
        [SerializeField] private Tilemap targetTileMap;
        [SerializeField] private Tilemap map;
        [SerializeField] private Rect clickArea;
        
        private readonly Dictionary<GameObject, Vector3Int> _lastCellPos 
            = new Dictionary<GameObject, Vector3Int>();
        private GameObject _selectedHero;
        private Vector3 _temp;
        
        public ScriptableListGameObject selectableHeroes;
    

        private void Start()
        {
            clickArea.width = 9;
            clickArea.height = 10;
            foreach (var hero in selectableHeroes)
            {
                if (hero == null || targetTileMap == null) continue;
                var playerWorldPos = hero.transform.position;
                var cellPosition = targetTileMap.WorldToCell(playerWorldPos);
                _lastCellPos[hero] = cellPosition;
            }

            Timing.RunCoroutine(_MovePlayer());
        }

        private IEnumerator<float> _MovePlayer()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0) && Camera.main != null)
                { 
                    var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
                    if (hit.collider != null && selectableHeroes.Contains(hit.collider.gameObject))
                    { 
                        _selectedHero = hit.collider.gameObject;
                    }
                }
                if (_selectedHero != null)
                {
                    MoveSelectedPlayer();
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    
        private void MoveSelectedPlayer()
        {
            if (Input.GetMouseButton(0))
            {
                if (Camera.main == null) return;
                var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (!clickArea.Contains(new Vector2(worldPoint.x, worldPoint.y))) return;
                var clickPosition = map.WorldToCell(worldPoint);
                if (clickPosition != _lastCellPos[_selectedHero])
                {
                    targetTileMap.ClearAllTiles();
                    targetTileMap.SetTile(clickPosition, highlightTile);
                    clickPosition.z = 0;
                    _lastCellPos[_selectedHero] = clickPosition;
                }
                _selectedHero.transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
                _temp = _selectedHero.transform.position;
            }
            else
            {
                targetTileMap.ClearAllTiles();
                _selectedHero.transform.position = targetTileMap.GetCellCenterWorld(_lastCellPos[_selectedHero]);
                _selectedHero = null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(clickArea.center, clickArea.size);
        }
    }
}