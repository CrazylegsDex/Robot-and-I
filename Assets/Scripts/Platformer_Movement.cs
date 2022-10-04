using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_Movement : MonoBehaviour
{
    // Public variables
    public int maxJumpCount;
    public float moveSpeed;
    public float jumpForce;
    public float checkRadius;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;

    // Private variables
    private int jumpCount;
    private float moveDirection;
    private bool faceRight = true;
    private bool isJumping = false;
    private bool isGrounded;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Awake is called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        // Get the elements in the current sprites Rigidbody2D and SpriteRenderer
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        jumpCount = maxJumpCount; // jump Count Starts at 1
    }

    // Update is called once per frame
    void Update() // Called Second
    {
        // Get player inputs
        processInputs();

        // Animate player direction
        Animate();
    }

    // Better for handling physics, can be called multiple times per frame
    private void FixedUpdate() // Called First
    {
        // Check to see if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects); // Should return when on the ground.
        // Could possibly call right after hit spacebar and before left the ground.
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        // Use the player inputs
        moveCharacter();
    }

    private void processInputs()
    {
        // Process the keyboard inputs
        moveDirection = Input.GetAxis("Horizontal"); // Returns -1 to 1
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void Animate()
    {
        // Change the direction of the player if needed
        if (moveDirection > 0 && !faceRight)
        {
            flipCharacter();
        }
        else if (moveDirection < 0 && faceRight)
        {
            flipCharacter();
        }
    }

    private void moveCharacter()
    {
        // Use the player inputs to move the character
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping && jumpCount > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            --jumpCount;
            isJumping = false;
        }
    }

    private void flipCharacter()
    {
        faceRight = !faceRight; // Invert the direction bool
        if (sr.flipX)
            sr.flipX = false;
        else
            sr.flipX = true;
    }
}
