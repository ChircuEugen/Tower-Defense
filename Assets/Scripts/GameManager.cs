using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnLivesChanged;
    public static event Action<int> OnResourcesChanged;
    private int playerLives = 20;
    private int resources = 0;

    private void OnEnable()
    {
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd;
        Enemy.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd;
        Enemy.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void Start()
    {
        OnLivesChanged?.Invoke(playerLives);
        OnResourcesChanged?.Invoke(resources);
    }
    private void HandleEnemyReachedEnd(EnemyDataSO data)
    {
        playerLives -= data.damage;
        OnLivesChanged?.Invoke(playerLives);
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        AddResources(enemy.Data.rewardResource);
    }

    private void AddResources(int amount)
    {
        resources += amount;
        OnResourcesChanged?.Invoke(resources);
    }
}
