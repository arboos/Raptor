using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int Health;
    [SerializeField] protected int MaxHealth;

    public virtual void TakeDamage(int count)
    {
        Health -= count;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public virtual void TakeHeal(int count)
    {
        Health += count;
        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    protected virtual void Die() { }
}
