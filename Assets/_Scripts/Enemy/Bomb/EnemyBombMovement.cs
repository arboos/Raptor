using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBombMovement : EnemyMovement
{
    private Vector3 direction;
    
    private void Awake()
    {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 0.5f), 0f);
    }

    protected override void Move()
    {
        Vector3 moveVector = direction * (Time.deltaTime * speedY);
        transform.position += moveVector;
    }

}
