using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBumblebeeAttack : EnemyAttack
{
    private Vector3[] plasmSpawnPos = new[]
    {
        new Vector3(-0.25f, -0.5f, 0f),
        new Vector3(0.25f, -0.5f, 0f),
        new Vector3(0.25f, -1f, 0f),
        new Vector3(-0.25f, -1f, 0f)

    };

    [SerializeField] private GameObject plasmPrefab;
    
    protected override void Shoot()
    {
        currentShootCooldown = shootCooldown; 
        ShootRockets();
        ShootPlasm();
    }

    private void ShootRockets()
    {
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

    private void ShootPlasm()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject spawnedBullet = Instantiate(plasmPrefab);

            spawnedBullet.transform.position = transform.position + plasmSpawnPos[i];
        }
    }
}
