using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int tileId;
    public Hero hero;
    public Enemy enemy;
}
public class GridMap : MonoBehaviour
{
    public int height;
    public int width;
    
    Node[,] gridMap;

    public void Init(int width, int height)
    {
        gridMap = new Node[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridMap[x,y] = new Node();
            }
        }
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
        gridMap[x, y].tileId = to;
    }

    public int GetTile(int x, int y)
    {
        if (CheckPos(x, y) == false)
        {
            Debug.LogWarning("Out of bound");
            return -1;
        }
        return gridMap[x, y].tileId;
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

    public bool CheckWalkable(int xPos, int yPos)
    {
        return gridMap[xPos, yPos].tileId == 0;
    }

    public Hero GetHero(int x, int y)
    {
        return gridMap[x, y].hero;
    }

    public void SetHero(MapElement mapElement,int xPos, int yPos)
    {
        gridMap[xPos, yPos].hero = mapElement.GetComponent<Hero>();
    }

    public Enemy GetEnemy(int x, int y)
    {
        return gridMap[x, y].enemy;
    }

    public void SetEnemy(MapElement mapElement,int xPos, int yPos)
    {
        gridMap[xPos,yPos].enemy = mapElement.GetComponent<Enemy>();
    }
}
