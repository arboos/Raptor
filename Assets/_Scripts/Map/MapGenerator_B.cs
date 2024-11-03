using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneratorB : MonoBehaviour
{
    [SerializeField] private GameObject tileParent;
    
    public List<GameObject> waterTiles;

    private float n;

    public int chunksSpawnedCount;
        
    
    [SerializeField] private float seed;
    [SerializeField] private float zoom = 30f;
    
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private int X_length;
    [SerializeField] private int Y_length;
    public bool spawnNewTiles;

    [SerializeField] private List<GameObject> chunksSpawned = new List<GameObject>();
    
    private void Start()
    {
        SpawnChunck(-8);
        SpawnChunck(4);
        SpawnChunck(16);
        
        StartCoroutine(GenerateMap());
    }

    private IEnumerator GenerateMap()
    {
        while (spawnNewTiles)
        {
            yield return new WaitForSeconds(4f);
            SpawnChunck();
            chunksSpawned.Remove(transform.GetChild(0).gameObject);
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void SpawnChunck(int offsetY)
    {
        GameObject tileList = Instantiate(tileParent);
        tileList.transform.SetParent(transform);
        tileList.transform.position = new Vector3(-(float)X_length * 0.25f, offsetY, 0f);

        for (int i = 0; i < X_length; i++)
        {
            for (int j = 0; j < Y_length; j++)
            {
                n = Mathf.PerlinNoise((i + seed) / zoom, (j + chunksSpawnedCount*Y_length + seed) / zoom);

                if (n < 0.05f) // Самая низкая(глубокая) точка карты
                {
                    SpawnTile(i, j, tileList, waterTiles[0]);
                }
                else if (n >= 0.05f && n < 0.1f)
                {
                    SpawnTile(i, j, tileList, waterTiles[1]);
                }
                else if (n >= 0.1f && n < 0.2f)
                {
                    SpawnTile(i, j, tileList, waterTiles[2]);
                }
                else if (n >= 0.2f)
                {
                    SpawnTile(i, j, tileList, waterTiles[Random.Range(3, 8)]);
                }
            }
        }
        chunksSpawnedCount++;
        chunksSpawned.Add(tileList);
    }
    
    public void SpawnChunck()
    {
        GameObject tileList = Instantiate(tileParent);
        tileList.transform.SetParent(transform);
        tileList.transform.position = new Vector3(-(float)X_length * 0.25f, transform.GetChild(2).transform.position.y + (float)Y_length/2f, 0f);
        
        for (int i = 0; i < X_length; i++)
        {
            for (int j = 0; j < Y_length; j++)
            {
                n = Mathf.PerlinNoise((i + seed) / zoom, (j + chunksSpawnedCount*Y_length + seed) / zoom);

                if (n < 0.05f) // Самая низкая(глубокая) точка карты
                {
                    SpawnTile(i, j, tileList, waterTiles[0]);
                }
                else if (n >= 0.05f && n < 0.1f)
                {
                    SpawnTile(i, j, tileList, waterTiles[1]);
                }
                else if (n >= 0.1f && n < 0.2f)
                {
                    SpawnTile(i, j, tileList, waterTiles[2]);
                }
                else if (n >= 0.2f)
                {
                    SpawnTile(i, j, tileList, waterTiles[Random.Range(3, 8)]);
                }
            }
        }

        chunksSpawnedCount++;
        chunksSpawned.Add(tileList);
    }

    public void SpawnTile(int i, int j, GameObject tileList, GameObject prefab)
    {
        GameObject spawnedTile = Instantiate(prefab);
        spawnedTile.transform.localPosition = new Vector3((float)i/2f, (float)j/2f, 0f) + tileList.transform.position;
        spawnedTile.gameObject.transform.SetParent(tileList.transform);
    }

}
