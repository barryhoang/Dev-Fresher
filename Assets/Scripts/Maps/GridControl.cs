using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;
using MEC;
using System.Linq;

public class GridControl : MonoBehaviour
{
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private Tilemap map;
    [SerializeField] private Rect clickArea;
    [SerializeField] private ScriptableListGameObject listHero;
    [SerializeField] private Dictionary<GameObject, Vector3Int> lastCellPos 
        = new Dictionary<GameObject, Vector3Int>();
    
    private GameObject selectedHero;
    private Vector3 temp;
    public HeroStateMachines HSM;
    

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
            if (Input.GetMouseButtonDown(0) && HSM.currentState == HeroStateMachines.TurnState.IDLE)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && listHero.Contains(hit.collider.gameObject))
                {
                    selectedHero = hit.collider.gameObject;
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