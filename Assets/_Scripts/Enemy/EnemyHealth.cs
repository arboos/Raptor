using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] private GameObject destroyParticle;
    private Tween colorChangeTween = null;

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

        Destroy(gameObject);
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
