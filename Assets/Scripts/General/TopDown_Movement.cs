// This script allows movement for the player in the topdown view
//
// Author: Robot and I Team
// Last modification date: 10-28-2022

using UnityEngine;

namespace PlayerControl
{
    public class TopDown_Movement : MonoBehaviour
    {
        // Public variables
        public float moveSpeed; // Inspector view modifiable
        public Rigidbody2D rb; // Associated sprite object

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
}
