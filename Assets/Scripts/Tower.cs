using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerDataSO data;
    private CircleCollider2D circleCollider;
    private ObjectPooler projectilePool;

    private List<Enemy> enemiesInRange;

    private float shootTimer;


    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = data.range;
        enemiesInRange = new List<Enemy>();
        projectilePool = GetComponent<ObjectPooler>();
        shootTimer = data.shootInterval;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            shootTimer = data.shootInterval;
            Shoot();
        }
    }

    private void Shoot()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeInHierarchy);

        if(enemiesInRange.Count > 0)
        {
            GameObject projectile = projectilePool.GetPooledObject();
            projectile.transform.position = transform.position;
            projectile.SetActive(true);
            Vector2 shootDirection = (enemiesInRange[0].transform.position - transform.position).normalized;
            projectile.GetComponent<Projectile>().Shoot(data, shootDirection);

        }
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}
