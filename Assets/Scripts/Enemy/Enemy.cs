using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDataSO data;
    public EnemyDataSO Data => data;

    [SerializeField] private Transform healthBar;
    private Vector3 healthBarOriginalScale;

    public static event Action<EnemyDataSO> OnEnemyReachedEnd;
    public static event Action<Enemy> OnEnemyDestroyed;

    private Vector3 _targetPosition;
    private Path currentPath;
    private int currentWaypoint;

    private float lives;
    private float maxLives;

    private bool wasCounted = false;

    private void Awake()
    {
        currentPath = GameObject.FindGameObjectWithTag("Path").GetComponent<Path>();
        healthBarOriginalScale = healthBar.localScale;
    }

    private void OnEnable()
    {
        currentWaypoint = 0;
        _targetPosition = currentPath.GetPosition(currentWaypoint);
        //lives = data.lives;
        //UpdateHealthBar();
    }

    private void Update()
    {
        if (wasCounted) return;

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
                wasCounted = true;
                OnEnemyReachedEnd?.Invoke(data);
                gameObject.SetActive(false);
            }
            
        }
    }

    public void Initialize(float healthMultiplier)
    {
        wasCounted = false;
        maxLives = data.lives * healthMultiplier;
        lives = maxLives;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        lives -= damage;
        UpdateHealthBar();
        if (lives <= 0)
        {
            wasCounted = true;
            OnEnemyDestroyed?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercent = lives / maxLives;
        Vector3 scale = healthBarOriginalScale;
        scale.x = healthBarOriginalScale.x * healthPercent;
        healthBar.localScale = scale;
    }
}
