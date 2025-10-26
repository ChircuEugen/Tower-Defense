using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Vector3 _targetPosition;
    private Path currentPath;
    private int currentWaypoint;

    private void Awake()
    {
        currentPath = GameObject.FindGameObjectWithTag("Path").GetComponent<Path>();
    }

    private void OnEnable()
    {
        currentWaypoint = 0;
        _targetPosition = currentPath.GetPosition(currentWaypoint);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

        float distanceFromTarget = (transform.position - _targetPosition).magnitude;
        if(distanceFromTarget < 0.1)
        {
            if(currentWaypoint < currentPath.waypoints.Length - 1)
            {
                currentWaypoint++;
                _targetPosition = currentPath.GetPosition(currentWaypoint);
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }
    }
}
