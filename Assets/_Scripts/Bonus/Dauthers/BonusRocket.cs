using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRocket : Bonus
{
    [SerializeField] private Weapon weapon;
    protected override void TakeBonus()
    {
        PlayerInfo.Instance.playerAttack.weaponList.Add(weapon);
        Destroy(gameObject);
    }
}
