using System.Collections.Generic;
using MEC;
using UnityEngine;
using Pathfinding;

[HelpURL("https://arongranberg.com/astar/documentation/stable/class_partial1_1_1_astar_a_i.php")]
public class AstarAI : MonoBehaviour
{
    public Transform targetPosition;

    private Seeker seeker;

    [SerializeField] private Vector3 _vector3MoveDirection;
    public Path path;
    

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;
    private Vector2[] validDirections = {Vector2.right, Vector2.left, Vector2.up, Vector2.down};
    
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        p.Claim(this);
        if (!p.error)
        {
            if (path != null) path.Release(this);
            path = p;
            currentWaypoint = 0;
        }
        else
        {
            p.Release(this);
        }
    }

    public void Update()
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
            if (distanceToWaypoint < nextWaypointDistance)
            {
                
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }
        var dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        
        transform.position += dir * (Time.deltaTime * 3.0f);
    }
    
}