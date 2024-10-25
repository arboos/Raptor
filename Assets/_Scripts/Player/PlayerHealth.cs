using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthSystem
{
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
    
}
