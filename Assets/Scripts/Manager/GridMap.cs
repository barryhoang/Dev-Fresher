using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    public int height;
    public int length;
    
    int[,] grid;

    public void Init(int length, int height)
    {
        grid = new int[length,height];
        this.height = height;
        this.length = length;
    }

    public void Set(int x, int y, int to)
    {
        if (CheckPos(x, y) == false)
        {
            Debug.LogWarning("Out of bound");
            return;
        }
        grid[x, y] = to;
    }

    public int Get(int x, int y)
    {
        if (CheckPos(x, y) == false)
        {
            Debug.LogWarning("Out of bound");
            return -1;
        }
        return grid[x, y];
    }

    public bool CheckPos(int x, int y)
    {
        if (x < 0 || x >= length)
        {
            return false;
        }

        if (y < 0 || y >= height)
        {
            return false;
        }

        return true;
    }

    public bool CheckWalkable(in int xPos, in int yPos)
    {
        return grid[xPos, yPos] == 0;
    }
}
