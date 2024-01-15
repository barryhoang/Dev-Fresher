using UnityEngine;

public class SaveLoadMap : MonoBehaviour
{
    [SerializeField] private MapData mapData;
    [SerializeField] private GridManager gridManager;

    
    public void Save()
    {
        var map = gridManager.ReadTileMap();
        mapData.Save(map);
    }

    public void Load()
    {
        gridManager.Clear();
        var width = mapData.width;
        var height = mapData.height;
        var i = 0;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                gridManager.SetTile(x,y,mapData.map[i]);
                i += 1;
            }
        }
    }

    internal void LoadMap(GridMap gridMap)
    {
        mapData.Load(gridMap);
    }
}
