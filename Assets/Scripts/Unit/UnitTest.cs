using System.Collections;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using Pathfinding;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    [SerializeField] private Unit[] UnitsEnemyOrPlayer;
    [SerializeField] private FightingGridVariable _fightingGrid;
    [SerializeField] private Path path;
    [SerializeField] private float nextWaypointDistance = 10;
    [SerializeField] private bool reachedEndOfPath;
    [SerializeField] private float moveDuration;
    
    private int currentWaypoint = 0;
    private bool isMoving = false;
    private Unit unitTarget;
    
    public BlockManager blockManager;
    public List<SingleNodeBlocker> obstacles;

    BlockManager.TraversalProvider traversalProvider;
    public void Start()
    {
        // Create a traversal provider which says that a path should be blocked by all the SingleNodeBlockers in the obstacles array
        traversalProvider = new BlockManager.TraversalProvider(blockManager, BlockManager.BlockMode.OnlySelector, obstacles);
        SearchForPath(UnitsEnemyOrPlayer);
        reachedEndOfPath = false;
        //Timing.RunCoroutine(abc().CancelWith(gameObject));
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
        var a = FindClosestUnit(UnitsEnemyOrPlayerTemp);
        unitTarget = FindClosestUnit(UnitsEnemyOrPlayerTemp);
        var b = FindClosestTarget(a);
        CreatePath(new Vector3(b.x, b.y));
    }

    private void CreatePath(Vector3 target)
    {
        // Create a new Path object
        var path = ABPath.Construct(transform.position, target, OnPathComplete);

        // Make the path use a specific traversal provider
        path.traversalProvider = traversalProvider;

        // Calculate the path synchronously
        AstarPath.StartPath(path);
        path.BlockUntilCalculated();

        if (path.error) {
            Debug.Log("No path was found");
        } else {
            Debug.Log("A path was found with " + path.vectorPath.Count + " nodes");

            // Draw the path in the scene view
            for (int i = 0; i < path.vectorPath.Count - 1; i++) {
                Debug.DrawLine(path.vectorPath[i], path.vectorPath[i + 1], Color.red);
            }
        }
    }
    
    public IEnumerator<float> UpdateMove()
    {
        while (true)
        {
            if (path == null) yield return Timing.WaitForOneFrame;
            float distanceToWaypoint;
            while (!reachedEndOfPath)
            {
                distanceToWaypoint = (transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude;
                if (distanceToWaypoint < nextWaypointDistance)
                {
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        reachedEndOfPath = true;
                        _fightingGrid.Value[transform.position.ToV2Int().x, transform.position.ToV2Int().y] = this.unitTarget;
                        yield break;
                    }
                }
                else
                {
                    yield break;
                }
            }
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            Vector2Int velocity = dir.ToV2Int();

            Vector2Int nextPosition = path.vectorPath[currentWaypoint].ToV2Int();

            if (!reachedEndOfPath)
            {
                UpdateFightingGrid(nextPosition, velocity);
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    private void SetToMove(Vector2 velocity)
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

    private void UpdateFightingGrid(Vector2Int nextPosition, Vector2Int velocity)
    {
        var position = transform.position.ToV2Int();
        //_fightingGrid.Value[position.x, position.y] = null;
        
        SearchForPath(UnitsEnemyOrPlayer);
        
        /*if (_fightingGrid.Value[nextPosition.x, nextPosition.y] != null)
        {
            SearchForPath(UnitsEnemyOrPlayer, seeker);
        }
        else if(_fightingGrid.Value[nextPosition.x, nextPosition.y] == null)
        {
            _fightingGrid.Value[nextPosition.x, nextPosition.y] = this;
        }*/
        SetToMove(velocity);
    }

    private IEnumerator<float> Move(Vector2 direction)
    {
        isMoving = true;

        // Make a note of where we are and where we are going.
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * 1.0f);

        // Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return Timing.WaitForOneFrame;
        }

        transform.position = endPosition;
        isMoving = false;
    }

    private Vector2Int FindClosestTarget(Unit enemy)
    {
        Vector2Int[] targets = TakeTarget(enemy);
        Vector2Int closestTarget = Vector2Int.zero;
        var shortestDistance = float.MaxValue;

        foreach (var target in targets)
        {
            if(target.x <= -1 || target.x >= 15 || target.y <= -1 || target.y >= 6) continue;
            var distance = Vector2.Distance(transform.position, target);
            if(_fightingGrid.Value[target.x,target.y] != null) continue;
            if (!(distance < shortestDistance)) continue;
            
            shortestDistance = distance;
            closestTarget = target;
        }
        return closestTarget;
    }

    private Unit FindClosestUnit(Unit[] enemys)
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
