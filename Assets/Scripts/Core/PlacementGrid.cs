using Obvious.Soap;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 _onMouseDown;
    [SerializeField] private ScriptableEventVector2 _onMouseHold;
    [SerializeField] private ScriptableEventVector2 _onMouseUp;
    [SerializeField] private FightingGridVariable _fightingGrid;

    [SerializeField] private GameObject _boxColor;

    private Unit player;
    private Vector2Int prevPosition;
    float epsilon = 0.001f;
    
    void Start()
    {
        _onMouseDown.OnRaised += SavePreviousPosition;
        _onMouseHold.OnRaised += MovePlayer;
        _onMouseUp.OnRaised += SnapPlayer;
    }
    
    private void SavePreviousPosition(Vector2 value)
    {
        var mousePoint = value.ToV2Int();
        if (value.x >= 6 || value.x <= -1 || value.y >= 6 || value.y <= -1) return;
        if (_fightingGrid.Value[mousePoint.x, mousePoint.y] == null) return;
        
        player = _fightingGrid.Value[mousePoint.x, mousePoint.y];
        _fightingGrid.Value[mousePoint.x, mousePoint.y] = null;
        prevPosition = mousePoint;
        _boxColor.SetActive(true);
    }
    
    private void MovePlayer(Vector2 value)
    {
        if (player == null) return;
        player.transform.position = value;
        
        ChangeColor(value);
    }
    
    private void ChangeColor(Vector2 value)
    {
        var mousePoint = value.ToV2Int();
        Debug.Log(mousePoint);
        if (value.x > 5 || value.x < 0 || value.y > 5 || value.y < 0) return;
        
        _boxColor.transform.position = new Vector3(mousePoint.x, mousePoint.y);
    }
    
    private void SnapPlayer(Vector2 value)
    {
        Vector2Int mousePos = value.ToV2Int();
        if (mousePos.x >= 6 || mousePos.x <= -1 || mousePos.y >= 6 || mousePos.y <= -1)
        {
            MovePlayer(prevPosition);
            _fightingGrid.Value[prevPosition.x, prevPosition.y] = player;
        }
        else if(_fightingGrid.Value[mousePos.x, mousePos.y] == null)
        {
            MovePlayer(mousePos);
            _fightingGrid.Value[mousePos.x, mousePos.y] = player;
        }
        else
        {
            Unit temp = _fightingGrid.Value[mousePos.x, mousePos.y];
            _fightingGrid.Value[mousePos.x, mousePos.y] = player;
            MovePlayer(mousePos);
            temp.transform.position = new Vector3(prevPosition.x, prevPosition.y);
            _fightingGrid.Value[prevPosition.x, prevPosition.y] = temp;
        }
        _boxColor.SetActive(false);
        player = null;
    }
    
    void OnDisable()
    {
        _onMouseDown.OnRaised -= SavePreviousPosition;
        _onMouseHold.OnRaised -= MovePlayer;
        _onMouseUp.OnRaised -= SnapPlayer;
    }
}