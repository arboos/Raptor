using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrippleTurretMovement : EnemyMovement
{
    protected override void Move()
    {
        Vector3 moveVector = new Vector3(0f, speedY * Time.deltaTime, 0);
        transform.position += moveVector;
    }
}
