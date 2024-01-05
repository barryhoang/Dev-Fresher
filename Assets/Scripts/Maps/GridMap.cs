using UnityEngine;

public class GridMap : MonoBehaviour
{
    public int height;
    public int width;
    private int[,] gridMap;

    
    public void Init(int width, int height)
    {
        gridMap = new int[width,height];
        this.height = height;
        this.width = width;
    }

    public void SetTile(int x, int y, int to)
    {
        if (CheckPos(x, y) == false)
        {
            Debug.LogWarning("Out of bound");
            return;
        }
        gridMap[x, y] = to;
    }

    public int GetTile(int x, int y)
    {
        if (CheckPos(x, y) == false)
        {
            Debug.LogWarning("Out of bound");
            return -1;
        }
        return gridMap[x, y];
    }

    public bool CheckPos(int x, int y)
    {
        if (x < 0 || x >= width)
        {
            return false;
        }

        if (y < 0 || y >= height)
        {
            return false;
        }

        return true;
    }
}
