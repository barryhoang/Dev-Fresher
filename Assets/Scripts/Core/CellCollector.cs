using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellCollector : MonoBehaviour
{
    public Tilemap playground;
    public ScriptableListVector3 allCellPositions;

    void Start()
    {
        CollectAllCellPositions();
    }

    void CollectAllCellPositions()
    {
        if (playground == null)
        {
            Debug.LogError("Tilemap (playground) not assigned!");
            return;
        }

        BoundsInt bounds = playground.cellBounds;

        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0); // Set z to 0 in a 2D game
                allCellPositions.Add(cellPosition);
            }
        }
    }
}
