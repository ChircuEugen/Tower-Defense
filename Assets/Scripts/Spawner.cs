using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static event Action<int> OnWaveChanged;

    [SerializeField] private WaveDataSO[] waves;
    private int currentWaveIndex = 0;
    private int waveCounter = 0;
    private WaveDataSO currentWave => waves[currentWaveIndex];

    private float spawnTimer;
    private float spawnCounter;
    private int enemiesRemoved;

    [SerializeField] private ObjectPooler eyeCrowPool;
    [SerializeField] private ObjectPooler dragonPool;
    [SerializeField] private ObjectPooler whiteDragonPool;

    private Dictionary<EnemyType, ObjectPooler> poolDictionary;

    private float timeBetweenWaves = 2f;
    private float waveCooldown;
    private bool isBetweenWaves = false;


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

    private void Awake()
    {
        poolDictionary = new Dictionary<EnemyType, ObjectPooler>()
        {
            {EnemyType.EyeCrow, eyeCrowPool },
            {EnemyType.Dragon, dragonPool },
            {EnemyType.WhiteDragon, whiteDragonPool },
        };
    }

    private void Start()
    {
        OnWaveChanged?.Invoke(currentWaveIndex);
    }

    private void Update()
    {
        if(isBetweenWaves)
        {
            waveCooldown -= Time.deltaTime;
            if(waveCooldown <= 0)
            {
                currentWaveIndex = (currentWaveIndex + 1) % waves.Length;
                waveCounter++;
                OnWaveChanged?.Invoke(waveCounter);
                spawnCounter = 0;
                enemiesRemoved = 0;
                spawnTimer = 0;
                isBetweenWaves = false;
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0 && spawnCounter < currentWave.enemiesPerWave)
            {
                spawnTimer = currentWave.spawnInterval;
                SpawnEnemy();
                spawnCounter++;
            }
            else if (spawnCounter >= currentWave.enemiesPerWave && enemiesRemoved >= currentWave.enemiesPerWave)
            {
                isBetweenWaves = true;
                waveCooldown = timeBetweenWaves;
            }
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
    
    private void HandleEnemyDestroyed(Enemy enemy)
    {
        enemiesRemoved++;
    }
}
