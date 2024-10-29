using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigGuyAttack : EnemyAttack
{
    [SerializeField] private float bulletSpeed;
    
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

            Vector3 dir = new Vector3(1f, -1f, 0);
            if (i < 2) dir.x = -1f;
            
            spawnedBullet.GetComponent<Rigidbody2D>().velocity =
                dir.normalized * bulletSpeed;
        }
    }
    
}
