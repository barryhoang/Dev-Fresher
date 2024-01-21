using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Pathfinding;
using PrimeTween;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private Unit[] UnitsEnemyOrPlayer;
    [SerializeField] private FightingGridVariable _fightingGrid;
    [SerializeField] private Button _fightButtonActive;
    [SerializeField] private Path path;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private ScriptableEventNoParam _fightButton;

    public Unit _unitTarget;
    public Vector3 pathTarget;
    public Vector3 nextPos;
    private int currentWaypoint = 0;
    private bool isMoving = false;
    public BlockManager blockManager;
    public List<SingleNodeBlocker> obstacles;
    public Vector2Int closestTarget;
    BlockManager.TraversalProvider traversalProvider;

    private void Awake()
    {
        traversalProvider =
            new BlockManager.TraversalProvider(blockManager, BlockManager.BlockMode.OnlySelector, obstacles);
        reachedEndOfPath = false;
        SearchForPath(UnitsEnemyOrPlayer);
    }

    public void Start()
    {
        // Create a traversal provider which says that a path should be blocked by all the SingleNodeBlockers in the obstacles array
        /*traversalProvider =
            new BlockManager.TraversalProvider(blockManager, BlockManager.BlockMode.OnlySelector, obstacles);
        reachedEndOfPath = false;*/
        _fightButton.OnRaised += StartGame;
        /*SearchForPath(UnitsEnemyOrPlayer);*/
    }

    private void OnDisable()
    {
        _fightButton.OnRaised -= StartGame;
    }

    public void StartGame()
    {
        Timing.RunCoroutine(UpdateMove().CancelWith(gameObject), Segment.FixedUpdate);
    }

    public void OnPathComplete(Path p)
    {
        p.Claim(this);
        if (!p.error)
        {
            if (path != null) path.Release(this);
            path = p;
            currentWaypoint = 0;
        }
        else p.Release(this);
    }

    private void SearchForPath(Unit[] UnitsEnemyOrPlayerTemp)
    {
        closestTarget = FindClosestTarget(UnitsEnemyOrPlayerTemp);
        _unitTarget = FindClosestUnit(UnitsEnemyOrPlayerTemp);
        path = CreatePath(new Vector3(closestTarget.x, closestTarget.y));
    }

    private Path CreatePath(Vector3 target)
    {
        var path = ABPath.Construct(transform.position, target, OnPathComplete);
        path.traversalProvider = traversalProvider;

        // Calculate the path synchronously
        AstarPath.StartPath(path);
        path.BlockUntilCalculated();

        if (path.error)
        {
            Debug.Log("No path was found");
        }
        else
        {
            //Debug.Log("A path was found with " + path.vectorPath.Count + " nodes");
            for (int i = 0; i < path.vectorPath.Count - 1; i++)
            {
                Debug.DrawLine(path.vectorPath[i], path.vectorPath[i + 1], Color.red);
            }
        }

        return path;
    }

    /*private IEnumerator<float> UpdateMove()
    {
        if (path == null) yield break;
        while (isMoving == false)
        {
            _unitTarget = FindClosestUnit(UnitsEnemyOrPlayer);
            if (Vector2.Distance(_unitTarget.transform.position, transform.position) <= 1f)
            {
                Debug.Log("A");
                yield break;
            }
            CreatePath(_unitTarget.transform.position);
            Tween.Position(transform, new Vector3(path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y),new TweenSettings(duration: 1f, useFixedUpdate: true));
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y), 2 * Time.deltaTime);
            
            if (transform.position == path.vectorPath[0])
            {
                isMoving = false;
            }
            
            
            yield return Timing.WaitForOneFrame;
        }
    }*/


    /*private void Update()
    {
        if (path == null)
        {
            return;
        }

        reachedEndOfPath = false;

        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < 0.0001)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    _fightingGrid.Value[transform.position.ToV2Int().x, transform.position.ToV2Int().y] = this;
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        if (Vector2.Distance(path.vectorPath[1], _unitTarget.pathTarget) <= 1f)
        {
            return;
        }

        var dir = (path.vectorPath[currentWaypoint] - transform.position).normalized.ToV2Int();
        UpdateFightingGrid(path.vectorPath[currentWaypoint].ToV2Int(), dir);
        SetToMove(dir);
        //_fightingGrid.Value[transform.position.ToV2Int().x, transform.position.ToV2Int().y] = null;

        //transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], 2.0f * Time.deltaTime);
    }*/

    private IEnumerator<float> UpdateMove()
    {
        /*_fightingGrid.Value[transform.position.ToV2Int().x, transform.position.ToV2Int().y] = null;*/
        
        if (path == null) yield break;
        while (path.vectorPath.Count >= 1)
        {
            if (closestTarget == null) yield return Timing.WaitForOneFrame;
            
            if (IsValidTarget(closestTarget) == false)
            {
                SearchForPath(UnitsEnemyOrPlayer);
            }
            else if (IsValidTarget(closestTarget) == true)
            {
                var position = transform.position;

                _fightingGrid.Value[position.ToV2Int().x, position.ToV2Int().y] = null;

                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y), 2 * Time.deltaTime);
                Tween.Position(transform, new Vector3(path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y),new TweenSettings(duration: 2f, useFixedUpdate: true));
                //_fightingGrid.Value[path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y] = this;
                Debug.Log(_unitTarget + " " +_unitTarget.transform.position + " " + transform.position);
                Debug.Log( gameObject.name + " " +_unitTarget + " " + Vector2.Distance(_unitTarget.transform.position, transform.position));
                
                if (CheckDistanceCell(transform.position.ToV2Int(), closestTarget))
                {
                    if(Vector2.Distance(_unitTarget.transform.position, transform.position) <= 1f)
                        //if (CheckDistanceCell(path.vectorPath[1].ToV2Int(), _unitTarget.transform.position.ToV2Int()))
                    {
                        _fightingGrid.Value[transform.position.ToV2Int().x, transform.position.ToV2Int().y] = this;
                        yield break;
                    }
                }
                SearchForPath(UnitsEnemyOrPlayer);
            }


            /*transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y), 2 * Time.deltaTime);*/

            /*var position = transform.position;*/

            //_fightingGrid.Value[position.ToV2Int().x, position.ToV2Int().y] = null;
            /*if (_fightingGrid.Value[path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y] == null)
            {
                _fightingGrid.Value[path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y] = this;
                transform.position = Vector2.MoveTowards(position, new Vector2(path.vectorPath[1].ToV2Int().x, path.vectorPath[1].ToV2Int().y), 2 * Time.deltaTime);
                
                SearchForPath(UnitsEnemyOrPlayer);
            }
            else
            {
                SearchForPath(UnitsEnemyOrPlayer);
                yield return Timing.WaitForOneFrame;
            }
            //transform.position = position;
    
            if (reachedEndOfPath)
            {
                //SearchForPath(UnitsEnemyOrPlayer);
                Debug.Log("AAA");
            }*/


            //_fightingGrid.Value[nextPosition.x, nextPosition.y] = this;

            //Debug.Log(nextPosition);
            /*if (transform.position == path.vectorPath[1])
            {
                SearchForPath(UnitsEnemyOrPlayer);
                //_fightingGrid.Value[nextPosition.x, nextPosition.y] = this;
                pathTarget = path.vectorPath[1];
                Vector2Int dir = (path.vectorPath[1] - transform.position).normalized.ToV2Int();
                if (Vector2.Distance(path.vectorPath[1], _unitTarget.pathTarget) <= 1f)
                {
                    while (Vector2.Distance(_unitTarget.transform.position,transform.position) > 1f)
                    {
                        Debug.Log("A");
                        transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[1], 2 * Time.deltaTime);
                        yield return Timing.WaitForOneFrame;
                    }
                    //Debug.Log(_unitTarget.name);
                    //Debug.Log(gameObject.name + " " + path.vectorPath[1] + " " + _unitTarget.pathTarget);
                    Debug.Log(gameObject.name + " " + Vector2.Distance(path.vectorPath[1], _unitTarget.pathTarget));
                    //SearchForPath(UnitsEnemyOrPlayer);
                    //var abc = transform.position.ToV2Int();
                    //_fightingGrid.Value[abc.x, abc.y] = this;
                    break;
                }
                path.vectorPath[1].ToV2Int();
                
            }
            SearchForPath(UnitsEnemyOrPlayer);*/
            yield return Timing.WaitForOneFrame;
        }
    }

    private bool CheckDistanceCell(Vector2Int pos, Vector2Int posTarget)
    {
        int distance = Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y);
        return Math.Abs(distance) <= 1f;
    }

    private Vector2Int FindClosestTarget(Unit[] enemies)
    {
        Vector2Int closestTarget = new Vector2Int(transform.position.ToV2Int().x, transform.position.ToV2Int().y);
        var shortestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            Vector2Int[] targets = TakeTarget(enemy);

            foreach (var target in targets)
            {
                if (!IsValidTarget(target)) continue;
                var distance = Vector2.Distance(transform.position, target);
                if (!(distance < shortestDistance)) continue;

                shortestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    private bool IsValidTarget(Vector2Int target)
    {
        return !(target.x < 0 || target.x >= 15 || target.y < 0 || target.y >= 6) &&
               !(target.x >= _fightingGrid.size.x || target.y >= _fightingGrid.size.y) &&
               _fightingGrid.Value[target.x, target.y] == null
            /*&& _fightingGrid.Value[nextPos.ToV2Int().x, nextPos.ToV2Int().y] == null*/;
    }

    public Unit FindClosestUnit(Unit[] enemys)
    {
        var shortestDistance = float.MaxValue;
        Unit shortestEnemy = null;
        foreach (var enemy in enemys)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                shortestEnemy = enemy;
            }
        }

        return shortestEnemy;
    }

    /*private void UpdateFightingGrid(Vector2Int nextPosition, Vector2Int velocity)
    {
        var position = transform.position.ToV2Int();
        SearchForPath(UnitsEnemyOrPlayer);

        /*if (_fightingGrid.Value[nextPosition.x, nextPosition.y] != null)
        {
            SearchForPath(UnitsEnemyOrPlayer);
        }
        else if (_fightingGrid.Value[nextPosition.x, nextPosition.y] == null)
        {
            _fightingGrid.Value[nextPosition.x, nextPosition.y] = this;
        }#1#

        SetToMove(velocity);
    }*/

    /*private void SetToMove(Vector2 velocity)
    {
        if (!isMoving && !reachedEndOfPath)
        {
            if (velocity == Vector2.up)
            {
                Timing.RunCoroutine(Move(Vector2.up));
            }
            else if (velocity == Vector2.down)
            {
                Timing.RunCoroutine(Move(Vector2.down));
            }
            else if (velocity == Vector2.left)
            {
                Timing.RunCoroutine(Move(Vector2.left));
            }
            else if (velocity == Vector2.right)
            {
                Timing.RunCoroutine(Move(Vector2.right));
            }
        }
    }
    */


    /*private IEnumerator<float> Move(Vector2 direction)
    {
        isMoving = true;

        // Make a note of where we are and where we are going.
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * 1.0f);

        // Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / 0.5f;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return Timing.WaitForOneFrame;
        }

        transform.position = endPosition;
        isMoving = false;
    }*/


    /*private Vector2Int FindClosestTarget(Unit enemy)
    {
        Vector2Int[] targets = TakeTarget(enemy);
        Vector2Int closestTarget = Vector2Int.zero;
        var shortestDistance = float.MaxValue;

        foreach (var target in targets)
        {
            if (target.x <= 0 || target.x >= 15 || target.y <= 0 || target.y >= 6) continue;
            if (target.x >= _fightingGrid.size.x || target.y >= _fightingGrid.size.y) continue;
            if (_fightingGrid.Value[target.x, target.y] != null) continue;
            var distance = Vector2.Distance(transform.position, target);
            if (!(distance < shortestDistance)) continue;
            shortestDistance = distance;
            closestTarget = target;
        }

        return closestTarget;
    }*/


    private Vector2Int[] TakeTarget(Unit u)
    {
        var position = u.transform.position;
        Vector2Int[] targets = new Vector2Int[4];

        targets[0] = (position + Vector3.right).ToV2Int();
        targets[1] = (position + Vector3.left).ToV2Int();
        targets[2] = (position + Vector3.up).ToV2Int();
        targets[3] = (position + Vector3.down).ToV2Int();
        return targets;
    }

    private void Attack()
    {
    }

    private void TakeDamage()
    {
    }

    private void Die()
    {
    }
}