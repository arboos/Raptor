using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> waterTiles;

    private float n;

    [SerializeField] private float seed;
    [SerializeField] private float zoom = 30f;
    
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private int X_length;
    [SerializeField] private int Y_length;
    
    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int i = 0; i < X_length; i++)
        {
            for (int j = 0; j < Y_length; j++)
            {
                n = Mathf.PerlinNoise((i + seed) / zoom, (j + seed) / zoom);

                if (n < 0.4f) // Самая низкая(глубокая) точка карты
                {
                    Instantiate(waterTiles[0], new Vector3(i, j, 0f) + spawnPosition.position, Quaternion.Euler(0, 0, 0)).gameObject.transform.SetParent(spawnPosition);
                }
                else if (n >= 0.4f && n < 0.6f)
                {
                    Instantiate(waterTiles[1], new Vector3(i, j, 0f) + spawnPosition.position, Quaternion.Euler(0, 0, 0)).gameObject.transform.SetParent(spawnPosition);;
                }
                else if (n >= 0.6f && n < 0.8f)
                {
                    Instantiate(waterTiles[2], new Vector3(i, j, 0f) + spawnPosition.position, Quaternion.Euler(0, 0, 0)).gameObject.transform.SetParent(spawnPosition);;
                }
                else // Самая высокая точка карты
                {
                    Instantiate(waterTiles[3], new Vector3(i, j, 0f) + spawnPosition.position, Quaternion.Euler(0, 0, 0)).gameObject.transform.SetParent(spawnPosition);;
                }
            }
        }
    }
}
