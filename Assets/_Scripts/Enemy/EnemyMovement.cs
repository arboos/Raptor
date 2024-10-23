using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float directionX;
    public float speedY;
    
    protected virtual void Move()
    { }

    private void Update()
    {
        Move();
    }
}
