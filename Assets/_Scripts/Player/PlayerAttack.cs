using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float value;

    public List<Weapon> weaponList;

    public bool canShoot = true;
    
    private void Start()
    {
        if (PlayerInfo.Instance.playerAttack == null)
        {
            PlayerInfo.Instance.playerAttack = this;
        }
    }

    private void Update()
    {
        if(canShoot) ShootAllWeapons();
        
        foreach (var weapon in weaponList)
        {
            weapon.currentShootCooldown -= Time.deltaTime;
        }
    }
    
    public void HandleAttackValue(InputAction.CallbackContext context)
    {
        value = context.ReadValue<float>();
    }

    private void ShootAllWeapons()
    {
        if(value <= 0) return;
        
        foreach (var weapon in weaponList)
        {
            if(weapon.currentShootCooldown <= 0) Shoot(weapon);
        }
    }

    private void Shoot(Weapon weapon)
    {
        weapon.currentShootCooldown = weapon.shootCooldown;
        
        for (int i = 0; i < weapon.bulletCount; i++)
        {
            if (weapon.bulletPrefab.gameObject.name == "_Player_Laser")
            {
                weapon.bulletSpawnPosition = transform.GetChild(1);
            }
            GameObject spawnedBullet = Instantiate(weapon.bulletPrefab);
            if (!weapon.bulletPrefab.GetComponent<Bullet>().globalPosition)
                spawnedBullet.transform.SetParent(gameObject.transform);
            float startPosX = -(((weapon.bulletCount - 1) * weapon.betweenBulletsDistance)) / 2;
            float bulletOffsetX = startPosX + i * weapon.betweenBulletsDistance;

            if (weapon.bulletSpawnPosition == null) weapon.bulletSpawnPosition = transform.GetChild(0);
            Vector3 spawnPos = weapon.bulletSpawnPosition.position;
            spawnPos.x += bulletOffsetX;

            spawnedBullet.transform.position = spawnPos;
        }
    }
    
}

[System.Serializable]
public class Weapon
{
    public Transform bulletSpawnPosition;
    
    public GameObject bulletPrefab;

    public float betweenBulletsDistance;
    
    public float shootCooldown;
    public float currentShootCooldown;
    
    public int bulletCount;
    
}
