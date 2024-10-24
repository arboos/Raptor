using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;

    public float lifeTime;

    [SerializeField] private string comparesWith;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        Move();
    }
    
    protected virtual void Move() {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(comparesWith))
        {
            print("Trigger");
            Hit(other);
        }
    }

    protected void Hit(Collider2D other)
    {
        print("Hit");
        other.GetComponent<HealthSystem>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
