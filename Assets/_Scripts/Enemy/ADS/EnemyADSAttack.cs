using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyADSAttack : EnemyAttack
{
    [SerializeField] private float bulletSpeed;
    protected override async void Shoot()
    {
        currentShootCooldown = shootCooldown; 
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab);
            
            Vector3 spawnPos = bulletSpawnPosition.position;

            spawnedBullet.transform.position = spawnPos;

            spawnedBullet.GetComponent<Rigidbody2D>().velocity =
                (transform.position - PlayerInfo.Instance.gameObject.transform.position).normalized * bulletSpeed;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
    }
}
