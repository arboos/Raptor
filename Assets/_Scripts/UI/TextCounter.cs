using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextCounter : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawner;

    private TextMeshPro textMP;
    
    void Awake()
    {
        textMP = GetComponent<TextMeshPro>();
        Count();   
    }

    private async void Count()
    {
        textMP.text = "3";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        
        textMP.text = "2";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        
        textMP.text = "1";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        textMP.text = "В бой!";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        enemySpawner.SetActive(true);
        Destroy(gameObject);
    }
    
}
