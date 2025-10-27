using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private TowerDataSO towerData;
    private Vector3 shootDirection;
    private float projectileDuration;

    private void Update()
    {
        if(projectileDuration <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            projectileDuration -= Time.deltaTime;
            transform.position += new Vector3(shootDirection.x, shootDirection.y) * towerData.projectileSpeed * Time.deltaTime;
        }
    }

    public void Shoot(TowerDataSO data, Vector2 direction)
    {
        towerData = data;
        shootDirection = direction;
        projectileDuration = data.projectileDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(towerData.damage);
            gameObject.SetActive(false);
        }
    }
}
