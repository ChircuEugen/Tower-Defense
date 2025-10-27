using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDataSO data;

    public static event Action<EnemyDataSO> OnEnemyReachedEnd;
    public static event Action<Enemy> OnEnemyDestroyed;

    private Vector3 _targetPosition;
    private Path currentPath;
    private int currentWaypoint;

    private float lives;

    private void Awake()
    {
        currentPath = GameObject.FindGameObjectWithTag("Path").GetComponent<Path>();
    }

    private void OnEnable()
    {
        currentWaypoint = 0;
        _targetPosition = currentPath.GetPosition(currentWaypoint);
        lives = data.lives;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, data.speed * Time.deltaTime);

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
                OnEnemyReachedEnd?.Invoke(data);
                gameObject.SetActive(false);
            }
            
        }
    }

    public void TakeDamage(float damage)
    {
        lives -= damage;

        if(lives < 0)
        {
            OnEnemyDestroyed?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
