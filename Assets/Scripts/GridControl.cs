using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using Units.Enemy;
using Units.Hero;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace Maps
{
    public class GridControl : MonoBehaviour
    {
        [SerializeField] private TileBase highlightTile;
        [SerializeField] private Tilemap targetTileMap;
        [SerializeField] public Tilemap map;
        [SerializeField] private List<GameObject> heroPrefabs;
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private float tileSize = 1.0f;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ScriptableListHero scriptableListHero;
        [SerializeField] private ScriptableListEnemy scriptableListEnemy;
        [SerializeField] private ScriptableEventNoParam onLose;
        [SerializeField] private ScriptableEventNoParam onVictory;
        [SerializeField] private Button fightButton;
        [SerializeField] private ScriptableEventVector2 onBtnDown;
        [SerializeField] private MapVariable mapVariable;
        
        private readonly Dictionary<GameObject, Vector3Int> _lastCellPos 
            = new Dictionary<GameObject, Vector3Int>();
        private GameObject _selectedHero;
        private Vector3 _temp;
        
        public ScriptableListGameObject selectableHeroes;
        public Rect clickArea;

        private void Awake()
        {
            SpawnUnits();
            fightButton.gameObject.SetActive(true);
            var button = fightButton.GetComponent<Button>();
            button.onClick.AddListener(StartOnClick);
        }

        private void Start()
        {
            clickArea.width = 9;
            clickArea.height = 10;
            foreach (var hero in selectableHeroes)
            {
                if (hero == null || targetTileMap == null) continue;
                var playerWorldPos = hero.transform.position;
                var cellPosition = map.WorldToCell(playerWorldPos);
                _lastCellPos[hero] = cellPosition;
            }

            Timing.RunCoroutine(_MovePlayer());
            Timing.RunCoroutine(CheckState());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(clickArea.center, clickArea.size);
        }

        private IEnumerator<float> _MovePlayer()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0) && Camera.main != null)
                { 
                    var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
                    if (hit.collider != null && selectableHeroes.Contains(hit.collider.gameObject))
                    { 
                        _selectedHero = hit.collider.gameObject;
                    }
                }
                if (_selectedHero != null)
                {
                    MoveSelectedPlayer();
                }
                yield return Timing.WaitForOneFrame;
            }
        }
    
        private void MoveSelectedPlayer()
        {
            if (Input.GetMouseButton(0))
            {
                if (Camera.main == null) return;
                var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (clickArea.Contains(new Vector2(worldPoint.x, worldPoint.y)))
                {
                    var clickPosition = map.WorldToCell(worldPoint);
                    if (clickPosition != _lastCellPos[_selectedHero])
                    {
                        targetTileMap.ClearAllTiles();
                        targetTileMap.SetTile(clickPosition, highlightTile);
                        clickPosition.z = 0;
                        _lastCellPos[_selectedHero] = clickPosition;
                    }

                    _selectedHero.transform.position = new Vector3(worldPoint.x, worldPoint.y, 1);
                    _temp = _selectedHero.transform.position;
                }
            }
            else
            {
                targetTileMap.ClearAllTiles();
                _selectedHero.transform.position = targetTileMap.GetCellCenterWorld(_lastCellPos[_selectedHero]);
                _selectedHero = null;
            }
        }

        private void SpawnUnits()
        {
            foreach (var hero in heroPrefabs)
            {
                var randomX = (int) Random.Range(1, clickArea.width-1);
                var randomY = (int) Random.Range(1, clickArea.height-1);
                var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
                var spawnedHero = Instantiate(hero, spawnPosition, Quaternion.identity, map.transform);
            }
            
            foreach (var enemy in enemyPrefabs)
            {
                var randomX = Random.Range(clickArea.width,clickArea.width*2-1);
                var randomY = Random.Range(1, clickArea.height-1);
                var spawnPosition = new Vector3(randomX * tileSize, randomY * tileSize, 1);
                var spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity, map.transform);
                spawnedEnemy.transform.Rotate(0,180,0);
            }
        }

        private IEnumerator<float> CheckState()
        {
            if (scriptableListHero.Count == 0)
            {
                gameManager.SetGameState(GameManager.State.Lose);
                onLose.Raise();
            }
            if (scriptableListEnemy.Count == 0)
            {
                gameManager.SetGameState(GameManager.State.Victory);
                onVictory.Raise();
            }

            yield return Timing.WaitForOneFrame;
        }

        private void StartOnClick()
        {
            gameManager.currentState = GameManager.State.Fight;
        }
        
        private void CheckHeroPos(Vector2 mousePos)
        {
            Vector2Int mousePosInt = new Vector2Int((int)Mathf.RoundToInt(mousePos.x),(int)Mathf.RoundToInt(mousePos.y));
            Debug.Log("MousePos: "+mousePosInt);
            if (mapVariable.Value[mousePosInt.x,mousePosInt.y])
            {
                Hero h = mapVariable.Value[mousePosInt.x, mousePosInt.y];
                Debug.Log(h.gameObject.name);
            }
        }

        private void OnDestroy()
        {
            onBtnDown.OnRaised -= CheckHeroPos;
        }
    }
}