using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFirstAid : Bonus
{
    [SerializeField] private int healCount;
    
    protected override void TakeBonus()
    {
        PlayerInfo.Instance.playerHealth.TakeHeal(healCount);
        Destroy(gameObject);
    }
}
