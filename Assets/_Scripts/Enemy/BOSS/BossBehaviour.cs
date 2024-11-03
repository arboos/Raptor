using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{

    public float actionSpeed;
    public int shootCount;

    public GameObject rocketPrefab;
    public GameObject plasmPrefab;
    public float plasmSpeed;
    
    private Vector3[] bossPoints = new[]
    {
        new Vector3(0f, 3f, 0f),
        new Vector3(-2f, 1.5f, 0f),
        new Vector3(-2f, 0f, 0f),
        new Vector3(0f, -1.5f, 0f),
        new Vector3(2f, 0f, 0f),
        new Vector3(2f, 1.5f, 0f),
    };

    [SerializeField] private Transform[] rocketSpawnPositions;
    [SerializeField] private Transform plasmSpawnPosition;

    private Animator animator;
    
    private void Awake()
    {
        BossActions();
    }

    public async void BossActions()
    {
        for(int i = 0; i < 9999; i++)
        {
            await Cycle();
        }
    }

    public async UniTask Cycle()
    {
        foreach (var point in bossPoints)
        {
            Tween moveTween = transform.DOMove(point, actionSpeed, false);
            await moveTween.ToUniTask();
            
            if (Random.value < 0.25f)
            {
                await Attack();
            }
        }
    }
    
    public async UniTask Attack()
    {
        for (int i = 0; i < shootCount; i++)
        {
            ShootPlasm();
            ShootRockets();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        }
    }
    
    public void ShootRockets()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject spawnedBullet = Instantiate(rocketPrefab);
            
            Vector3 spawnPos = rocketSpawnPositions[i].position;

            spawnedBullet.transform.position = spawnPos;
        }
    }
    
    public void ShootPlasm()
    {
        GameObject spawnedBullet = Instantiate(plasmPrefab);
        spawnedBullet.transform.position = plasmSpawnPosition.position;
        
        spawnedBullet.GetComponent<Rigidbody2D>().velocity =
            (transform.position - PlayerInfo.Instance.gameObject.transform.position).normalized * plasmSpeed;
        
    }
    

}
