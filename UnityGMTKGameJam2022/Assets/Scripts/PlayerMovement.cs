using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float moveSpeed = 3f;
    private PlayerInputActions playerInputActions;

    Rigidbody2D rb;

    [SerializeField, Range(0f, 20f)]
    float maxAcceleration = 10f;
    Vector2 inputVector;
    Vector2 velocity;
    Camera cam;



    [SerializeField]Rect allowedArea;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        //allowedArea = Camera.main.rect;
        cam = Camera.main;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Update()
    {

        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();

       // transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2f, 2f),
       //     Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);

    }

    private void FixedUpdate()
    {
        Player_Move();
    }

    private void Player_Move()
    {
        velocity = rb.velocity;

        Vector2 desiredVelocity = inputVector * moveSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);

        rb.velocity = velocity;

        /*

        Vector3 displacement = velocity * Time.deltaTime;

        //transform.Translate(inputVector.normalized * Time.deltaTime * moveSpeed);

        //Vector2 newPosition = rb.position;
        Vector2 newPosition = transform.localPosition + displacement;

        if (newPosition.x < allowedArea.xMin)
        {
            newPosition.x = allowedArea.xMin;
            velocity.x = 0f;
        }
        else if (newPosition.x > allowedArea.xMax)
        {
            newPosition.x = allowedArea.xMax;
            velocity.x = 0f;
        }
        if (newPosition.y < allowedArea.yMin)
        {
            newPosition.y = allowedArea.yMin;
            velocity.y = 0f;
        }
        else if (newPosition.y > allowedArea.yMax)
        {
            newPosition.y = allowedArea.yMax;
            velocity.y = 0f;
        }

        transform.localPosition = newPosition;
        //rb.MovePosition(newPosition);

        */
    }
}
