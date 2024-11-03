using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
    public static ShopManager Instance { get; private set; }
    
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private HorizontalLayoutGroup parentHorizontal;

    [SerializeField] private List<Weapon> weapons;
    
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
        
        LoadItems();
    }
    

    public void LoadItems()
    {
        foreach (var item in items)
        {
            GameObject spawnedItem = Instantiate(shopItemPrefab, parentHorizontal.transform);
            spawnedItem.GetComponent<ItemLoader>().Initialize(item);
        }
    }

    public void BuyBoost(int index, int price, ItemLoader itemLoader)
    {
        if (PlayerInfo.Instance.playerEconomic.Buy(price))
        {
            PlayerInfo.Instance.playerAttack.weaponList.Add(weapons[index]);
            itemLoader.Deacticate();
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
