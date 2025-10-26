using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnTimer;
    public float spawnInterval = 1f;

    [SerializeField] private ObjectPooler pool;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0)
        {
            spawnTimer = spawnInterval;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject spawnedObject = pool.GetPooledObject();
        spawnedObject.transform.position = transform.position;
        spawnedObject.SetActive(true);
        
    }
}
