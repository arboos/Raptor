using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public Transform bulletSpawnPosition;
    [SerializeField] protected GameObject bulletPrefab;

    public float shootCooldown;
    protected float currentShootCooldown;

    public int bulletCount;
    public float betweenBulletDistance;

    public int collisionDamage;
    
    private void Update()
    {
        currentShootCooldown -= Time.deltaTime;
        
        if(currentShootCooldown<=0) Shoot();
    }

    protected virtual void Shoot() {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthSystem>().TakeDamage(collisionDamage);
            SoundsBaseCollection.Instance.Collision.Play();
            GetComponent<HealthSystem>().TakeDamage(1000);
        }
    }
}
