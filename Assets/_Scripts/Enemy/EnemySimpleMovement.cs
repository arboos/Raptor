using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySimpleMovement : EnemyMovement
{
    public float moveXDistance;
    public float moveXTime;
    private bool movedXYet;

    public float offsetChance;
    
    protected override async void Move()
    {
        if (!movedXYet && Random.value < offsetChance) ;
        Vector3 moveVector = new Vector3();
        transform.position += moveVector;
    }
}
