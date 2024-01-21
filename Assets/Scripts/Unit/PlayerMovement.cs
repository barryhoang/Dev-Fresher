using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Button _button;
    [SerializeField] private Vector3 _vector3MoveDirection;
    [SerializeField] private Vector3 pathDirection;
    
    [SerializeField] private float moveDuration;
    [SerializeField] private float gridSize = 1f;

    private Vector3 target;
    private NavMeshAgent agent;
    private NavMeshPath path;
    static float agentDrift = 0.0001f;

    public float raycastDistance = 1f;

    private Vector3[] validDirections = {Vector3.right, Vector3.left, Vector3.up, Vector3.down};
    private bool isMoving = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        path = new NavMeshPath();
        target = _enemy.transform.position + new Vector3(agentDrift, 0, 0);

        // Add a listener to the button click event
        /*_button.onClick.AddListener(MoveToTarget());*/
        Timing.RunCoroutine(_MoveUpdate());
    }

    IEnumerator<float> _MoveUpdate()
    {
        while (true)
        {
            agent.CalculatePath(target, path);

            // Get the direction vector from the agent's position to the next waypoint
            pathDirection = (path.corners[1] - agent.transform.position).normalized;
            pathDirection.z = 0f;

            _vector3MoveDirection = FindClosestValidDirection(pathDirection);

            // You can also visualize the path direction by drawing a line
            Debug.DrawLine(agent.transform.position, agent.transform.position + pathDirection * 5.0f, Color.red, 0.1f);

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 0.01f);
            }

            Timing.RunCoroutine(_MoveToTarget(target));
            yield return Timing.WaitForOneFrame;
        }
    }
    
    bool IsPositionOccupied(Vector3 targetPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out hit, raycastDistance))
        {
            // Check if the ray hits any collider
            return true;
        }
        return false;
    }

    private IEnumerator<float> _MoveToTarget(Vector3 targetPosition)
    {
        if (!IsPositionOccupied(targetPosition) && !isMoving)
        {
            // If the input function is active, move in the appropriate direction.
            if (_vector3MoveDirection == Vector3.up)
            {
                Timing.RunCoroutine(Move(Vector2.up));
            }
            else if (_vector3MoveDirection == Vector3.down)
            {
                Timing.RunCoroutine(Move(Vector2.down));
            }
            else if (_vector3MoveDirection == Vector3.left)
            {
                Timing.RunCoroutine(Move(Vector2.left));
            }
            else if (_vector3MoveDirection == Vector3.right)
            {
                Timing.RunCoroutine(Move(Vector2.right));
            }

            yield return Timing.WaitForOneFrame;
        }
        else
        {
            Debug.LogWarning("Target position is occupied. Cannot move.");
        }
    }

    // Tìm hướng gần nhất trong danh sách các hướng hợp lệ
    private Vector3 FindClosestValidDirection(Vector3 direction)
    {
        float minAngle = float.MaxValue;
        float maxDot = float.MinValue;
        Vector3 closestValidDirection = Vector3.zero;

        foreach (Vector3 validDir in validDirections)
        {
            float angle = Vector3.Angle(direction, validDir);
            float dot = Vector3.Dot(direction, validDir);

            if (angle < minAngle || (angle == minAngle && dot > maxDot))
            {
                minAngle = angle;
                maxDot = dot;
                closestValidDirection = validDir;
            }
        }

        return closestValidDirection;
    }

    private IEnumerator<float> Move(Vector2 direction)
    {
        isMoving = true;

        // Make a note of where we are and where we are going.
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        // Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return Timing.WaitForOneFrame;
        }

        // Make sure we end up exactly where we want.
        transform.position = endPosition;
        isMoving = false;
    }
}