using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEconomic : MonoBehaviour
{
    public int money;
    
    private void Start()
    {
        if (PlayerInfo.Instance.playerEconomic == null)
        {
            PlayerInfo.Instance.playerEconomic = this;
        }
    }

    public bool Buy(int count)
    {
        if (money >= count)
        {
            money -= count;
            return true;
        }
        else
        {
            return false;
        }
    }
}
