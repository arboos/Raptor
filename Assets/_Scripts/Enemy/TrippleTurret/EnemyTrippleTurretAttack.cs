using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyTrippleTurretAttack : EnemyAttack
{
    [SerializeField] private float bulletSpeed;
    private Vector3[] vels = new[] {new Vector3(-0.3f ,-1, 0), new Vector3(0 ,-1, 0), new Vector3(0.3f ,-1, 0)};
    protected override async void Shoot()
    {
        currentShootCooldown = shootCooldown; 
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab);
            
            Vector3 spawnPos = bulletSpawnPosition.position;

            spawnedBullet.transform.position = spawnPos;

            spawnedBullet.GetComponent<Rigidbody2D>().velocity =
                vels[i].normalized * bulletSpeed;
        }
    }
}
