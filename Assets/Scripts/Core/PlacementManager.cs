using Obvious.Soap;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private FightingGridVariable _fightingGrid;
    [SerializeField] private Unit[] _player;
    [SerializeField] private ScriptableEventNoParam _fightButton;
    [SerializeField] private ScriptableListUnit units;
    [SerializeField] private GameObject _playerGrid;

    private void Start()
    {
        SetUnitIntoFightingGrid();
        _fightButton.OnRaised += fightButton;
    }
    
    private void SetUnitIntoFightingGrid()
    {
        for (int i = 0; i < _player.Length; i++)
        {
            units.Add(_player[i]);
            var position = units[i].transform.position;
            _fightingGrid.Value[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = units[i];
        }
    }
    
    private void fightButton()
    {
        _playerGrid.SetActive(false);
    }
}
