using UnityEngine;

namespace Apathfinding
{
    public class GridMap : MonoBehaviour
    {
        public int length;
        public int height;
    
        private int[,] grid;

        public void Init (int length, int height)
        {
            grid = new int[length, height];
            this.length = length;
            this.height = height;
        }
        public void Set(int x, int y, int to)
        {
    
            if(CheckPosition(x,y) == false)
            {
                return;
            }
            grid[x, y] = to;
        }

        public int Get(int x, int y)
        {
            if(CheckPosition(x,y) == false)
            {
                return -1;
            }
            return grid[x,y];
        }

        public bool CheckPosition(int x, int y)
        {
            if(x < 0 || x >= length)
                return false;
            if (y < 0 || y >= height)
                return false;
            return true;
        }

    
    
        internal bool CheckWalkable(int xPos, int yPos,Vector3 posStart)
        {
            if (grid[(int) posStart.x, (int) posStart.y] != 0)
            {
                return true;
            }
            return grid[xPos, yPos] == 0;
        }
    }
}
