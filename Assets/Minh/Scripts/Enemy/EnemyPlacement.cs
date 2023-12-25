using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minh
{
    public class EnemyPlacement : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Collider2D _enemyCollider;

        public void Init()
        {
            Vector3Int cellPosition = _grid.WorldToCell(transform.position);
            Vector3 cellCenter = _grid.GetCellCenterWorld(cellPosition);
            transform.position = cellCenter;
        }

        public void SetEnemyCenter()
        {
            Vector3Int cellPosition = _grid.WorldToCell(transform.position);
            Vector3 cellCenter = _grid.GetCellCenterWorld(cellPosition);
            transform.position = cellCenter;
        }
    }
    
}