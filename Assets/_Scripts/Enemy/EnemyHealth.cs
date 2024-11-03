using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] private GameObject destroyParticle;
    private Tween colorChangeTween = null;
    public int money;
    
    public bool hasDeathAnim = false;

    public override void TakeDamage(int count)
    {
        base.TakeDamage(count);
        ToColor(Color.red, 0.1f);
    }

    protected override void Die()
    {
        if (destroyParticle != null)
        {
            GameObject spawnedParticle = Instantiate(destroyParticle);
            spawnedParticle.transform.position = transform.position;
            spawnedParticle.GetComponent<SelfDestroy>().moveVector = GetComponent<EnemyMovement>().directionMovement;
            SoundsBaseCollection.Instance.Explosion.Play();
        }

        if (money > 0)
        {
            GameObject spawnedMoney = Instantiate(GameManager.Instance.MoneyPrefab);
            spawnedMoney.transform.position = transform.position;
            spawnedMoney.GetComponent<Coin>().count = money;
        }
        
        if(!hasDeathAnim) Destroy(gameObject);
        else
        {
            GetComponent<Animator>().SetTrigger("Dead");
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
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
