using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriangleAttack : EnemyAttack
{
    protected override void Shoot()
    {
        currentShootCooldown = shootCooldown; 
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab);
            float startPosX = -(((bulletCount - 1) * betweenBulletDistance)) / 2;
            float bulletOffsetX = startPosX + i * betweenBulletDistance;
            
            Vector3 spawnPos = bulletSpawnPosition.position;
            spawnPos.x += bulletOffsetX;

            spawnedBullet.transform.position = spawnPos;
        }
    }
}
