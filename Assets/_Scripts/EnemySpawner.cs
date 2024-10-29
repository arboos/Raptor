using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWave> Waves;
    private string saveFile = "Assets/Resources/Level1.json";

    private void Start()
    {
        ReadFile();
        StartCoroutine(Spawner());
    }
    
    public void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            Waves = new List<EnemyWave>();
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching class.
            Waves = JsonUtility.FromJson<EnemySpawnOrder>(fileContents).waves;
            
            print("File Read");
        }
    }
    
    public void WriteFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(Waves);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    public GameObject FindPrefabByName(string name)
    {
        return Resources.Load("Prefabs/Enemies/"+name).GameObject();
    }
        
    public IEnumerator Spawner()
    {
        foreach (var wave in Waves)
        {
            foreach (var enemy in wave.enemies)
            {
                GameObject spawnedEnemy = Instantiate(FindPrefabByName(enemy.prefabName));
                spawnedEnemy.GetComponent<EnemyMovement>().directionX = enemy.directionX;
                spawnedEnemy.transform.position = new Vector3(enemy.spawnPositionX, transform.position.y, transform.position.z);
            }

            yield return new WaitForSeconds(wave.waitAfter);
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
