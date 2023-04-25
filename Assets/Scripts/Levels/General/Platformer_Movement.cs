// This script allows control of Bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 03-08-2023

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
        static PlayerStates curBitState;

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
        public bool canGrab;        // if true bit can grab
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
        private Transform tf;       // used to keep track of Bit's Position for other objects to relate with
        private static Animator an;        // Animations for Bit
        private GameObject chec;    // Game Object for the
        private Transform checTf;   // transform for the 
        private Vector3 checkOffset;
        private static bool aniLock = false;   // if true no new Bit animation will start
        public static bool canAny = true; // when false Bit will be unable to do anything, used to stop controls when bit is dead
        private static bool canJump = true;// when false Bit will be unable to jump
        private static bool lLRight = true;// True when bit is initialized or last looked right, fals when last looked left.
        private bool isAct = false; // when true Bit is mid action and can't move or jump, ex. grabbing, but not holding
        private int idleActs;       // keeps track of Bit's remaining idle actions
        private float idleTime;     // keeps track of how much time until Bit will perform another idle action
        private float moveStrength; // GetAxis of Horizontal controls, ranges from -1 to 1
        private bool leftKey;       // is true if 'A' or '<-' are held down
        private bool righKey;       // is true if 'D' or '->' are held down
        private bool actKey;        // is true if 'E' is held down
        private bool firstFixedJump = false;    // keeps track of first fixed update where 'spacebar' is pressed
        private bool firstFixedAct = false;
        private int airJumpCount;   // keeps track of Bit's remaining air jumps
        private float airTime;      // keeps track of the time left in a jump
        private bool isJumping = false; // is true for the 1.5 block jump, that starts from the ground
        private bool isAirJumping = false;  // is true for the small air jumps, that starts in the air
        private bool firstFixedDeath = false;   // keeps track of first fixed update where Bit dies... dark

        static PlayerStates CurBitState
        {
            set
            {
                if (!aniLock)
                {
                    curBitState = value;
                    switch (curBitState)
                    {
                        case PlayerStates.LookC:
                            an.Play("LookC");
                            break;
                        case PlayerStates.LookL:
                            an.Play("LookL");
                            lLRight = false;
                            break;
                        case PlayerStates.LookR:
                            an.Play("LookR");
                            lLRight = true;
                            break;
                        case PlayerStates.WalkL:
                            an.Play("WalkL");
                            lLRight = false;
                            break;
                        case PlayerStates.WalkR:
                            an.Play("WalkR");
                            lLRight = true;
                            break;
                        case PlayerStates.PushL:
                            an.Play("PushL");
                            lLRight = false;
                            break;
                        case PlayerStates.PushR:
                            an.Play("PushR");
                            break;
                        case PlayerStates.ReturnL:
                            an.Play("ReturnL");
                            break;
                        case PlayerStates.ReturnR:
                            an.Play("ReturnR");
                            lLRight = true;
                            break;
                        case PlayerStates.JumpC:
                            an.Play("JumpC");
                            break;
                        case PlayerStates.JumpL:
                            an.Play("JumpL");
                            lLRight = false;
                            break;
                        case PlayerStates.JumpR:
                            an.Play("JumpR");
                            lLRight = true;
                            break;
                        case PlayerStates.AirAC:
                            an.Play("AirAC");
                            break;
                        case PlayerStates.AirAL:
                            an.Play("AirAL");
                            lLRight = false;
                            break;
                        case PlayerStates.AirAR:
                            an.Play("AirAR");
                            lLRight = true;
                            break;
                        case PlayerStates.AirDC:
                            an.Play("AirDC");
                            break;
                        case PlayerStates.AirDL:
                            an.Play("AirDL");
                            lLRight = false;
                            break;
                        case PlayerStates.AirDR:
                            an.Play("AirDR");
                            lLRight = true;
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
                            lLRight = false;
                            break;
                        case PlayerStates.LandedR:
                            an.Play("LandedR");
                            aniLock = true;
                            canJump = false;
                            lLRight = true;
                            break;
                        case PlayerStates.BonkCC:
                            an.Play("BonkCC");
                            aniLock = true;
                            break;
                        case PlayerStates.BonkLC:
                            an.Play("BonkLC");
                            aniLock = true;
                            lLRight = false;
                            break;
                        case PlayerStates.BonkRC:
                            an.Play("BonkRC");
                            aniLock = true;
                            lLRight = true;
                            break;
                        case PlayerStates.BonkLW:
                            an.Play("BonkLW");
                            aniLock = true;
                            lLRight = false;
                            break;
                        case PlayerStates.BonkRW:
                            an.Play("BonkRW");
                            aniLock = true;
                            lLRight = true;
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
        } // sets what actions will be performed for each animation change



        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D, Transform, and Animator
            rb = GetComponent<Rigidbody2D>();
            tf = GetComponent<Transform>();
            an = GetComponent<Animator>();
            chec = GameObject.FindWithTag("Checker");
            checTf = chec.GetComponent<Transform>();
            //tool = gameObject.transform.GetChild(1).gameObject;
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
            if (Input.GetKeyDown("e"))
                firstFixedAct = true;
        }

        // Better for handling physics, can be called multiple times per frame: current fixed frame count set to 1000 fps
        void FixedUpdate()
        {
            // Death animation test, press PageUp to have Bit play dead
            // unintentional sideffect it also turn off the terminal velocity function, so Bit becomes Brick
            if (firstFixedDeath)
            {
                CurBitState = PlayerStates.Off; // Triggers the "Off" animation once, before that triggers the looping "Fatal" animation
            }

            // contains controls for bits key player functions movement, animations, idling or The the cursor is not currently in a TMP object
            if (canAny && !TMP_Selection.GetTyping())
            {
                leftKey = Input.GetKey("a") || Input.GetKey("left"); // Checks for any left presses this frame
                righKey = Input.GetKey("right") || Input.GetKey("d"); // Checks for any Right presses this frame
                moveStrength = Input.GetAxis("Horizontal");

                if (lLRight)
                {
                    checkOffset = new Vector3(15f, 0f, 0f);
                    checTf.position = tf.position + checkOffset;
                }
                else
                {
                    checkOffset = new Vector3(-15f, 0f, 0f);
                    checTf.position = tf.position + checkOffset;
                }

                if (firstFixedAct && canGrab)
                {
                    Tool_Animations.ToolAction();
                    firstFixedAct = false;
                }
                else
                {
                    Tool_Animations.ToolUpdate(lLRight);
                }

                // checks if Bit is grounded or not, and decides what state they will go into
                if (IsGrounded())
                {
                    ActOnGro(); // Ground movement
                    GroAnimation(); // Ground animations
                }
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
            if (!isAct)
            {
                // horizontal movement
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
        }



        // Air Movement
        private void ActInAir()
        {
            if (!isAct)
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
                    CurBitState = PlayerStates.LandedL;
                }
                // if Bit is rising but close to the ground play the "JumpL" animation
                else if (rb.velocity.y > 0.1f)
                {
                    CurBitState = PlayerStates.JumpL;
                }
                // if wall is to the left of bit Push it, unimplemented Left wall bonk animation go near here
                else if (WallToLeft())
                {
                    CurBitState = PlayerStates.PushL;
                }
                // if nothing special happening just walk
                else
                {
                    CurBitState = PlayerStates.WalkL;
                }
            }
            else if (moveStrength > 0) // Right
            {
                idleTime = maxIdleTime; // if moving, not idle
                idleActs = maxIdleActs; // reset both values

                // if Bit is falling play the "LandedR" animation, temp disable jumping while animation plays
                if (rb.velocity.y < -0.1f)
                {
                    CurBitState = PlayerStates.LandedR;
                }
                // if Bit is rising but close to the ground play the "JumpR" animation
                else if (rb.velocity.y > 0.1f)
                {
                    CurBitState = PlayerStates.JumpR;
                }
                // if wall is to the right of Bit Push it, unimplemented Right wall bonk animation go near here
                else if (WallToRight())
                {
                    CurBitState = PlayerStates.PushR;
                }
                // if nothing special happening just walk
                else
                {
                    CurBitState = PlayerStates.WalkR;
                }
            }
            else // Centered/Idle
            {
                idleTime -= Time.deltaTime; // when not moving cound down the Idle

                // if Bit is falling play the "LandedC" animation, temp disable jumping while animation plays
                if (rb.velocity.y < -0.1f)
                {
                    CurBitState = PlayerStates.LandedC;
                }
                // if Bit is rising but close to the ground play the "JumpC" animation
                else if (rb.velocity.y > 0.1f)
                {
                    CurBitState = PlayerStates.JumpC;
                }
                // if bit ends a "LandedC" or "BonkCC" animation while on the ground return to "LookC"
                else if (rb.velocity.y >= -0.1f && rb.velocity.y <= 0.1f && (CheCurBitAni("LandedC") || CheCurBitAni("BonkCC")))
                {
                    CurBitState = PlayerStates.LookC;
                }
                // Idle Animation controller
                else if (CheCurBitAni("LookC"))
                {
                    // when idleTime is up
                    if (idleTime <= 0)
                    {
                        // final idle bit will "Doze" once before entering the "Sleep" animations
                        if (idleActs == 0)
                        {
                            CurBitState = PlayerStates.Doze;
                        }
                        // every even number idle animation befor bit sleeps, as long as bit is glitched
                        else if (idleActs % 2 == 0 && isGlitched)
                        {
                            CurBitState = PlayerStates.Glitch;
                        }
                        // every third idle animation before bit sleeps, no guarantee if bit is glitched
                        else if (idleActs % 3 == 0)
                        {
                            CurBitState = PlayerStates.Smile;
                        }
                        // all other idle animations bit blinks
                        else
                        {
                            CurBitState = PlayerStates.Blink;
                        }
                        idleActs--; // decrement idle actions
                        idleTime = maxIdleTime; // reset idle time
                    }
                }
                // if idle animation while LookL return bit to LookC
                else if (CheCurBitAni("LookL"))
                {
                    if (idleTime <= 0)
                    {
                        CurBitState = PlayerStates.ReturnL;
                        idleTime = maxIdleTime; // reset idle time
                    }
                }
                // if idle animation while LookR return bit to LookC
                else if (CheCurBitAni("LookR"))
                {
                    if (idleTime <= 0)
                    {
                        CurBitState = PlayerStates.ReturnR;
                        idleTime = maxIdleTime; // reset idle time
                    }
                }
                else
                {
                    // if any of these left facing states LookL
                    if (CheCurBitAni("LandedL") || CheCurBitAni("WalkL") || CheCurBitAni("PushL") || CheCurBitAni("BonkLW") || CheCurBitAni("BonkLC"))
                    {
                        CurBitState = PlayerStates.LookL;
                    }
                    // if any of these right facing states LookR
                    else if (CheCurBitAni("LandedR") || CheCurBitAni("WalkR") || CheCurBitAni("PushR") || CheCurBitAni("BonkRW") || CheCurBitAni("BonkRC"))
                    {
                        CurBitState = PlayerStates.LookR;
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
                    CurBitState = PlayerStates.AirDL;
                }
                // go up look up
                else if (rb.velocity.y > 0.1f)
                {
                    CurBitState = PlayerStates.AirAL;
                }
                // hit ceiling go ow.
                else if (HasBonked())
                {
                    CurBitState = PlayerStates.BonkLC;
                }
            }
            else if (moveStrength > 0) // Right
            {
                // go down look down
                if (rb.velocity.y < -0.1f)
                {
                    CurBitState = PlayerStates.AirDR;
                }
                // go up look up
                else if (rb.velocity.y > 0.1f)
                {
                    CurBitState = PlayerStates.AirAR;
                }
                // hit ceiling go oof.
                else if (HasBonked())
                {
                    CurBitState = PlayerStates.BonkRC;
                }
            }
            else // Centered/Idle
            {
                // go down look down
                if (rb.velocity.y < -0.1f)
                {
                    CurBitState = PlayerStates.AirDC;
                }
                // go up look up
                else if (rb.velocity.y > 0.1f)
                {
                    CurBitState = PlayerStates.AirAC;
                }
                // hit ceiling go *Goofy Scream*.
                else if (HasBonked())
                {
                    CurBitState = PlayerStates.BonkCC;
                }
            }
        }

        public static void PauseBit()
        {
            canAny = false;
            if (lLRight)
            {
                CurBitState = PlayerStates.LookR;
            }
            else
            {
                CurBitState = PlayerStates.LookL;
            }
            aniLock = true;
        }

        public static void PlayBit()
        {
            aniLock = false;
            canAny = true;
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
        private bool CheCurBitAni(string name)
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
            CurBitState = PlayerStates.LookC;
        }

        // Bit is Programmed to dream of electric sheep until interupted, by movement, jumping or death... again dark
        private void BitIsSleep()
        {
            CurBitState = PlayerStates.Dream;
        }

        // call to kill Bit; call this when bit colides with an obstacle
        private void KillBit()
        {
            firstFixedDeath = true;
        }

        // call to revive Bit
        private void HealBit()
        {
            firstFixedDeath = false;
        }

        // after shutting off unexpectedly, bit displays the last message "Error: A Fatal Excption has occrred at 0XJ985 \n *Press reset..."
        private void BitIsDead()
        {
            aniLock = false;
            CurBitState = PlayerStates.Fatal;
        }
    }
}
