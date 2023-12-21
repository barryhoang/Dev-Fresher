using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Obvious.Soap;

public class Grid : MonoBehaviour
{
    private int _width;
    private int _height;
    private float _cellSize;
    private int[,] gridArray;

    public Grid(int width,int height,float cellSize)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        
        gridArray = new int[width,height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; x < gridArray.GetLength(1); y++)
            {
                UtilsClass.CreateWorldText(gridArray[x, y].ToString(),null ,GetWorldPosition(x,y)+new Vector3(cellSize,_cellSize)*.5f,20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y+1),Color.white);
                Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white);
            }
            Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white);
            Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height),Color.white);
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y)*_cellSize;
    }

}
