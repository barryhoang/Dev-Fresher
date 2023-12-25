using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Grid : MonoBehaviour
    {
        public int gridSizeX = 10;
        public int gridSizeY = 10;
        public float cellSize = 1f;
     
        void Start()
        {
            DrawGrid();
        }

        void DrawGrid()
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = (gridSizeX + gridSizeY + 2) * 2;

            int index = 0;
            lineRenderer.SetPosition(index++,Vector3.zero);
            lineRenderer.SetPosition(index++, new Vector3(3, 0, 0));
            lineRenderer.SetPosition(index++, new Vector3(3, 3, 0));
            lineRenderer.SetPosition(index++, new Vector3(0, 3, 0));
            lineRenderer.SetPosition(index++,new Vector3(0,0,0));
            lineRenderer.SetPosition(index++, new Vector3(1, 3, 0));
            
            // lineRenderer.SetPosition(index++, new Vector3(3, 0, 0));
            
            // Draw horizontal lines
            for (int y = 0; y <= gridSizeY; y++)
            {
               
            }
           
            for (int x = 0; x <= gridSizeX; x++)
            {
               
            }
        }
    }
}
