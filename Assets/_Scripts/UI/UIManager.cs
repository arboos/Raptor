using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject LoseScreen;
    public GameObject WictoryScreen;
    public GameObject WictoryText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MoneyText_Shadow;
    public Image healthBar;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MoneyText.text = PlayerInfo.Instance.playerEconomic.money.ToString();
        MoneyText_Shadow.text = PlayerInfo.Instance.playerEconomic.money.ToString();
    }

    public void Pause()
    {
        
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
