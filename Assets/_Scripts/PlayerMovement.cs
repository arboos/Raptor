using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }

    public float movementSpeed;

    private void Start()
    {
        if (PlayerInfo.Instance.playerMovement == null)
        {
            PlayerInfo.Instance.playerMovement = this;
        }
    }

    private void Update()
    {
        Move();
    }

    public void HandleMovementVector(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        
        Vector3 moveVector = new Vector3(InputVector.normalized.x * movementSpeed * Time.deltaTime,
            InputVector.normalized.y * movementSpeed * Time.deltaTime, 0);
        transform.position += moveVector;
    }
}
