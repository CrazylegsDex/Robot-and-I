// This script allows control of Bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 02-28-2023

using UnityEngine;

namespace PlayerControl
{
    public class Platformer_Movement : MonoBehaviour
    {
        public enum PlayerStates
        {
            LookL,
            WalkL,
            BonkLW,
            PushL,
            ReturnL,
            JumpL,
            AirAL,
            AirDL,
            BonkLC,
            LandedL,
            LookR,
            WalkR,
            BonkRW,
            PushR,
            ReturnR,
            JumpR,
            AirAR,
            AirDR,
            BonkRC,
            LandedR,
            LookC,
            JumpC,
            AirAC,
            AirDC,
            BonkCC,
            LandedC,
            Blink,
            Glitch,
            Smile,
            Doze,
            Dream,
            Off,
            Fatal
        } // Keeps track of all the animation states
        PlayerStates currentState;

        // Public variables - Inspector View modifiable
        //                          //Tested// Description
        public float maxXSpeedG;    // 04.0 // Max X-speed while grounded
        public float maxXSpeedA;    // 05.0 // Max X-speed while in air
        public float jumpForce;     // 04.5 // Force of jump
        public int maxAirJumps;     // 01.0 // max mid-air jumps
        public float maxAirTime;    // 0.30 // for sustained jump, the max time in second the jump can be held
        public float termVel;       // -6.0 // terminal velocity, this will prevent floaty jumps, in a way
        public int maxIdleActs;     // 08.0 // number of idle actions Bit will perform before sleeping
        public float maxIdleTime;   // 08.5 // how long bit will wait between performing idle actions
        public bool isGlitched;     // determines if bit will produce glitched animation or not
        public Transform ceiCh1;    // for ceiling check rectangle
        public Transform ceiCh2;    // ^ same
        public Transform wallChL1;  // for left wall check rectangle
        public Transform wallChL2;  // ^ same
        public Transform wallChR1;  // for right wall check rectangle
        public Transform wallChR2;  // ^ same
        public Transform grouCh1;   // for ground check rectangle
        public Transform grouCh2;   // ^ same
        public LayerMask plat;     // the layers/s that will be considered walls, ceilings, and floors (Platforms) 
        public LayerMask obj;   // the layers/s that will be considered walls and floors (objects like boxes)

        // Private variables
        private Rigidbody2D rb;     // Bit's rigidbody, will be used to change bits x and y velocity
        private Transform tf;       // used to update the position of the focalpoint to Bit's vicinity, (and therefore it helps keep the camera on Bit)
        private Animator an;         // Animations
        private bool aniLock = false;   // if true no new animation will start
        private bool canAny = true; // when false Bit will be unable to do anything, used to stop controls when bit is dead
        private bool canJump = true;// when false Bit will be unable to jump
        private bool lastLookRigh;
        private bool canAct;// when false 
        private int idleActs;       // keeps track of Bit's remaining idle actions
        private float idleTime;     // keeps track of how much time until Bit will perform another idle action
        private float moveStrength; // GetAxis of Horizontal controls, ranges from -1 to 1
        private bool leftKey;       // is true if 'A' or '<-' are held down
        private bool righKey;       // is true if 'D' or '->' are held down
        private bool actKey;        // is true if 'E' is held down
        private bool firstFixedJump = false;    // keeps track of first fixed update where 'spacebar' is pressed
        private int airJumpCount;   // keeps track of Bit's remaining air jumps
        private float airTime;      // keeps track of the time left in a jump
        private bool isJumping = false; // is true for the 1.5 block jump, that starts from the ground
        private bool isAirJumping = false;  // is true for the small air jumps, that starts in the air
        private bool firstFixedDeath = false;   // keeps track of first fixed update where Bit dies... dark

        PlayerStates CurrentState
        {
            set
            {
                if (!aniLock)
                {
                    currentState = value;
                    switch (currentState)
                    {
                        case PlayerStates.LookC:
                            an.Play("LookC");
                            break;
                        case PlayerStates.LookL:
                            an.Play("LookL");
                            break;
                        case PlayerStates.LookR:
                            an.Play("LookR");
                            break;
                        case PlayerStates.WalkL:
                            an.Play("WalkL");
                            break;
                        case PlayerStates.WalkR:
                            an.Play("WalkR");
                            break;
                        case PlayerStates.PushL:
                            an.Play("PushL");
                            break;
                        case PlayerStates.PushR:
                            an.Play("PushR");
                            break;
                        case PlayerStates.ReturnL:
                            an.Play("ReturnL");
                            break;
                        case PlayerStates.ReturnR:
                            an.Play("ReturnR");
                            break;
                        case PlayerStates.JumpC:
                            an.Play("JumpC");
                            break;
                        case PlayerStates.JumpL:
                            an.Play("JumpL");
                            break;
                        case PlayerStates.JumpR:
                            an.Play("JumpR");
                            break;
                        case PlayerStates.AirAC:
                            an.Play("AirAC");
                            break;
                        case PlayerStates.AirAL:
                            an.Play("AirAL");
                            break;
                        case PlayerStates.AirAR:
                            an.Play("AirAR");
                            break;
                        case PlayerStates.AirDC:
                            an.Play("AirDC");
                            break;
                        case PlayerStates.AirDL:
                            an.Play("AirDL");
                            break;
                        case PlayerStates.AirDR:
                            an.Play("AirDR");
                            break;
                        case PlayerStates.LandedC:
                            an.Play("LandedC");
                            aniLock = true;
                            canJump = false;
                            break;
                        case PlayerStates.LandedL:
                            an.Play("LandedL");
                            aniLock = true;
                            canJump = false;
                            break;
                        case PlayerStates.LandedR:
                            an.Play("LandedR");
                            aniLock = true;
                            canJump = false;
                            break;
                        case PlayerStates.BonkCC:
                            an.Play("BonkCC");
                            aniLock = true;
                            break;
                        case PlayerStates.BonkLC:
                            an.Play("BonkLC");
                            aniLock = true;
                            break;
                        case PlayerStates.BonkRC:
                            an.Play("BonkRC");
                            aniLock = true;
                            break;
                        case PlayerStates.BonkLW:
                            an.Play("BonkLW");
                            aniLock = true;
                            break;
                        case PlayerStates.BonkRW:
                            an.Play("BonkRW");
                            aniLock = true;
                            break;
                        case PlayerStates.Blink:
                            an.Play("Blink");
                            break;
                        case PlayerStates.Smile:
                            an.Play("Smile");
                            break;
                        case PlayerStates.Glitch:
                            an.Play("Glitch");
                            break;
                        case PlayerStates.Doze:
                            an.Play("Doze");
                            break;
                        case PlayerStates.Dream:
                            an.Play("Dream");
                            break;
                        case PlayerStates.Off:
                            an.Play("Off");
                            aniLock = true;
                            canAny = false;
                            break;
                        case PlayerStates.Fatal:
                            an.Play("Fatal");
                            aniLock = true;
                            break;
                    }
                }
            }
        } // sets what actions will be performed for each state change

        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D, Transform, and Animator
            rb = GetComponent<Rigidbody2D>();
            tf = GetComponent<Transform>();
            an = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            airJumpCount = maxAirJumps; // airJumpCount is set to whatever is in the inspector view
            airTime = maxAirTime;       // airTime is set to whatever is in the inspector view
            idleActs = maxIdleActs; // sets idleActs of focal point to whatever is in the inspector view
            idleTime = maxIdleTime; // sets idleTime of focal point to whatever is in the inspector view
            //focalPoint.position = tf.position; // sets position of focal point to that of bit
        }

        // Update is called once per frame: current tested frame counds come to around 800-900 fps, but 200-300 fps are also possible
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
                firstFixedJump = true;
        }

        // Better for handling physics, can be called multiple times per frame: current fixed frame count set to 1000 fps
        void FixedUpdate()
        {
            // Death animation test, press PageUp to have Bit play dead
            // unintentional sideffect it also turn off the terminal velocity function, so Bit becomes Brick
            if (firstFixedDeath)
            {
                CurrentState = PlayerStates.Off; // Triggers the "Off" animation once, before that triggers the looping "Fatal" animation
            }

            // contains controls for bits key player functions movement, animations, idling or The the cursor is not currently in a TMP object
            if (canAny && !TMP_Selection.GetTyping())
            {
                leftKey = Input.GetKey("a") || Input.GetKey("left"); // Checks for any left presses this frame
                righKey = Input.GetKey("right") || Input.GetKey("d"); // Checks for any Right presses this frame
                moveStrength = Input.GetAxis("Horizontal");

                // checks if Bit is grounded or not, and decides what state they will go into
                if (IsGrounded())
                {
                    ActOnGro(); // Ground movement
                    GroAnimation(); // Ground animations
                }
                // checks if bit is in water
                //else if(inWater()) // In case we need water controls 
                //{
                //    moveInWater(); // not made
                //    watAnimation(); // not made/animated
                //}
                else
                {
                    ActInAir(); // Air Movement
                    AirAnimation(); // Air animations
                }
            }
            // if can't move (due to death) this is the part where bit's movement is disabled
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y); // movement disabled, except gravity
            }
            firstFixedJump = false;
        }


        //Grounded Movement
        private void ActOnGro()
        {
            airJumpCount = maxAirJumps; // reset air jumps because Bit is Grounded

            if (leftKey)
            {
                if (righKey) // Left and Right Press
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y); // no movement
                }
                else //Left only press
                {
                    if (WallToLeft()) // Moving Right or Obstacle to Left
                    {
                        rb.velocity = new Vector2(-1f, rb.velocity.y); // small x movement so bit can touch left walls
                    }
                    else // Moving Left or Stopped
                    {
                        rb.velocity = new Vector2(-maxXSpeedG, rb.velocity.y); // speed is immeditly set to ground max, to the left
                    }
                }
            }
            else
            {
                if (righKey) // right only press
                {
                    if (WallToRight()) // Moving Left or Obstacle to Right
                    {
                        rb.velocity = new Vector2(1f, rb.velocity.y); // small x movement so bit can touch right walls
                    }
                    else // Moving Right or Stopped
                    {
                        rb.velocity = new Vector2(maxXSpeedG, rb.velocity.y); // speed is immeditly set to ground max, to the Right
                    }
                }
                else // no press
                {
                    if (rb.velocity.x < 0f) // Moving Left
                    {
                        if (WallToLeft())
                        {
                            rb.velocity = new Vector2(-1f, rb.velocity.y); // small x movement so bit can touch left walls
                        }
                        else
                        {
                            rb.velocity = new Vector2(maxXSpeedG * moveStrength, rb.velocity.y); //allows bit to slow to a stop rather than do so abbruptly
                        }
                    }
                    else if (rb.velocity.x > 0f) // Moving Right
                    {
                        if (WallToRight())
                        {
                            rb.velocity = new Vector2(1f, rb.velocity.y); // small x movement so bit can touch right walls
                        }
                        else
                        {
                            rb.velocity = new Vector2(maxXSpeedG * moveStrength, rb.velocity.y); //allows bit to slow to a stop rather than do so abbruptly
                        }
                    }
                }
            }

            // check if jumping already, if not jumping, ignore
            if (isJumping)
            {
                canJump = false;
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
            else
            {
                canJump = true;
            }

            // if jump button pressed, setup for grounded jump
            if (firstFixedJump && canJump)
            {
                isJumping = true;
                airTime = maxAirTime;
                firstFixedJump = false;
            }
        }



        // Air Movement
        private void ActInAir()
        {
            // sets X velocity to be -maxXSpeedA, 0, or maxXSpeedA; for bunny hopping
            if (leftKey)
            {
                if (righKey) // Left and Right Press
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y); // no move
                }
                else //Left only press
                {
                    if (WallToLeft()) // Moving Right or Obstacle to Left
                    {
                        rb.velocity = new Vector2(-1f, rb.velocity.y); // small x movement so bit can touch left walls
                    }
                    else // Moving Left or Stopped
                    {
                        rb.velocity = new Vector2(-maxXSpeedA, rb.velocity.y); // speed is immeditly set to air max, to the left
                    }
                }
            }
            else
            {
                if (righKey) // right only press
                {
                    if (WallToRight()) // Moving Left or Obstacle to Right
                    {
                        rb.velocity = new Vector2(1f, rb.velocity.y); // small x movement so bit can touch right walls
                    }
                    else // Moving Right or Stopped
                    {
                        rb.velocity = new Vector2(maxXSpeedA, rb.velocity.y); // speed is immeditly set to air max, to the Right
                    }
                }
                else // no press
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y); // unlike on the ground it would be difficult to place jump if the player kept sliding in air
                }
            }

            // check if jumping from ground already
            if (isJumping)
            {
                canJump = false;
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
            else
            {
                canJump = true;
            }

            // if jump button pressed, setup
            if (firstFixedJump && canJump && (airJumpCount > 0))
            {
                isAirJumping = true;
                airJumpCount--;
                firstFixedJump = false;
            }

            // maintains terminal velocity, this might be the only part that should be moved to FixedUpdate
            if (rb.velocity.y < termVel)
            {
                rb.velocity = new Vector2(rb.velocity.x, termVel);
            }

            // if Bit hits ceiling, end jump and set vertical velocity to zero
            if (HasBonked())
            {
                isJumping = false;
                isAirJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }


        // Grounded Animations
        private void GroAnimation()
        {
            if (moveStrength < 0) // Left
            {
                idleTime = maxIdleTime; // if moving, not idle
                idleActs = maxIdleActs; // reset both values

                // if Bit is falling play the "LandedL" animation, temp disable jumping while animation plays
                if (rb.velocity.y < -0.1f)
                {
                    CurrentState = PlayerStates.LandedL;
                }
                // if Bit is rising but close to the ground play the "JumpL" animation
                else if (rb.velocity.y > 0.1f)
                {
                    CurrentState = PlayerStates.JumpL;
                }
                // if wall is to the left of bit Push it, unimplemented Left wall bonk animation go near here
                else if (WallToLeft())
                {
                    CurrentState = PlayerStates.PushL;
                }
                // if nothing special happening just walk
                else
                {
                    CurrentState = PlayerStates.WalkL;
                }
            }
            else if (moveStrength > 0) // Right
            {
                idleTime = maxIdleTime; // if moving, not idle
                idleActs = maxIdleActs; // reset both values

                // if Bit is falling play the "LandedR" animation, temp disable jumping while animation plays
                if (rb.velocity.y < -0.1f)
                {
                    CurrentState = PlayerStates.LandedR;
                }
                // if Bit is rising but close to the ground play the "JumpR" animation
                else if (rb.velocity.y > 0.1f)
                {
                    CurrentState = PlayerStates.JumpR;
                }
                // if wall is to the right of Bit Push it, unimplemented Right wall bonk animation go near here
                else if (WallToRight())
                {
                    CurrentState = PlayerStates.PushR;
                }
                // if nothing special happening just walk
                else
                {
                    CurrentState = PlayerStates.WalkR;
                }
            }
            else // Centered/Idle
            {
                idleTime -= Time.deltaTime; // when not moving cound down the Idle

                // if Bit is falling play the "LandedC" animation, temp disable jumping while animation plays
                if (rb.velocity.y < -0.1f)
                {
                    CurrentState = PlayerStates.LandedC;
                }
                // if Bit is rising but close to the ground play the "JumpC" animation
                else if (rb.velocity.y > 0.1f)
                {
                    CurrentState = PlayerStates.JumpC;
                }
                // if bit ends a "LandedC" or "BonkCC" animation while on the ground return to "LookC"
                else if (rb.velocity.y >= -0.1f && rb.velocity.y <= 0.1f && (CheCurAni("LandedC") || CheCurAni("BonkCC")))
                {
                    CurrentState = PlayerStates.LookC;
                }
                // Idle Animation controller
                else if (CheCurAni("LookC"))
                {
                    // when idleTime is up
                    if (idleTime <= 0)
                    {
                        // final idle bit will "Doze" once before entering the "Sleep" animations
                        if (idleActs == 0)
                        {
                            CurrentState = PlayerStates.Doze;
                        }
                        // every even number idle animation befor bit sleeps, as long as bit is glitched
                        else if (idleActs % 2 == 0 && isGlitched)
                        {
                            CurrentState = PlayerStates.Glitch;
                        }
                        // every third idle animation before bit sleeps, no guarantee if bit is glitched
                        else if (idleActs % 3 == 0)
                        {
                            CurrentState = PlayerStates.Smile;
                        }
                        // all other idle animations bit blinks
                        else
                        {
                            CurrentState = PlayerStates.Blink;
                        }
                        idleActs--; // decrement idle actions
                        idleTime = maxIdleTime; // reset idle time
                    }
                }
                // if idle animation while LookL return bit to LookC
                else if (CheCurAni("LookL"))
                {
                    if (idleTime <= 0)
                    {
                        CurrentState = PlayerStates.ReturnL;
                        idleTime = maxIdleTime; // reset idle time
                    }
                }
                // if idle animation while LookR return bit to LookC
                else if (CheCurAni("LookR"))
                {
                    if (idleTime <= 0)
                    {
                        CurrentState = PlayerStates.ReturnR;
                        idleTime = maxIdleTime; // reset idle time
                    }
                }
                else
                {
                    // if any of these left facing states LookL
                    if (CheCurAni("LandedL") || CheCurAni("WalkL") || CheCurAni("PushL") || CheCurAni("BonkLW") || CheCurAni("BonkCL"))
                    {
                        CurrentState = PlayerStates.LookL;
                    }
                    // if any of these right facing states LookR
                    else if (CheCurAni("LandedR") || CheCurAni("WalkR") || CheCurAni("PushR") || CheCurAni("BonkRW") || CheCurAni("BonkCR"))
                    {
                        CurrentState = PlayerStates.LookR;
                    }
                }
            }
        }

        // Air Animations
        private void AirAnimation()
        {
            idleTime = maxIdleTime; // no idleing in air, they don't happen unless chimneys are jumpped over
            idleActs = maxIdleActs; // should keep things reset for inevitable ground contact
            if (moveStrength < 0) // Left
            {
                // go down look down, feel these comments of a fading man (it's 4:31 A.M.)
                if (rb.velocity.y < -0.1f)
                {
                    CurrentState = PlayerStates.AirDL;
                }
                // go up look up
                else if (rb.velocity.y > 0.1f)
                {
                    CurrentState = PlayerStates.AirAL;
                }
                // hit ceiling go ow.
                else if (HasBonked())
                {
                    CurrentState = PlayerStates.BonkLC;
                }
            }
            else if (moveStrength > 0) // Right
            {
                // go down look down
                if (rb.velocity.y < -0.1f)
                {
                    CurrentState = PlayerStates.AirDR;
                }
                // go up look up
                else if (rb.velocity.y > 0.1f)
                {
                    CurrentState = PlayerStates.AirAR;
                }
                // hit ceiling go oof.
                else if (HasBonked())
                {
                    CurrentState = PlayerStates.BonkRC;
                }
            }
            else // Centered/Idle
            {
                // go down look down
                if (rb.velocity.y < -0.1f)
                {
                    CurrentState = PlayerStates.AirDC;
                }
                // go up look up
                else if (rb.velocity.y > 0.1f)
                {
                    CurrentState = PlayerStates.AirAC;
                }
                // hit ceiling go *Goofy Scream*.
                else if (HasBonked())
                {
                    CurrentState = PlayerStates.BonkCC;
                }
            }
        }

        // returns true if Bit hurts their head
        private bool HasBonked()
        {
            return Physics2D.OverlapArea(ceiCh1.position, ceiCh2.position, plat);
        }

        // returns true if wall is to the left
        private bool WallToLeft()
        {
            return Physics2D.OverlapArea(wallChL1.position, wallChL2.position, plat) || Physics2D.OverlapArea(wallChL1.position, wallChL2.position, obj);
        }

        // returns true if wall is to the Right
        private bool WallToRight()
        {
            return Physics2D.OverlapArea(wallChR1.position, wallChR2.position, plat) || Physics2D.OverlapArea(wallChR1.position, wallChR2.position, obj);
        }

        // returns true if Bit is on the ground
        private bool IsGrounded()
        {
            return Physics2D.OverlapArea(grouCh1.position, grouCh2.position, plat) || Physics2D.OverlapArea(grouCh1.position, grouCh2.position, obj);
        }

        // give String of animation name, returns true if it is the current animation
        private bool CheCurAni(string name)
        {
            return an.GetCurrentAnimatorStateInfo(0).IsName(name);
        }

        // unlocks animation locks, so animations can update again, called at the end of some animations, in animation events.
        private void EndAni()
        {
            aniLock = false;
        }

        //special animation unlock for the "Landing" animations, also reallows(Is that word?) Jumping
        private void EndLandAni()
        {
            aniLock = false;
            canJump = true;
        }

        // at the end of an idle animation so just stand there {Menacingly!}, well until the next one plays
        private void EndIdleAni()
        {
            CurrentState = PlayerStates.LookC;
        }

        // Bit is Programmed to dream of electric sheep until interupted, by movement, jumping or death... again dark
        private void BitIsSleep()
        {
            CurrentState = PlayerStates.Dream;
        }

        // after shutting off unexpectedly, bit displays the last message "Error: A Fatal Excption has occrred at 0XJ985 \n *Press reset..."
        private void BitIsDead()
        {
            aniLock = false;
            CurrentState = PlayerStates.Fatal;
        }
        /*
        // Public variables - Inspector View modifiable
        public int maxJumpCount;
        public float moveSpeed;
        public float jumpForce;
        public float checkRadius;
        public Transform ceilingCheck;
        public Transform groundCheck;
        public LayerMask groundObjects;
        public Transform buttonCheck;
        public LayerMask buttonObjects;
        public Transform boxCheck;
        public LayerMask boxObjects;
        public GameObject[] boxTests;
        public GameObject boxChosen;


        // Private variables
        private int jumpCount;
        private float moveDirection;
        private bool isJumping = false;
        private bool grab = false;
        private bool isGrounded;
        private bool isOnButton;
        private bool nearBox;
        private Rigidbody2D rb;
        private bool safe = true;

        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D and SpriteRenderer
            rb = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            jumpCount = maxJumpCount; // jumpCount is set to whatever is in the Inspector View
        }

        // Update is called once per frame
        void Update() // Called Second
        {
            if (!TMP_Selection.GetTyping()) // The the cursor is not currently in a TMP object
            {
                // Get player inputs
                processInputs();
            }
        }

        // Better for handling physics, can be called multiple times per frame
        private void FixedUpdate() // Called First
        {
            // Check to see if the player is grounded
            // Should return when on the ground.
            // Could possibly call right after hit spacebar and before left the ground.
            // Fix the positioning of the "groundCheck" object if having issues
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
            isOnButton = Physics2D.OverlapCircle(buttonCheck.position, checkRadius, buttonObjects);
            if (isGrounded || isOnButton)
                jumpCount = maxJumpCount; // Reset the jump count

            // Use the player inputs
            moveCharacter();
        }

        private void processInputs()
        {
            // Process the keyboard inputs
            moveDirection = Input.GetAxis("Horizontal"); // Returns -1 to 1
            if (Input.GetButtonDown("Jump") && jumpCount > 0)
                isJumping = true;
            if (!grab)
            {
                nearBox = Physics2D.OverlapCircle(boxCheck.position, checkRadius, boxObjects);
            }
            //checkRadius = .2
            try//incase there are no boxes in the level
            {
                boxTests = GameObject.FindGameObjectsWithTag("Box");
            }
            catch (Exception)
            {
                safe = false;
            }
            if (safe)
            {
                if (nearBox && Input.GetKeyDown(KeyCode.E) && !grab)
                {


                    foreach (GameObject go in boxTests)
                    {

                        float boxPos = Vector3.Distance(transform.position, go.transform.position);

                        //Debug.Log(boxPos / 100);
                        if (boxPos / 100 >= .19 && boxPos / 100 <= 0.25)
                        {

                            go.transform.parent = boxCheck.parent;
                            go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            ///added below
                            go.transform.position = new Vector3(boxCheck.transform.position.x + 6, boxCheck.transform.position.y, boxCheck.transform.position.z);
                            //added above line to fix physics issue
                            go.GetComponent<Rigidbody2D>().isKinematic = true;
                            boxChosen = go;
                        }
                        grab = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    //Debug.Log("E key was pressed.");

                    boxChosen.transform.parent = null;
                    boxChosen.GetComponent<Rigidbody2D>().isKinematic = false;
                    grab = false;
                }
            }
        }

        private void moveCharacter()
        {
            // Use the player inputs to move the character
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            if (isJumping && jumpCount > 0) // Allows double jumping
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                --jumpCount;
                isJumping = false;
            }
        }
        */
    }
}
