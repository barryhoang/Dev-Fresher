using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] private Tile highlightTile;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private GridManager gridManager;
    Pathfinding pathfinding;

    int currentX = 0;
    int currentY = 0;
    int targetPosX = 0;
    int targetPosY = 0;

    private void Update()
    {
        MouseInput();
        pathfinding = gridManager.GetComponent<Pathfinding>();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetTilemap.ClearAllTiles();
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = targetTilemap.WorldToCell(worldPoint);
            //gridManager.Set(clickPosition.x,clickPosition.y, 5);

            targetPosX = clickPosition.x;
            targetPosY = clickPosition.y;

            /*List<PathNode> path = pathfinding.FindPath(currentX,currentY,targetPosX,targetPosY);

            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    targetTilemap.SetTile(new Vector3Int(path[i].xPos, path[i].yPos, 0), highlightTile);
                }
            }

            currentX = targetPosX;
            currentY = targetPosY;*/
        }
    }
}
