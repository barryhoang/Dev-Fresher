using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;
using UnityEngine.Tilemaps;
using Obvious.Soap;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _fillTile;
    [SerializeField] private TileBase _previousTile;
    [SerializeField] private Rect _clickArea;
    [SerializeField] private ScriptableListGameObject _players;
    [SerializeField] private Dictionary<GameObject, Vector3Int> _lastCellPositions = new Dictionary<GameObject, Vector3Int>();
    [SerializeField] ScriptableListVector3 allCellPositions;
    
    private GameObject _selectedPlayer;
    private Vector3Int _selectedPlayerInitialCellPosition;

    private void Start()
    {
        // Initialize _lastCellPositions with the current cell positions of the players
        foreach (var player in _players)
        {
            if (player != null && _tilemap != null)
            {
                Vector3 playerWorldPos = player.transform.position;
                Vector3Int cellPosition = _tilemap.WorldToCell(playerWorldPos);
                _lastCellPositions[player] = cellPosition;
            }
        }

        Timing.RunCoroutine(_MovePlayer());
    }

    private IEnumerator<float> _MovePlayer()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the mouse position
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && _players.Contains(hit.collider.gameObject))
                {
                    // Select the clicked player
                    _selectedPlayer = hit.collider.gameObject;
                }
            }

            if (_selectedPlayer != null)
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

            // Move the selected player only
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_clickArea.Contains(new Vector2(mouseWorldPos.x, mouseWorldPos.y)))
            {
                Vector3Int cellPosition = _tilemap.WorldToCell(mouseWorldPos);

                // Check if the mouse has moved to a new cell position
                if (cellPosition != _lastCellPositions[_selectedPlayer])
                {
                    // Clear the previous tile
                    _tilemap.SetTile(_lastCellPositions[_selectedPlayer], _previousTile);

                    // Set the fill tile at the new cell position
                    _tilemap.SetTile(cellPosition, _fillTile);

                    // Update the last cell position
                    _lastCellPositions[_selectedPlayer] = cellPosition;
                }

                // Move the selected player sprite to follow the mouse
                _selectedPlayer.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
            }
        }
        else
        {
            // Clear the tile if the mouse is outside the clickArea
            _tilemap.SetTile(_lastCellPositions[_selectedPlayer], _previousTile);

            // Reset the selected player position to the last cell position
            _selectedPlayer.transform.position = _tilemap.GetCellCenterWorld(_lastCellPositions[_selectedPlayer]);

            // Deselect the player
            _selectedPlayer = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_clickArea.center, _clickArea.size);
    }
}