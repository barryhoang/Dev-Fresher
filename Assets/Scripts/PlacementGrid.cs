using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;
using Obvious.Soap;
using UnityEngine.Tilemaps;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField] private ScriptableEventVector2 onBtnDown;
    [SerializeField] private ScriptableEventVector2 onBtnDrag;
    [SerializeField] private ScriptableEventVector2 onBtnUp;
    [SerializeField] private MapVariable mapVariable;
    [SerializeField] private GameObject placementGrid;

    private Vector2 _posBefore;
    private Hero _hero;
    [SerializeField] private Tilemap map;
    private bool _dragging;
    private Hero _tempHero;
    private Vector2 _heroPos;
    private GameObject _selectedHero;
    private Vector3 _temp;
    private readonly Dictionary<GameObject, Vector3Int> _lastCellPos 
        = new Dictionary<GameObject, Vector3Int>();
    public ScriptableListGameObject selectableHeroes;

    private void Start()
    {
        onBtnDown.OnRaised += MouseDown;
        onBtnDrag.OnRaised += MouseDrag;
        onBtnUp.OnRaised += MouseUp;
        placementGrid.SetActive(true);
    }

    private void MouseDown(Vector2 value)
    {
        var mousePosInt = new Vector2Int(Mathf.RoundToInt(value.x),
            Mathf.RoundToInt(value.y));
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
        if (hit.collider != null && selectableHeroes.Contains(hit.collider.gameObject))
        { 
            _hero = hit.collider.gameObject.GetComponent<Hero>();
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
            _posBefore = _hero.transform.position;
        }
    }

    private void MouseDrag(Vector2 value)
    {
        if (_hero != null)
        {
            _hero.transform.position = value;
        }
    }

    private void MouseUp(Vector2 mousePos)
    {
        if(_hero == null) return;
        
        var mousePosInt = new Vector2Int(Mathf.RoundToInt(mousePos.x),
            Mathf.RoundToInt(mousePos.y));
        if (mousePosInt.x < 3 || mousePosInt.x > 8 || mousePosInt.y < 2 || mousePosInt.y > 7)
        {
            _hero.transform.position = map.WorldToCell(_posBefore);
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
        }
        else if (mapVariable.Value[mousePosInt.x,mousePosInt.y] == null)
        {
            _hero.transform.position = map.WorldToCell((Vector2) mousePosInt);;
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
        }
        else
        {
            _tempHero = mapVariable.Value[mousePosInt.x, mousePosInt.y];
            _hero.transform.position = map.WorldToCell((Vector2) mousePosInt);;
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
            _tempHero.transform.position = map.WorldToCell(_posBefore);
            mapVariable.Value[(int) _posBefore.x, (int) _posBefore.y] = _tempHero;
        }
        _hero = null;
    }
    private void OnDestroy()
    {
        onBtnDown.OnRaised -= MouseDown;
        onBtnDrag.OnRaised -= MouseDrag;
        onBtnUp.OnRaised -= MouseUp;
    }
}
