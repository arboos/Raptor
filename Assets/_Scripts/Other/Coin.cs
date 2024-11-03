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
            Destroy(gameObject);
        }
    }
}
