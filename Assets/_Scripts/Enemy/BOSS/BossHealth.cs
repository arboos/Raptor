using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossHealth : HealthSystem
{
    [SerializeField] private GameObject destroyParticle;
    private Tween colorChangeTween = null;
    public int money;
    
    public bool hasDeathAnim = false;
    private BossBehaviour bossBehaviour;
    [SerializeField] private GameObject explosionPrefab;
    
    private void Awake()
    {
        bossBehaviour = GetComponent<BossBehaviour>();
    }

    public override void TakeDamage(int count)
    {
        if (bossBehaviour.isAlive)
        {
            base.TakeDamage(count);
            ToColor(Color.red, 0.1f);
            if (Health <= 0.5f * MaxHealth && Health > 0.25f * MaxHealth)
            {
                bossBehaviour.healthImage.color = Color.yellow;
            }
            else if (Health <= 0.25f * MaxHealth)
            {
                bossBehaviour.healthImage.color = Color.red;
                bossBehaviour.actionSpeed = 0.75f;
            }
        }
    }

    protected override async void Die()
    {
        if (bossBehaviour.isAlive)
        {
            bossBehaviour.isAlive = false;
            GetComponent<Animator>().SetTrigger("Dead");
            
            PlayerInfo.Instance.playerMovement.canMove = false;
            PlayerInfo.Instance.playerAttack.canShoot = false;
            SoundsBaseCollection.Instance.Soundtrack.Stop();
            
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        
            foreach (var bullet in bullets)
            {
                Destroy(bullet);
            }
        
            SoundsBaseCollection.Instance.Win.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            for (int i = 0; i < 40; i++)
            {
                GameObject spawnedEx = Instantiate(explosionPrefab);
                spawnedEx.transform.localScale *= 1.4f;
                spawnedEx.transform.position = transform.position +
                                               new Vector3(Random.Range(-2f, 2f), Random.Range(-1.5f, 1.5f), 0);
                spawnedEx.GetComponent<SpriteRenderer>().sortingOrder = 50;
                SoundsBaseCollection.Instance.Explosion.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            }

            Tween moveTween = transform.DOMove(new Vector3(15f, 0f, 0f), 3f);
            await moveTween.ToUniTask();

        
        

            UIManager.Instance.WictoryText.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            SoundsBaseCollection.Instance.Win.Play();
            
            await UniTask.Delay(TimeSpan.FromSeconds(4f));
            UIManager.Instance.WictoryText.SetActive(false);
            UIManager.Instance.WictoryScreen.SetActive(true);
        }
    }
    
    public async UniTask ToColor(Color color, float duration)
    {
        if (colorChangeTween != null) return;
        else
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color startColor = spriteRenderer.color;
            
            colorChangeTween = spriteRenderer.DOColor(color, duration);
            await colorChangeTween.ToUniTask();
            
            Tween tweenBack = spriteRenderer.DOColor(startColor, 0.1f);
            await tweenBack.ToUniTask();

            colorChangeTween = null;
        }
    }
}
