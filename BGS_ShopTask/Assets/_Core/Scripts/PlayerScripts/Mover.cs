using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Class Variables")]
    public Vector2 moveDirection;
    public Vector2 lastMoveDirection;
    public float moveSpeed;

    [Header("Components")]
    private Rigidbody2D rigidBody;


    void Start()
    {
        moveDirection = new Vector2();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RegisterInput();
        Move();
    }

    private void RegisterInput()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(xMove, yMove).normalized;

        //Store last moved direction to properly flip the sprite when the user stops moving.

        if (moveDirection.x != 0)
        {
            lastMoveDirection.x = moveDirection.x;
        }

        if (moveDirection.y != 0)
        {
            lastMoveDirection.y = moveDirection.y;
        }

    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
