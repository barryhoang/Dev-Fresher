using System;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class PlayerPlacement : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Collider2D _characterCollider;
        [SerializeField] private IntVariable _startxPosition;
        [SerializeField] private IntVariable _startyPosition;
        [SerializeField] private IntVariable _gridSizex;
        [SerializeField] private IntVariable _gridSizey;
        [SerializeField] private Vector3Reference _Position;

        private int _endxPosition;
        private int _endyPosition;
        private bool _isDragging = false;
        private Vector3 _offset;
        public Vector3 _prevTransform;
        public Vector3 cellCenterBefore;
        public Grid grid;

        private void Awake()
        {
            _endxPosition = _startxPosition + _gridSizex - 1;
            _endyPosition = _startyPosition + _gridSizey - 1;
        }

        private void Start()
        {
            
            SnapToGrid();
            _prevTransform = transform.position;
        }


        private void Update()
        {
           
            if (_isDragging)
            {
               
                // Update player Position
                Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
                Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
                var transform1 = transform;
                transform1.position = cursorWorldPoint + _offset;

                
                Vector3Int cellPosition = grid.WorldToCell(transform1.position);
                Vector3 cellCenter = grid.GetCellCenterWorld(cellPosition);
                Debug.Log(cellPosition+"PLAYER CELL POSITION");

                if (!_characterCollider.bounds.Contains(cellCenter))
                {
                    transform.position = cellCenter;
                }

                if (cellPosition.x < _startxPosition || cellPosition.x >_endxPosition  || cellPosition.y < _startxPosition || cellPosition.y > _endyPosition)
                {
                    Checking();
                }
               

                // Check Mouse Button Up
                if (Input.GetMouseButtonUp(0))
                {
                    _prevTransform = transform.position;

                    
                    _isDragging = false;
                }
            }
        }

        void OnMouseDown()
        {
            // Bắt đầu kéo thả khi chuột được nhấn
            _isDragging = true;
            _offset = transform.position -
                      Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            
        }

        private void SnapToGrid()
        {
            Vector3Int cellPosition = grid.WorldToCell(transform.position);
            transform.position = grid.GetCellCenterWorld(cellPosition);
        }

        public void ResetPosition()
        {
            transform.position = _prevTransform;
            Vector3Int cellPositionBefore = grid.WorldToCell(_prevTransform);
            Vector3 cellCenterBefore = grid.GetCellCenterWorld(cellPositionBefore);
            transform.position = cellCenterBefore;
        }

        private void Checking()
        {
            var transform1 = transform;
            transform1.position = _prevTransform;
            Debug.Log(transform1.position + "Player TRANSFORM");
            Vector3Int cellPositionBefore = grid.WorldToCell(_prevTransform);
            cellCenterBefore = grid.GetCellCenterWorld(cellPositionBefore);
            transform.position = cellCenterBefore;
        }
    }
}