// This script allows control of bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 02-14-2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelBit
{
    public class Bit_Level_Script : MonoBehaviour
    {
        // Public variables - Inspector View modifiable
        //                          //Tested// Description
        public float maxXSpeedG;    // 04.0 // Max X-speed while grounded
        public float maxXSpeedA;    // 06.0 // Max X-speed while in air
        public float jumpForce;     // 04.5 // Force of jump
        public int maxAirJumps;     // 01.0 // max mid-air jumps
        public float maxAirTime;    // 0.30 // for sustained jump, the max time in second the jump can be held
        public float termVel;       // -6.0 // terminal velocity, this will prevent floaty jumps, in a way
        public float maxIdleTime;   // 02.5 // 
        public Animator an;         // Animations
        public Transform ceiCh1;    // for ceiling check rectangle
        public Transform ceiCh2;    // 
        public Transform wallChL1;  // for left wall check rectangle
        public Transform wallChL2;  //
        public Transform wallChR1;  // for right wall check rectangle
        public Transform wallChR2;  //
        public Transform grouCh1;   // for ground check rectangle
        public Transform grouCh2;   //
        public LayerMask ceiObj;    // the layers/s that will be considered walls (had trouble with ceiling layer, just use ground layer for now)
        public LayerMask wallObj;   // the layers/s that will be considered walls
        public LayerMask grouObj;   // the layers/s that will be considered ground
        public Transform focalPoint;//

        // Private variables
        private Rigidbody2D rb;
        private Transform tf;
        private Vector3 tFUpdate;
        private int aniDirection;
        private float idleTime;
        private bool isIdle = false;
        private float moveDirection;
        private float moveStrength;
        private bool jumpPress;
        private bool firstFixed = false;
        private int airJumpCount;   // air jumps remaining
        private float airTime;      // sustained jump time remaining
        private bool isJumping = false;
        private bool isAirJumping = false;
        private bool camFollow = true;
        private Vector3 camXShift;

        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D and SpriteRenderer
            rb = GetComponent<Rigidbody2D>();
            tf = GetComponent<Transform>();
            an = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            airJumpCount = maxAirJumps; // airJumpCount is set to whatever is in the inspector view
            airTime = maxAirTime;       // airTime is set to whatever is in the inspector view
            focalPoint.position = tf.position; // sets position of focal point to that of bit
            idleTime = maxIdleTime;
        }

        // Update is called once per frame
        void Update()
        {
            jumpPress = Input.GetButtonDown("Jump");
            firstFixed = true;
        }

        // Better for handling physics, can be called multiple times per frame
        void FixedUpdate()
        {
            moveDirection = Input.GetAxisRaw("Horizontal");
            moveStrength = Input.GetAxis("Horizontal");
            if (camFollow)
            {
                camXShift = new Vector3(moveStrength * 3.5f, 0f - 0.11f, 0f);
                focalPoint.position = tf.position; // + camXShift; // add back for camera to follow fixed point rather than Bit
            }

            if (moveDirection == 0f && !jumpPress)
            {
                isIdle = true;
            }
            else
            {
                isIdle = false;
            }
            an.SetBool("Idle", isIdle);

            an.SetFloat("Direction", moveDirection);

            // temporarily disabled while testing
            //if (!TMP_Selection.GetTyping()) // The the cursor is not currently in a TMP object
            //{
            if (isGrounded()) // checks if Bit is grounded or not, and decides what state they will go into
            {
                moveOnGro();
                if (isIdle)
                {
                    idleTime -= Time.deltaTime;
                    if (idleTime <= 0f)
                    {
                        if (an.GetCurrentAnimatorStateInfo(0).IsName("Level_Walk_L"))
                        {

                        }
                        else if (an.GetCurrentAnimatorStateInfo(0).IsName("Level_Walk_R"))
                        {

                        }
                        else
                        {

                        }
                    }
                }
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
            airJumpCount = maxAirJumps; // reset air jumps because Bit is Grounded
            if (moveDirection < 0f) // Left Press
            {
                if (wallToLeft()) // Moving Right or Obstacle to Left
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
                else // Moving Left or Stopped
                {
                    rb.velocity = new Vector2(-maxXSpeedG, rb.velocity.y);
                }
            }
            else if (moveDirection > 0f) // Right Press
            {
                if (wallToRight()) // Moving Left or Obstacle to Right
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
                else // Moving Right or Stopped
                {
                    rb.velocity = new Vector2(maxXSpeedG, rb.velocity.y);
                }
            }
            else // No Press
            {
                if (rb.velocity.x < 0f) // Moving Left
                {
                    if (wallToLeft())
                    {
                        rb.velocity = new Vector2(0f, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(maxXSpeedG * moveStrength, rb.velocity.y);
                    }
                }
                else if (rb.velocity.x > 0f) // Moving Right
                {
                    if (wallToRight())
                    {
                        rb.velocity = new Vector2(0f, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(maxXSpeedG * moveStrength, rb.velocity.y);
                    }
                }
                //Not Moving No Change
            }

            // if jump button pressed, setup
            if (jumpPress && firstFixed)
            {
                isJumping = true;
                airTime = maxAirTime;
                firstFixed = false;
            }

            // check if jumping already, if not jumping, ignore
            if (isJumping)
            {
                // check if any airtime remains
                if (airTime > 0f)
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

        private void moveInAir()
        {
            // sets X velocity to be -maxXSpeedA, 0, or maxXSpeedA; for bunny hopping
            if (moveDirection < 0f) // Left Press
            {
                if (wallToLeft() || rb.velocity.x > 0) // Moving Right or Obstacle to Left
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
                else // Moving Left or Stopped
                {
                    rb.velocity = new Vector2(-maxXSpeedA, rb.velocity.y);
                }
            }
            else if (moveDirection > 0f) // Right Press
            {
                if (wallToRight() || rb.velocity.x < 0) // Moving Left or Obstacle to Right
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
                else // Moving Right or Stopped
                {
                    rb.velocity = new Vector2(maxXSpeedA, rb.velocity.y);
                }
            }
            else // No Press
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            // if jump button pressed...
            if (jumpPress && firstFixed && !isJumping)
            {
                // check for remaining air jumps, if not ignore
                if (airJumpCount > 0f)
                {
                    airJumpCount--;
                    isAirJumping = true;
                }
            }
            
            // check if jumping from ground already
            if (isJumping)
            {
                // check if any airtime remains
                if (airTime > 0f)
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
            // check if preforming an air jump 
            else if (isAirJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.2f); // set vertical velocity to jumpForce
                isAirJumping = false;
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

        // returns true if Bit hurts their head
        private bool hasBonked()
        {
            return Physics2D.OverlapArea(ceiCh1.position, ceiCh2.position, ceiObj);
        }

        // returns true if wall is to the left
        private bool wallToLeft()
        {
            return Physics2D.OverlapArea(wallChL1.position, wallChL2.position, wallObj);
        }

        // returns true if wall is to the Right
        private bool wallToRight()
        {
            return Physics2D.OverlapArea(wallChR1.position, wallChR2.position, wallObj);
        }
        
        // returns true if Bit is on the ground
        private bool isGrounded()
        {
            return Physics2D.OverlapArea(grouCh1.position, grouCh2.position, grouObj);
        }

    }
}