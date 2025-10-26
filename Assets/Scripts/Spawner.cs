using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private WaveDataSO[] waves;
    private int currentWaveIndex = 0;
    private WaveDataSO currentWave => waves[currentWaveIndex];

    private float spawnTimer;
    private float spawnCounter;
    private int enemiesRemoved;

    [SerializeField] private ObjectPooler eyeCrowPool;
    [SerializeField] private ObjectPooler dragonPool;
    [SerializeField] private ObjectPooler whiteDragonPool;

    private Dictionary<EnemyType, ObjectPooler> poolDictionary;

    private void OnEnable()
    {
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd;
    }

    private void Awake()
    {
        poolDictionary = new Dictionary<EnemyType, ObjectPooler>()
        {
            {EnemyType.EyeCrow, eyeCrowPool },
            {EnemyType.Dragon, dragonPool },
            {EnemyType.WhiteDragon, whiteDragonPool },
        };
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0 && spawnCounter < currentWave.enemiesPerWave)
        {
            spawnTimer = currentWave.spawnInterval;
            SpawnEnemy();
            spawnCounter++;
        }
        else if(spawnCounter >= currentWave.enemiesPerWave && enemiesRemoved >= currentWave.enemiesPerWave)
        {
            currentWaveIndex = (currentWaveIndex + 1) % waves.Length;
            spawnCounter = 0;
            enemiesRemoved = 0;
        }
    }

    private void SpawnEnemy()
    {
        if(poolDictionary.TryGetValue(currentWave.enemyType, out var pool))
        {
            GameObject spawnedObject = pool.GetPooledObject();
            spawnedObject.transform.position = transform.position;
            spawnedObject.SetActive(true);
        }
    }

    private void HandleEnemyReachedEnd(EnemyDataSO data)
    {
        enemiesRemoved++;
    }    
}
