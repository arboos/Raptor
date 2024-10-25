using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float directionX;
    public float speedY;

    public Vector3 lastPosition;

    public Vector3 directionMovement;
    
    protected virtual void Move()
    { }

    private void Update()
    {
        Move();
        directionMovement = transform.position - lastPosition;
        lastPosition = transform.position;
    }
}
