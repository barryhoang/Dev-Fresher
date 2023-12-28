using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using PrimeTween;
using MEC;

namespace Minh
{
    public class PlayerMoveAStar : MonoBehaviour
    {
        [SerializeField] private Tilemap _targetTilemap;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private TileBase _hightlightTile;
        [SerializeField] private Pathfinding _pathfinding;

        [SerializeField] private int _currentX = 0;
        [SerializeField] private int _currentY = 0;
        [SerializeField] private int _targetPosX = 0;
        [SerializeField] private int _targetPosY = 0;
        private Vector3 prevPosition;
        [SerializeField] private TweenSettings _tweenSettings;

        private void Start()
        {
            Timing.RunCoroutine(MouseInput());
        }

        private void Update()
        {
            _pathfinding = _gridManager.GetComponent<Pathfinding>();
        }

       private IEnumerator<float> MouseInput()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _targetTilemap.ClearAllTiles();
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int clickPosition = _targetTilemap.WorldToCell(worldPoint);
                    _targetPosX = clickPosition.x;
                    _targetPosY = clickPosition.y;
                    List<PathNode> path = _pathfinding.FindPath(_currentX, _currentY, _targetPosX, _targetPosY,"player");
                    if (path != null)
                    {
                        for (int i = 0; i < path.Count-1 ; i++)
                        {
                            //Tween.Position(transform, new Vector3(path[i].xPos, path[i].yPos, 0), _tweenSettings);
                            //transform.position = new Vector3(path[i].xPos, path[i].yPos, 0);
                            yield return Timing.WaitUntilDone(
                                Timing.RunCoroutine(Move(new Vector3(path[i].xPos, path[i].yPos, 0))));
                        }
                        _currentX = _targetPosX;
                        _currentY = _targetPosY;
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> Move(Vector3 movePosition)
        {
            prevPosition = transform.position;
            Tween.Position(transform, movePosition, _tweenSettings);
            _gridManager.Set((int)prevPosition.x,(int)prevPosition.y,0);
            _gridManager.Set((int)movePosition.x,(int)movePosition.y,3);
            yield return Timing.WaitForSeconds(_tweenSettings.duration);
        }
    }
}