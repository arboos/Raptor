using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
    public static ShopManager Instance { get; private set; }
    
    [SerializeField] private List<ShopItem> items;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private HorizontalLayoutGroup parentHorizontal;

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
            switch (index)
            {
                case 0:
                    print("ROCKET DAMAGE BOUGHT");
                    itemLoader.Deacticate();
                    break;
            }
        }
    }

}
