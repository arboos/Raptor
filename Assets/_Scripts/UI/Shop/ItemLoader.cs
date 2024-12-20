using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemLoader : MonoBehaviour
{
    public ShopItem shopItem;

    [SerializeField] private Image icon;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI characteristicsText;
    
    
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    
    [SerializeField] private GameObject closeWindow;

    public void Initialize(ShopItem item)
    {
        shopItem = item;
        icon.sprite = shopItem.icon;
        nameText.text = shopItem.name;
        infoText.text = shopItem.info;
        characteristicsText.text = shopItem.characteristics;
        priceText.text = "$" + shopItem.price.ToString();
        buyButton.onClick.AddListener(delegate
        {
            ShopManager.Instance.BuyBoost(shopItem.index, shopItem.price, this);
        });
        
        if(shopItem.boughtYet) Deacticate();
    }

    public void Deacticate()
    {
        closeWindow.SetActive(true);
        shopItem.boughtYet = true;
        priceText.text = "Куплено";
        buyButton.enabled = false;
    }
}
