// Unity Script for the Player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Movement : MonoBehaviour
{
    // Public variables
    public float moveSpeed;
    public Rigidbody2D rb;

    // Private variables
    private Vector2 moveDirection;

    // Update is called once per frame, therefore based on frame rate
    void Update()
    {
        // Use this function for processing inputs from the user
        ProcessInputs();
    }

    // FixedUpdate is called on a consistent basis.
    // Results in less jaggy movement if used in physics calculations
    void FixedUpdate()
    {
        // Use this function for processing physics calculations
        Move();
    }

    void ProcessInputs()
    {
        // Create two variables for current X,Y axis
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Create a new variable for the move direction
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        // Calculate where to move to
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
