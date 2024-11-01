using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] private GameObject destroyParticle;
    
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
}
