using System;
using Obvious.Soap;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.WSA.Tile;

public class FightingGrid : MonoBehaviour
{
    [SerializeField] private FightingGridVariable _fightingGrid;
    [SerializeField] private Tilemap playGround;
    [SerializeField] private TileBase invalidBox;
    [SerializeField] private TileBase validBox;
    [SerializeField] private Unit[] _unitEnemy;
    [SerializeField] private ScriptableListUnit listUnitEnemy;

    private void Awake()
    {
        _createFightingGrid();
    }

    private void Update()
    {
        for (int i = 0; i < _fightingGrid.size.x; i++)
        {
            for (int j = 0; j < _fightingGrid.size.y; j++)
            {
                if (_fightingGrid.Value[i, j] != null)
                {
                    // Assuming you want to set the same tile for all units
                    Vector3Int cellPosition = new Vector3Int(i, j, 0);
                    playGround.SetTile(cellPosition, invalidBox);
                }
                else
                {
                    Vector3Int cellPosition = new Vector3Int(i, j, 0);
                    playGround.SetTile(cellPosition, null);
                }
            }
        }
        SetUnitIntoFightingGrid();
        
    }

    private void _createFightingGrid()
    {
        _fightingGrid.size = new Vector2Int(15, 6);
        _fightingGrid.Init();
    }

    private void SetUnitIntoFightingGrid()
    {
        foreach (var unit in listUnitEnemy)
        {
            if (!unit.gameObject.activeInHierarchy) continue;
            var position = unit.transform.position.ToV2Int();
            _fightingGrid.Value[position.x, position.y] = unit;
        }
    }

    

    
}