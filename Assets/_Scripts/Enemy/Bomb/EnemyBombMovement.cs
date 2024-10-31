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
        StartCoroutine(SetDirection());
    }

    private IEnumerator SetDirection()
    {
        yield return new WaitForEndOfFrame();
        direction = PlayerInfo.Instance.transform.position - transform.position;
        direction.Normalize();
    }
    
    protected override void Move()
    {
        Vector3 moveVector = direction * (Time.deltaTime * -speedY);
        transform.position += moveVector;
    }

}
