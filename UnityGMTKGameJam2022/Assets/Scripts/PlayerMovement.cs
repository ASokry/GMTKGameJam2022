using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Move.Enable();
    }

    private void FixedUpdate()
    {
        Player_Move();
    }

    private void Player_Move()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(inputVector.normalized * Time.deltaTime * moveSpeed);
    }
}
