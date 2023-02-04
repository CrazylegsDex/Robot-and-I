// This script allows control of bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 02-03-2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelBit
{
    public class Bit_Level_Script : MonoBehaviour
    {
        // Public variables - Inspector View modifiable
        public int maxAirJumps;
        public float moveSpeed;
        public float jumpForce;
        public float checkRadius;
        public Transform ceilingCheck;
        public Transform groundCheck1;
        public Transform groundCheck2;
        public LayerMask groundObjects;

        // Private variables
        private float XVel = 0f; //max Velocity 5
        //private float XAce = 0;
        private float YVel = 0f;
        //private float YAce = 0;
        private int airJumpCount;
        private float moveDirection;
        private bool isJumping = false;
        private Rigidbody2D rb;

        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D and SpriteRenderer
            rb = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            airJumpCount = maxAirJumps; // jumpCount is set to whatever is in the Inspector View
        }

        // Update is called once per frame
        void Update()
        {
            //if (!TMP_Selection.GetTyping()) // The the cursor is not currently in a TMP object
            //{
                // Get player inputs
                processInputs();
            //}
        }

        // Better for handling physics, can be called multiple times per frame
        private void FixedUpdate() // Called First
        {
            // Check to see if the player is grounded
            // Should return when on the ground.
            // Could possibly call right after hit spacebar and before left the ground.
            // Fix the positioning of the "groundCheck" object if having issues

            // Use the player inputs
            if(isGrounded())
            {
                airJumpCount = maxAirJumps; // Reset the jump count
                moveOnGro();
            }
            else
            {
                moveInAir();
            }
        }

        private void processInputs()
        {
            // Process the keyboard inputs
            moveDirection = Input.GetAxis("Horizontal"); // Returns -1 to 1
            if (Input.GetButtonDown("Jump"))
                isJumping = true;
        }

        private void moveOnGro()
        {
            // Use the player inputs to move the character
            if (moveDirection > 0)
                XVel = 5f;
            else if(moveDirection < 0)
                XVel = -5f;
            else
                XVel = 0f;
            if (isJumping)
                YVel = jumpForce;
            else
                YVel = 0f;
            isJumping = false;
            rb.velocity = new Vector2(XVel, YVel);
        }

        private void moveInAir()
        {
            if (moveDirection > 0)
                XVel = 5f;
            else if (moveDirection < 0)
                XVel = -5f;
            if (isJumping && airJumpCount > 0) // Allows double jumping
            {
                YVel = jumpForce;
                --airJumpCount;
            } else
            {
                YVel -=1;
                if (YVel < -6)
                    YVel = -6;
            }
            isJumping = false;
            rb.velocity = new Vector2(XVel, YVel);
        }

        private bool isGrounded()
        {
            return Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundObjects);
        }
    }
}
