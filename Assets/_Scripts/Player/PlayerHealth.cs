using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : HealthSystem
{

    [SerializeField] private Image healthBar;
    
    private void Start()
    {
        if (PlayerInfo.Instance.playerHealth == null)
        {
            PlayerInfo.Instance.playerHealth = this;
        }
    }

    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void TakeDamage(int count)
    {
        base.TakeDamage(count);
        healthBar.fillAmount = (float)Health / (float)MaxHealth;
    }

    public override void TakeHeal(int count)
    {
        base.TakeHeal(count);
        healthBar.fillAmount = (float)Health / (float)MaxHealth;
    }
}
