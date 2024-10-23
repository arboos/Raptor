using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private GameObject playerDefaultBullet;

    public float shootCooldown;
    private float currentShootCooldown;

    public int bulletCount;
    public float betweenBulletDistance;
    
    private float value;
    
    private void Start()
    {
        if (PlayerInfo.Instance.playerAttack == null)
        {
            PlayerInfo.Instance.playerAttack = this;
        }
    }

    private void Update()
    {
        currentShootCooldown -= Time.deltaTime;
        
        if(value > 0) Shoot(playerDefaultBullet, bulletCount, betweenBulletDistance);
    }
    
    public void HandleAttackValue(InputAction.CallbackContext context)
    {
        value = context.ReadValue<float>();
    }
    
    public void Shoot(GameObject bulletPrefab, int bulletCount, float betweenBulletsDistance)
    {
        if (currentShootCooldown <= 0)
        {
            currentShootCooldown = shootCooldown;
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject spawnedBullet = Instantiate(bulletPrefab);
                float startPosX = -(((bulletCount - 1) * betweenBulletsDistance)) / 2;
                float bulletOffsetX = startPosX + i * betweenBulletsDistance;

                Vector3 spawnPos = bulletSpawnPosition.position;
                spawnPos.x += bulletOffsetX;

                spawnedBullet.transform.position = spawnPos;
            }
        }
    }
    
    public void Shoot()
    {
        GameObject bulletPrefab = playerDefaultBullet;
        int bulletCountA = 2;
        float betweenBulletsDistanceA = 0.5f;
        
        if (currentShootCooldown <= 0)
        {
            currentShootCooldown = shootCooldown;
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject spawnedBullet = Instantiate(bulletPrefab);
                float startPosX = -(((bulletCountA - 1) * betweenBulletsDistanceA)) / 2;
                float bulletOffsetX = startPosX + i * betweenBulletsDistanceA;

                Vector3 spawnPos = bulletSpawnPosition.position;
                spawnPos.x += bulletOffsetX;

                spawnedBullet.transform.position = spawnPos;
            }
        }
    }
}
