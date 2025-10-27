using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnLivesChanged;
    private int playerLives = 20;

    private void OnEnable()
    {
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd;
    }

    private void Start()
    {
        OnLivesChanged?.Invoke(playerLives);
    }
    private void HandleEnemyReachedEnd(EnemyDataSO data)
    {
        playerLives -= data.damage;
        OnLivesChanged?.Invoke(playerLives);
    }
}
