using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTextUpdate : MonoBehaviour
{
    private TextMeshProUGUI textMP;

    private void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        textMP.text = PlayerInfo.Instance.playerEconomic.money.ToString();
    }
}
