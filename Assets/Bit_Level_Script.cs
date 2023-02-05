// This script allows control of bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 02-05-2023
//
// goals for 02-05-2023: work on more animations, add some animations, fine tune numbers
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelBit
{
    public class Bit_Level_Script : MonoBehaviour
    {
        // Public variables - Inspector View modifiable
        //                              //Tested// Description
        public float maxXSpeedG;        // 04.0 // Max X-speed while grounded
        public float maxXSpeedA;        // 06.0 // Max X-speed while in air
        public float jumpForce;         // 05.5 // Force of jump
        public int maxAirJumps;         // 01.0 // max mid-air jumps
        public float maxAirTime;        // 0.35 // for sustained jump, the max time in second the jump can be held
        public float termVel;           // -6.0 // terminal velocity, this will prevent floaty jumps, in a way
        public Transform ceilingCheck1; // for ceiling check rectangle
        public Transform ceilingCheck2; // 
        public LayerMask ceilingObjects;// had trouble with ceiling layer, just use ground layer instead
        public Transform groundCheck1;  // for ground check rectangle
        public Transform groundCheck2;  //
        public LayerMask groundObjects; // the layers/s that will be considered ground

        // Private variables
        private Rigidbody2D rb;
        private int airJumpCount; // air jumps remaining
        private float airTime;    // sustained jump time remaining
        private float moveDirection;
        private bool isJumping = false;

        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D and SpriteRenderer
            rb = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            airJumpCount = maxAirJumps; // airJumpCount is set to whatever is in the inspector view
            airTime = maxAirTime;       // airTime is set to whatever is in the inspector view
        }

        // Update is called once per frame
        void Update()
        {
            // temporarily disabled while testing
            //if (!TMP_Selection.GetTyping()) // The the cursor is not currently in a TMP object
            //{
            if (isGrounded()) // checks if Bit is grounded or not, and decides what state they will go into
            {
                airJumpCount = maxAirJumps; // reset air jumps because Bit is Grounded
                moveOnGro();
            }
            //else if(inWater())
            //{
            //    moveInWater();
            //}
            else
            {
                moveInAir();
            }
            //}
        }

        private void moveOnGro()
        {
            // sets X velocity to be -maxXSpeedG, 0, or maxXSpeedG; for snappy movement
            rb.velocity = new Vector2(maxXSpeedG * Input.GetAxisRaw("Horizontal"), rb.velocity.y);

            // if jump button pressed, setup
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                airTime = maxAirTime;
            }

            //if jump button is held...
            if (Input.GetButton("Jump"))
            {
                // check if jumping already, if not jumping, ignore
                if (isJumping)
                {
                    // check if any airtime remains
                    if (airTime > 0)
                    {
                        airTime -= Time.deltaTime; // remove air time
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // set vertical velocity to jumpForce
                    }
                    // no more airtime, end jump
                    else
                    {
                        isJumping = false;
                    }
                }
            }

            // if jump button released can end jump early
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        }

        private void moveInAir()
        {
            // sets X velocity to be -maxXSpeedA, 0, or maxXSpeedA; for bunny hopping
            rb.velocity = new Vector2(maxXSpeedA * Input.GetAxisRaw("Horizontal"), rb.velocity.y);

            // if jump button pressed...
            if (Input.GetButtonDown("Jump"))
            {
                // check for remaining air jumps, if not ignore
                if (airJumpCount > 0)
                {
                    airJumpCount--;
                    isJumping = true;
                    airTime = 0.2f * maxAirTime;
                }
            }

            //if jump button is held...
            if (Input.GetButton("Jump"))
            {
                // check if jumping already, if not jumping, ignore
                if (isJumping)
                {
                    // check if any airtime remains
                    if (airTime > 0)
                    {
                        airTime -= Time.deltaTime; // remove air time
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // set vertical velocity to jumpForce
                    }
                    // no more airtime, end jump
                    else
                    {
                        isJumping = false;
                    }

                }
            }

            // if jump button released can end jump early
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            // maintains terminal velocity, this might be the only part that should be moved to FixedUpdate
            if (rb.velocity.y < termVel)
            {
                rb.velocity = new Vector2(rb.velocity.x, termVel);
            }

            // if Bit hits ceiling, end jump and set vertical velocity to zero
            if (hasBonked())
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }

        // returns true if Bit is on the ground
        private bool isGrounded()
        {
            return Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundObjects);
        }

        // returns true if Bit hurts their head
        private bool hasBonked()
        {
            return Physics2D.OverlapArea(ceilingCheck1.position, ceilingCheck2.position, ceilingObjects);
        }
    }
}
