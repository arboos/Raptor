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
    
    private void Update()
    {
        currentShootCooldown -= Time.deltaTime;
        
        if(currentShootCooldown<=0) Shoot();
    }

    protected virtual void Shoot() {}
}
