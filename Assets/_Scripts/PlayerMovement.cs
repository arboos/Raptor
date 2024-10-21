using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }

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
        transform.position = new Vector3(transform.position.x + InputVector.x * Time.deltaTime, transform.position.y + InputVector.y * Time.deltaTime, 0) ;
    }
}
