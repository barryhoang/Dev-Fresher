using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap map;
    [SerializeField] private List<TileData> _tileDatas;
    [SerializeField] private Dictionary<TileBase, TileData> _dataFromTiles;
    [SerializeField] private GameObject HighlightTile;

    private void Awake()
    {
        _dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in _tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                _dataFromTiles.Add(tile,tileData);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Camera.main.transform.forward);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (hit.collider != null)
            {
                Vector3Int gridPos = map.WorldToCell(hit.point);
                TileBase clickedTile = map.GetTile(gridPos);
                bool _HeroSpawnTile = _dataFromTiles[clickedTile]._isHeroSpawnTile;
                if (gridPos.x <= map.cellSize.x / 2)
                {
                    _HeroSpawnTile = true;
                    Instantiate(HighlightTile,gridPos, Quaternion.identity);
                }
                Debug.Log(_HeroSpawnTile);
                Debug.Log(hit.point);
                Debug.Log(gridPos);
                
            }
        }
    }
}
