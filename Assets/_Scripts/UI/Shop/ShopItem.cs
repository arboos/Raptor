using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShopItem : ScriptableObject
{
    [Header("INDEX")] 
    public int index;
    
    [Header("Economic")]
    public bool boughtYet = false;
    public int price;

    [Header("Settings")]
    public Sprite icon;
    public string name;
    public string info;
    public string characteristics;
}
