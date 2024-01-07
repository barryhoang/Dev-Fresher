using UnityEngine;

namespace Maps
{
    public class GridMap : MonoBehaviour
    {
        public int height;
        public int width;
        private int[,] _gridMap;

    
        public void Init(int _width, int _height)
        {
            _gridMap = new int[_width,_height];
            _height = height;
            _width = width;
        }

        public void SetTile(int x, int y, int to)
        {
            if (CheckPos(x, y) == false)
            {
                Debug.LogWarning("Out of bound");
                return;
            }
            _gridMap[x, y] = to;
        }

        public int GetTile(int x, int y)
        {
            if (CheckPos(x, y) != false) return _gridMap[x, y];
            Debug.LogWarning("Out of bound");
            return -1;
        }

        public bool CheckPos(int x, int y)
        {
            if (x < 0 || x >= width)
            {
                return false;
            }

            return y >= 0 && y < height;
        }
    }
}
