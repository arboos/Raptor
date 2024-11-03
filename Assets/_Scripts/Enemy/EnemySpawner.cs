using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWave> Waves;
    [SerializeField] private TextAsset json;

    [SerializeField] private int startFrom;

    [SerializeField] private bool hasBossFight;

    private void Start()
    {
        ReadFile();
        StartCoroutine(Spawner());
    }
    
    public void ReadFile()
    {
        Waves = new List<EnemyWave>();
        Waves = JsonUtility.FromJson<EnemySpawnOrder>(json.ToString()).waves;
            
        print("File Read");
    }

    public GameObject FindPrefabByName(string name)
    {
        return Resources.Load("Prefabs/Enemies/"+name).GameObject();
    }
        
    public IEnumerator Spawner()
    {
        if (startFrom < 0)
        {
            startFrom = Waves.Count + startFrom;
        }
        for (int i = startFrom; i < Waves.Count; i++)
        {
            foreach (var enemy in Waves[i].enemies)
            {
                GameObject spawnedEnemy = Instantiate(FindPrefabByName(enemy.prefabName));
                spawnedEnemy.GetComponent<EnemyMovement>().directionX = enemy.directionX;
                spawnedEnemy.transform.position = new Vector3(enemy.spawnPositionX, transform.position.y, transform.position.z);
            }
            yield return new WaitForSeconds(Waves[i].waitAfter);
        }

        if (!hasBossFight)
        {
            yield return new WaitForSeconds(3f);
            UIManager.Instance.WictoryText.SetActive(true);
            SoundsBaseCollection.Instance.Soundtrack.Stop();
            SoundsBaseCollection.Instance.Win.Play();
            PlayerInfo.Instance.playerAttack.canShoot = false;
            PlayerInfo.Instance.playerMovement.canMove = false;
            
            
            yield return new WaitForSeconds(3f);
            UIManager.Instance.WictoryText.SetActive(false);
            UIManager.Instance.WictoryScreen.SetActive(true);
        } 
    }
}

[System.Serializable]
public class EnemySpawnOrder
{
    public List<EnemyWave> waves;
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
    public string prefabName;
    public float spawnPositionX;
    public int directionX = 1;
}
