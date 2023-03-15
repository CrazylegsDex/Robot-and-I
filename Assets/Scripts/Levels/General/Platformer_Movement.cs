// This script allows movement for the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 11-16-2022
using System;
using UnityEngine;

namespace PlayerControl
{
    public class Platformer_Movement : MonoBehaviour
    {
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
            if (!grab) {
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
    }
}
