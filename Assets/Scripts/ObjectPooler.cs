using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> pool;

    private void Start()
    {
        pool = new List<GameObject>();

        for(int i=0; i< poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(enemyPrefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in pool)
        {
            if(!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }
}
