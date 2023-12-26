using UnityEngine;
using Obvious.Soap;

namespace Minh
{
    public class Node
    {
        public int gridX;
        public int gridY;

        public bool IsWall;
        public Vector3 Position;

        public Node Parent;

        public int gCost;
        public int hCost;

        public int FCost
        {
            get { return gCost + hCost; }
        }

        public Node(bool a_IsWall, Vector3 a_Pos, int a_gridX, int a_gridY)
        {
            IsWall = a_IsWall;
            Position = a_Pos;
            gridX = a_gridX;
            gridY = a_gridY;

        }
    }
}