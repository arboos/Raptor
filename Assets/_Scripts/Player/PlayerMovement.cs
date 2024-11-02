using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }

    private Rigidbody2D rb;

    public float movementSpeed;
    public bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (PlayerInfo.Instance.playerMovement == null)
        {
            PlayerInfo.Instance.playerMovement = this;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void HandleMovementVector(InputAction.CallbackContext context)
    {
        if(canMove) InputVector = context.ReadValue<Vector2>();
        else InputVector = Vector2.zero;
    }

    private void Move()
    {
        Vector3 moveVector = new Vector3(InputVector.normalized.x * movementSpeed,
            InputVector.normalized.y * movementSpeed, 0);
        rb.velocity = moveVector;
    }
}
