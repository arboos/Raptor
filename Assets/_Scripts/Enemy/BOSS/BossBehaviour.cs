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
    public bool isAlive;
    
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
    public SpriteRenderer healthImage;
    
    private void Awake()
    {
        BossActions();
        animator = GetComponent<Animator>();
    }
    

    public async void BossActions()
    {
        while (isAlive)
        {
            await Cycle();
        }
    }

    public async UniTask Cycle()
    {
        if(!isAlive) return;
        foreach (var point in bossPoints)
        {
            if(!isAlive) return;
            Tween moveTween = transform.DOMove(point, actionSpeed, false);
            await moveTween.ToUniTask();
            
            if (Random.value < 0.5f)
            { 
                if(!isAlive) return;
                animator.SetBool("Attack", true);
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                
                await Attack();
                animator.SetBool("Attack", false);
            }
        }
    }
    
    public async UniTask Attack()
    {
        for (int i = 0; i < shootCount; i++)
        {
            if(!isAlive) return;
            ShootPlasm();
            ShootRockets();
            await UniTask.Delay(TimeSpan.FromSeconds(actionSpeed/4f));
        }
    }
    
    public void ShootRockets()
    {
        for (int i = 0; i < 4; i++)
        {
            if(!isAlive) return;
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
