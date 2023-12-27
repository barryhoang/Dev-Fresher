using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Minh
{
    public class PlayerMoveAStar : MonoBehaviour
    {
        [SerializeField] private Tilemap _targetTilemap;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private TileBase _hightlightTile;
        [SerializeField]private Pathfinding _pathfinding;

        [SerializeField] private int _currentX = 0;
        [SerializeField] private int _currentY = 0;
        [SerializeField] private int _targetPosX = 0;
        [SerializeField] private int _targetPosY = 0;
        private Vector3 prevPosition;

        
        private void Update()
        {
            MouseInput();
            _pathfinding = _gridManager.GetComponent<Pathfinding>();
        }

        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _targetTilemap.ClearAllTiles();
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPosition = _targetTilemap.WorldToCell(worldPoint);
                

                _targetPosX = clickPosition.x;
                _targetPosY = clickPosition.y;
                List<PathNode> path = _pathfinding.FindPath(_currentX, _currentY, _targetPosX, _targetPosY);
                if (path != null)
                {
                    for (int i = 0; i < path.Count-1; i++)
                    {
                        _targetTilemap.SetTile(new Vector3Int(path[i].xPos, path[i].yPos,0),_hightlightTile);
                        transform.position=new Vector3(path[i].xPos,path[i].yPos,0);
                        
                    }

                    transform.position = prevPosition;
                    _gridManager.Set((int)prevPosition.x,(int)prevPosition.y,0);
                    _gridManager.Set((int)transform.position.x,(int)transform.position.y,2);
                    _currentX = _targetPosX;
                    _currentY = _targetPosY;
                }
                
            }
        }
    }
    
}