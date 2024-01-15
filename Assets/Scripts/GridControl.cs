using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControl : MonoBehaviour
{
    [SerializeField] public Tilemap map;
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap targetTileMap;
    [SerializeField] private List<GameObject> heroPrefabs;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private ScriptableEventVector2 onBtnDown;
    [SerializeField] private MapVariable mapVariable;
    [SerializeField] private float tileSize = 1.0f;
        
    private readonly Dictionary<GameObject, Vector3Int> _lastCellPos 
        = new Dictionary<GameObject, Vector3Int>();
    private GameObject _selectedHero;
    private Vector3 _temp;
        
    public ScriptableListGameObject selectableHeroes;
    public Rect clickArea;

    private void Awake()
    {
        SpawnUnits();
    }

    private void Start()
    {
        clickArea.width = 6;
        clickArea.height = 6;
        foreach (var hero in selectableHeroes)
        {
            if (hero == null || targetTileMap == null) continue;
            var playerWorldPos = hero.transform.position;
            var cellPosition = map.WorldToCell(playerWorldPos);
            _lastCellPos[hero] = cellPosition;
        }

        Timing.RunCoroutine(_MovePlayer());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(clickArea.center, clickArea.size);
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
            if (clickArea.Contains(new Vector2(worldPoint.x, worldPoint.y)))
            {
                var clickPosition = map.WorldToCell(worldPoint);
                if (clickPosition != _lastCellPos[_selectedHero])
                {
                    targetTileMap.ClearAllTiles();
                    targetTileMap.SetTile(clickPosition, highlightTile);
                    clickPosition.z = 0;
                    _lastCellPos[_selectedHero] = clickPosition;
                }

                _selectedHero.transform.position = new Vector3(worldPoint.x, worldPoint.y, 1);
                _temp = _selectedHero.transform.position;
            }
        }
        else
        {
            targetTileMap.ClearAllTiles();
            _selectedHero.transform.position = targetTileMap.GetCellCenterWorld(_lastCellPos[_selectedHero]);
            _selectedHero = null;
        }
    }

    private void SpawnUnits()
    {
        foreach (var hero in heroPrefabs)
        {
            var randomX = Random.Range(3, 8);
            var randomY = Random.Range(3, 8);
            var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
            var pos = map.WorldToCell(spawnPosition);
            var snappedPosition = map.GetCellCenterWorld(pos);
            var spawnedHero = Instantiate(hero, snappedPosition, Quaternion.identity, map.transform);
        }
            
        foreach (var enemy in enemyPrefabs)
        {
            var randomX = Random.Range(10,17);
            var randomY = Random.Range(2, 9);
            var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
            var pos = map.WorldToCell(spawnPosition);
            var snappedPosition = map.GetCellCenterWorld(pos);
            var spawnedEnemy = Instantiate(enemy, snappedPosition, Quaternion.identity, map.transform);
            spawnedEnemy.transform.Rotate(0,180,0);
        }
    }

    private void CheckHeroPos(Vector2 mousePos)
    {
        var mousePosInt = new Vector2Int((int)Mathf.RoundToInt(mousePos.x),(int)Mathf.RoundToInt(mousePos.y));
        Debug.Log("MousePos: "+mousePosInt);
        if (mapVariable.Value[mousePosInt.x,mousePosInt.y])
        {
            var h = mapVariable.Value[mousePosInt.x, mousePosInt.y];
            Debug.Log(h.gameObject.name);
        }
    }

    private void OnDestroy()
    {
        onBtnDown.OnRaised -= CheckHeroPos;
    }
}