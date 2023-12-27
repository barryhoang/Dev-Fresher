using System;
using UnityEngine;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine.UIElements;

namespace Minh
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] public int _height;
        [SerializeField] public int _length;

        private int[,] _grid;

        public void Init(int length, int height)
        {
            _grid = new int [length, height];
            this._length = length;
            this._height = height;
            Debug.Log("Grid NODE"+_grid.Length);
        }

        public void Set(int x, int y, int to)
        {
            if (CheckPosition(x, y) == false)
            {
                Debug.LogWarning("Trying to Set an cell outside the grid boundries"+x.ToString()+":"+y.ToString());
                return; 
                
            }
            _grid[x, y] = to;
        }

        public int Get(int x, int y)
        {
            if (CheckPosition(x, y) == false)
            {
                Debug.LogWarning("Trying to Get an cell outside the grid boundries"+x.ToString()+":"+y.ToString());
                return -1; 
                
            }
            return _grid[x, y];
        }

        public bool CheckPosition(int x, int y)
        {
            if (x < 0||x>=_length)
            {
                return false;
            }

            if (y < 0 || y >= _height)
            {
                return false;
            }
            return true;
        }

        internal bool CheckWalkable(int xPos, int yPos)
        {
            return _grid[xPos, yPos] == 0;
        }
    }
}