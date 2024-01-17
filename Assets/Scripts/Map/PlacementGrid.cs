using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<GameObject> heroPrefabs;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private Tilemap map;

    private Vector2 _posBefore;
    private Hero _hero;
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
        mapVariable.Init();
        SpawnUnits();
    }

    private void MouseDown(Vector2 value)
    {
        var mousePosInt = new Vector2Int(Mathf.RoundToInt(value.x),
            Mathf.RoundToInt(value.y));
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
        if (hit.collider != null && selectableHeroes.Contains(hit.collider.gameObject))
        { 
            _hero = hit.collider.gameObject.GetComponent<Hero>();
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = null;
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
            _hero.transform.position = _posBefore;
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
        }
        else if (mapVariable.Value[mousePosInt.x,mousePosInt.y] == null)
        {
            _hero.transform.position = (Vector2) mousePosInt;
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
        }
        else
        {
            _tempHero = mapVariable.Value[mousePosInt.x, mousePosInt.y];
            _hero.transform.position = (Vector2)mousePosInt;;
            mapVariable.Value[mousePosInt.x, mousePosInt.y] = _hero;
            _tempHero.transform.position = _posBefore;
            mapVariable.Value[(int) _posBefore.x, (int) _posBefore.y] = _tempHero;
        }
        _hero = null;
    }
    
    private void SpawnUnits()
    {
        foreach (var hero in heroPrefabs)
        {
            var randomX = Random.Range(3, 8);
            var randomY = Random.Range(3, 8);
            var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
            var pos = map.WorldToCell(spawnPosition);
            var snappedPosition = map.GetCellCenterWorld(pos);
            var spawnedHero = Instantiate(hero, snappedPosition, Quaternion.identity, map.transform);
            mapVariable.Value[randomX, randomY] = hero.GetComponent<Hero>();
        }
            
        foreach (var enemy in enemyPrefabs)
        {
            var randomX = Random.Range(10,17);
            var randomY = Random.Range(2, 9);
            var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
            var pos = map.WorldToCell(spawnPosition);
            var snappedPosition = map.GetCellCenterWorld(pos);
            var spawnedEnemy = Instantiate(enemy, snappedPosition, Quaternion.identity, map.transform);
            spawnedEnemy.transform.Rotate(0,180,0);
        }
    }
    private void OnDestroy()
    {
        onBtnDown.OnRaised -= MouseDown;
        onBtnDrag.OnRaised -= MouseDrag;
        onBtnUp.OnRaised -= MouseUp;
    }
}
