using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public int width, height;
    public List<int> map;

    public void Load(GridMap gridMap)
    {
        gridMap.Init(width,height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridMap.Set(x,y,Get(x,y));
            }
        }
    }

    internal void Save(int[,] map)
    {
        width = map.GetLength(0);
        height = map.GetLength(1);
        this.map = new List<int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                this.map.Add(map[x,y]);
            }
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
    private int Get(int x, int y)
    {
        int index = x * height + y;
        if (index >= map.Count)
        {
            return -1;
        }
        return map[index];
    }
}

/*public void Save(GridMap gridMap)
{
    width = gridMap.width;
    height = gridMap.height;
    
    map = new List<int>();
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            map.Add(gridMap.Get(x,y));
        }
    }
}*/
