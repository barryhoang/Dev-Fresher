using Obvious.Soap;
using UnityEngine;

public class FightingGrid : MonoBehaviour
{
    [SerializeField] private FightingGridVariable _fightingGrid;

    private void Start()
    {
        _createFightingGrid();
    }

    private void _createFightingGrid()
    {
        _fightingGrid.size = new Vector2Int(14,6);
        _fightingGrid.Init();
    }
}
