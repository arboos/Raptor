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
    [SerializeField] private bool resetShop = false;
    
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
        if(resetShop) ResetShop();
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
        UnlockPlayer();
    }

    public void UnlockPlayer()
    {
        transform.position = new Vector3(0f, -3f, 0f);
        PlayerInfo.Instance.playerAttack.canShoot = true;
        PlayerInfo.Instance.playerMovement.canMove = true;
    }

    private void ResetShop()
    {
        foreach (var item in items)
        {
            item.boughtYet = false;
        }
    }
}
