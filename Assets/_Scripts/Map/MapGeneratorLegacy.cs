using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorLegacy : MonoBehaviour
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
        SpawnChunck();
    }
    
    public void SpawnChunck()
    {
        GameObject tileList = Instantiate(tileParent);
        tileList.transform.SetParent(transform);
        tileList.transform.localPosition = new Vector3(0f, 0f, 0f);

        for (int i = chunksSpawnedCount*X_length; i < chunksSpawnedCount*X_length+X_length; i++)
        {
            for (int j = chunksSpawnedCount*Y_length; j < chunksSpawnedCount*Y_length+Y_length; j++)
            {
                n = Mathf.PerlinNoise((i + seed) / zoom, (j + seed) / zoom);

                if (n < 0.4f) // Самая низкая(глубокая) точка карты
                {
                    SpawnTile(i, j, tileList, waterTiles[0]);
                }
                else if (n >= 0.4f && n < 0.6f)
                {
                    SpawnTile(i, j, tileList, waterTiles[1]);
                }
                else if (n >= 0.6f && n < 0.8f)
                {
                    SpawnTile(i, j, tileList, waterTiles[2]);
                }
                else // Самая высокая точка карты
                {
                    SpawnTile(i, j, tileList, waterTiles[3]);
                }
            }
        }

        chunksSpawnedCount++;
        chunksSpawned.Add(tileList);
    }

    public void SpawnTile(int i, int j, GameObject tileList, GameObject prefab)
    {
        GameObject spawnedTile = Instantiate(prefab,
            new Vector3(i-chunksSpawnedCount*X_length, j-chunksSpawnedCount*Y_length, 0f) + tileList.transform.position,
            Quaternion.Euler(0, 0, 0));
        spawnedTile.gameObject.transform.SetParent(tileList.transform);
    }
}
