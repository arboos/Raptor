using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;

    public float lifeTime;

    public bool destroyAfterHit = true;
    public bool globalPosition = true;
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
            Hit(other);
        }
    }

    protected void Hit(Collider2D other)
    {
        other.GetComponent<HealthSystem>().TakeDamage(damage);
        if(destroyAfterHit) Destroy(gameObject);
    }
}
