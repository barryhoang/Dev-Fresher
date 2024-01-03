using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using MEC;
using System.Linq;

public class GridControl : MonoBehaviour
{
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private Tilemap map;
    [SerializeField] private GridManager gridManager;
    
    [SerializeField] private Rect clickArea;
    [SerializeField] private ScriptableListGameObject listHero;
    [SerializeField] private ScriptableListGameObject listEnemy;
    [SerializeField] private Dictionary<GameObject, Vector3Int> lastCellPos 
        = new Dictionary<GameObject, Vector3Int>();
    
    private GameObject selectedHero;
    private Vector3Int selectedHeroInitCellPos;
    Pathfinding pathfinding;
    private Vector3 temp;


    private void Awake()
    {
        pathfinding = gridManager.GetComponent<Pathfinding>();
    }

    private void Start()
    {
        clickArea.width = 9;
        clickArea.height = 10;
        foreach (var hero in listHero)
        {
            if (hero != null && targetTilemap != null)
            {
                Vector3 playerWorldPos = hero.transform.position;
                Vector3Int cellPosition = targetTilemap.WorldToCell(playerWorldPos);
                lastCellPos[hero] = cellPosition;
            }
        }

        Timing.RunCoroutine(_MovePlayer());
    }

    private IEnumerator<float> _MovePlayer()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPosition = targetTilemap.WorldToCell(worldPoint);
                if (hit.collider != null && listHero.Contains(hit.collider.gameObject))
                {
                    selectedHero = hit.collider.gameObject;
                    Hero h = gridManager.GetHero(clickPosition.x, clickPosition.y);
                    if (h != null)
                    {
                        Debug.Log("Hero in cell: "+h.Name);
                    }
                }

                if (hit.collider != null && listEnemy.Contains(hit.collider.gameObject))
                {
                    Enemy e = gridManager.GetEnemy(clickPosition.x, clickPosition.y);
                    if (e != null)
                    {
                        Debug.Log("Enemy in cell: "+e.Name);
                    }
                }
            }

            if (selectedHero != null)
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
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (clickArea.Contains(new Vector2(worldPoint.x, worldPoint.y)))
            {
                Vector3Int clickPosition = map.WorldToCell(worldPoint);
                if (clickPosition != lastCellPos[selectedHero])
                {
                    targetTilemap.ClearAllTiles();
                    targetTilemap.SetTile(clickPosition, highlightTile);
                    clickPosition.z = 0;
                    lastCellPos[selectedHero] = clickPosition;
                }
                selectedHero.transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
                temp = selectedHero.transform.position;
            }
        }
        else
        {
            targetTilemap.ClearAllTiles();
            selectedHero.transform.position = targetTilemap.GetCellCenterWorld(lastCellPos[selectedHero]);
            selectedHero = null;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(clickArea.center, clickArea.size);
    }
}

/*private void MouseInput()
{
    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3Int clickPosition = targetTilemap.WorldToCell(worldPoint);
    if (Input.GetMouseButtonDown(0))
    {
        targetTilemap.ClearAllTiles();
        //gridManager.Set(clickPosition.x,clickPosition.y, 3);
        
        targetPosX = clickPosition.x;
        targetPosY = clickPosition.y;
        Debug.Log("Mouse Position: "+targetPosX+", "+targetPosY);

        
        
        /*List<PathNode> path = pathfinding.FindPath(currentX,currentY,targetPosX,targetPosY);
        if (path != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                targetTilemap.SetTile(new Vector3Int(path[i].xPos, path[i].yPos, 0), highlightTile);
            }
            currentX = targetPosX;
            currentY = targetPosY;
        }#1#
    }
}*/
