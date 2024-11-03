using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int count;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInfo.Instance.playerEconomic.money += count;
            SoundsBaseCollection.Instance.Coin.Play();
            UIManager.Instance.MoneyText.text = PlayerInfo.Instance.playerEconomic.money.ToString();
            UIManager.Instance.MoneyText_Shadow.text = PlayerInfo.Instance.playerEconomic.money.ToString();
            Destroy(gameObject);
        }
    }
}
