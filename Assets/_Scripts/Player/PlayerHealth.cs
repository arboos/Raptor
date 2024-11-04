using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : HealthSystem
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject[] parts;
    public GameObject smokeTale;

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
        PlayerInfo.Instance.playerAttack.canShoot = false;
        SoundsBaseCollection.Instance.Soundtrack.Stop();
        GameManager.Instance.EnemySpawner.gameObject.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        
        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
        
        SoundsBaseCollection.Instance.Lose.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        for (int i = 0; i < 25; i++)
        {
            GameObject spawnedEx = Instantiate(explosionPrefab);
            spawnedEx.transform.localScale *= 1.2f;
            spawnedEx.transform.position = transform.position +
                                           new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.8f, 0.8f), 0);
            spawnedEx.GetComponent<SpriteRenderer>().sortingOrder = 50;
            SoundsBaseCollection.Instance.Explosion.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        
        smokeTale.SetActive(false);
        PlayerInfo.Instance.playerAnimations.PlayerAnimator.SetBool("Dead", true);
        foreach (var part in parts)
        {
            GameObject spawnedPart = Instantiate(part);
            
            spawnedPart.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            
            spawnedPart.GetComponent<Rigidbody2D>().velocity = 
                new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * Random.Range(2.0f, 3.0f);
            
            spawnedPart.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-360f, 360f));
        }

        transform.GetChild(2).gameObject.SetActive(false);
        SoundsBaseCollection.Instance.Death.Play();
        
        
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SoundsBaseCollection.Instance.Lose.Play();
        UIManager.Instance.LoseScreen.SetActive(true);
        
        
    }

    public override void TakeDamage(int count)
    {
        base.TakeDamage(count);
        UIManager.Instance.healthBar.fillAmount = (float)Health / (float)MaxHealth;
        SoundsBaseCollection.Instance.Damage.Play();
        ToColor(Color.red, 0.2f);
        if (Health <= 0.5 * MaxHealth) smokeTale.SetActive(true);
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
        UIManager.Instance.healthBar.fillAmount = (float)Health / (float)MaxHealth;
        SoundsBaseCollection.Instance.Heal.Play();
        ToColor(Color.green, 0.2f);
    }
}
