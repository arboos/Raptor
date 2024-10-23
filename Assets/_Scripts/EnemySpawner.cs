using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWave> Waves = new List<EnemyWave>();

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public IEnumerator Spawner()
    {
        foreach (var wave in Waves)
        {
            foreach (var enemy in wave.enemies)
            {
                GameObject spawnedEnemy = Instantiate(enemy.prefab);
                spawnedEnemy.GetComponent<EnemyMovement>().directionX = enemy.directionX;
                spawnedEnemy.transform.position = new Vector3(enemy.spawnPositionX, transform.position.y, transform.position.z);
            }

            yield return new WaitForSeconds(wave.waitAfter);
        }
    }
}

[System.Serializable]
public class EnemyWave
{
    public List<EnemyPreset> enemies;
    public float waitAfter;
}

[System.Serializable]
public class EnemyPreset
{
    public GameObject prefab;
    public float spawnPositionX;
    public int directionX = 1;
}
