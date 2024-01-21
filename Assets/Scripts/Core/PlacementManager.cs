using System;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private FightingGridVariable _fightingGrid;
    [SerializeField] private Unit[] _unitPlayer;
    [SerializeField] private ScriptableEventNoParam _fightButton;
    [SerializeField] private ScriptableListUnit listUnitPlayer;
    [SerializeField] private GameObject _playerGrid;
    [SerializeField] private GameObject _fightButtonActive;

    private void Start()
    {
        SetUnitIntoFightingGrid();
        _fightButton.OnRaised += FightButton;
    }

    private void SetUnitIntoFightingGrid()
    {
        foreach (var unit in _unitPlayer)
        {
            if (!unit.gameObject.activeInHierarchy) continue;
            listUnitPlayer.Add(unit);
            //Debug.Log("abc" + " " + unit);
            var position = unit.transform.position.ToV2Int();
            _fightingGrid.Value[position.x, position.y] = unit;
        }
    }
    
    private void FightButton()
    {
        _playerGrid.SetActive(false);
        _fightButtonActive.SetActive(false);
    }

    private void OnDisable()
    {
        _fightButton.OnRaised -= FightButton;
    }
}
