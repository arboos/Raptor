using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBoatMovement : EnemyMovement
{

    public float moveXDistance;
    private float currentMoveXDistance;

    public float delayToX;
    private float currentDelayToX;

    public float moveXTime;
    private bool movedXYet;
    private bool movesX;

    public float moveChance;


    public void Awake()
    {
        if (directionX <= 0)
        {
            directionX = -1f;
            transform.position = new Vector3(11f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(-11f, transform.position.y, transform.position.z);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (!movedXYet && delayToX >= 0)
        {
            delayToX -= Time.deltaTime;
            if (delayToX <= 0) StartCoroutine(MoveX());
        }
        else if (!movedXYet && Random.value < moveChance / 60f) StartCoroutine(MoveX());
    }

    protected override void Move()
    {
        Vector3 moveVector = new Vector3(0f, speedY * Time.deltaTime, 0);
        transform.position += moveVector;
    }

    private IEnumerator MoveX()
    {
        movedXYet = true;
        
        while (currentMoveXDistance < moveXDistance)
        {
            currentMoveXDistance += (moveXDistance / moveXTime) * Time.deltaTime;
            transform.position +=
                new Vector3((moveXDistance / moveXTime) * Time.deltaTime * directionX, 0f, 0f);
            yield return new WaitForEndOfFrame();
        }
    }
}