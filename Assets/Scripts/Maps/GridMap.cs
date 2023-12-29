using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [HideInInspector] public int height;
    [HideInInspector] public int width;
    
    int[,] grid;

    public void Init(int width, int height)
    {
        grid = new int[width,height];
        this.height = height;
        this.width = width;
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

    public bool CheckHeroPos(float heroPosX)
    {
        return heroPosX >= 0 && heroPosX < width/2;
    }

    public bool CheckWalkable(in int xPos, in int yPos)
    {
        return grid[xPos, yPos] == 0;
    }
}
