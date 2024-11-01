using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    public Vector3 moveVector;

    private void Update()
    {
        if (moveVector != Vector3.zero)
        {
            transform.position += moveVector;
        }
    }

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
