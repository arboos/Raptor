using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Iceland_HealthSystem : HealthSystem
{
    [SerializeField] private GameObject bonusToSpawn;
    
    protected override void Die()
    {
        GetComponent<Animator>().SetBool("IsDead", true);

        GameObject spawnedBonus = Instantiate(bonusToSpawn);
        spawnedBonus.transform.position = transform.position;
        spawnedBonus.transform.SetParent(this.transform);
        
        GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.tag = "Untagged";
        gameObject.layer = 0;

        gameObject.GetComponent<Iceland_HealthSystem>().enabled = false;
    }
    
}
