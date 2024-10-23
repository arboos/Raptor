using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
