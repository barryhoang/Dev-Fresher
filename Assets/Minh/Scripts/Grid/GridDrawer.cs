using UnityEngine;
using Obvious.Soap;
namespace Minh
{
    public class GridDrawer : UnityEngine.MonoBehaviour
    {
        [SerializeField] private IntVariable _startxPosition;
        [SerializeField] private IntVariable _startyPosition;
        [SerializeField] private IntVariable _gridSizeX;
        [SerializeField] private IntVariable _gridSizeY;
       
        public float cellSize = 1.0f;
        public GameObject gridLinePrefab;
        
        private void Start()
        {
            DrawGrid();
        }

        public void DrawGrid()
        {
            int startX = _startxPosition;
            int startY = _startyPosition;

            for (int x = 0; x <= _gridSizeX; x++)
            {
                float xPos = startX + x * cellSize;
                CreateGridLine(new Vector3(xPos, startY, 0), new Vector3(xPos, startY + _gridSizeY * cellSize, 0));
            }

            for (int y = 0; y <= _gridSizeY; y++)
            {
                float yPos = startY + y * cellSize;
                CreateGridLine(new Vector3(startX, yPos, 0), new Vector3(startX + _gridSizeX * cellSize, yPos, 0));
            }
        }

        void CreateGridLine(Vector3 start, Vector3 end)
        {
            GameObject gridLine = Instantiate(gridLinePrefab, Vector3.zero, Quaternion.identity);
            LineRenderer lineRenderer = gridLine.GetComponent<LineRenderer>();
            gridLine.transform.SetParent(gameObject.transform);
            
            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, end);
            }
            else
            {
                Debug.LogWarning("LineRenderer component not found on gridLinePrefab.");
            }
        }

        public void DestroyGridLines()
        {
            // Destroy all child objects with LineRenderer component
            foreach (Transform child in transform)
            {
                LineRenderer lineRenderer = child.GetComponent<LineRenderer>();
                if (lineRenderer != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}