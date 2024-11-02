using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : HealthSystem
{

    [SerializeField] private Image healthBar;

    private Tween colorChangeTween = null;
    
    private void Start()
    {
        if (PlayerInfo.Instance.playerHealth == null)
        {
            PlayerInfo.Instance.playerHealth = this;
        }
    }

    protected override async void Die()
    {
        PlayerInfo.Instance.playerMovement.canMove = false;
        SoundsBaseCollection.Instance.Death.Play();
        
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void TakeDamage(int count)
    {
        base.TakeDamage(count);
        healthBar.fillAmount = (float)Health / (float)MaxHealth;
        SoundsBaseCollection.Instance.Damage.Play();
        ToColor(Color.red, 0.2f);
    }

    public async UniTask ToColor(Color color, float duration)
    {
        if (colorChangeTween != null) return;
        else
        {
            colorChangeTween = PlayerInfo.Instance.PlayerSpriteRenderer.DOColor(color, duration);
            Color startColor = PlayerInfo.Instance.PlayerSpriteRenderer.color;
            await colorChangeTween.ToUniTask();
            
            Tween tweenBack = PlayerInfo.Instance.PlayerSpriteRenderer.DOColor(startColor, 0.1f);
            await tweenBack.ToUniTask();

            colorChangeTween = null;
        }
    }

    public override void TakeHeal(int count)
    {
        base.TakeHeal(count);
        healthBar.fillAmount = (float)Health / (float)MaxHealth;
        SoundsBaseCollection.Instance.Heal.Play();
        ToColor(Color.green, 0.2f);
    }
}
