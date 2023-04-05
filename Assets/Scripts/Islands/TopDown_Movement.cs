// This script allows movement for the player in the topdown view
//
// Author: Robot and I Team
// Last modification date: 1-30-2023

using UnityEngine;
using GameMechanics; // Pulls in the interface from GameMechanics

namespace PlayerControl
{
    public class TopDown_Movement : MonoBehaviour, DataPersistenceInterface
    {
        // Public variables
		public string mapName;
        public float moveSpeed; // Inspector view modifiable
        public Rigidbody2D rb; // Associated sprite object
        public Animator an; // Animations
		public bool AllowMovement = true;

        // Hidden Public variables
        [HideInInspector] public static Vector3 position = new Vector3();

        // Private variables
        private Vector2 moveDirection;

	    // Start is called when the object is initialized
        void Start()
        {
            // Initialize our animation component and set Bit's inital direction to front
            an = gameObject.GetComponent<Animator>();
            an.SetFloat("YInput", -1);
        }

        // Update is called once per frame, therefore based on frame rate
        private void Update()
        {
            // Use this function for processing inputs from the user
            ProcessInputs();
        }

        // FixedUpdate is called on a consistent basis.
        // Results in less jaggy movement if used in physics calculations
        private void FixedUpdate()
        {
            // Use this function for processing physics calculations
            Move();
        }

        // Check if there is a collision. Play sound if so
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Play the sound attached to this script
            Audio_Manager test = GameObject.Find("ScriptController").GetComponent<Audio_Manager>();
            test.Play("Collision");
        }

        // LoadData method from the DataPersistenceInterface
        public void LoadData(GameData data)
        {
            // Update Bit's location from the gameData
            transform.position = data.playerPosition;
        }

        // SaveData method from the DataPersistenceInterface
        public void SaveData(GameData data)
        {
            // Save Bit's location to the gameData
            // Bit's location can either be his current location, or
            // a new location based on the Ship's teleport
            // If position is not set to 0's, this means that
            // the saved position needs to be updated with "position"
            // Else, set position with current position
            if (position != new Vector3(0f, 0f, 0f))
            {
                data.playerPosition = position;
                position = new Vector3(); // Reset position variable
            }
            else
            {
                data.playerPosition = transform.position;
            }
        }

        private void ProcessInputs()
        {
			if(Input.GetKeyDown(KeyCode.M))
			{
				StartCoroutine(Load_Level.SceneLoader(mapName));
			}
			else
			{
				if (AllowMovement)
				{
					// Create two variables for current X,Y axis
					float moveX = Input.GetAxisRaw("Horizontal");
					float moveY = Input.GetAxisRaw("Vertical");

					// Create a new variable for the move direction
					moveDirection = new Vector2(moveX, moveY).normalized;
				}
			}
        }

        private void Move()
        {
            // Calculate where to move to
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

            // Update Bit's direction based on movement; for animation purposes
            if(moveDirection != Vector2.zero)
            {
                an.SetFloat("XInput", moveDirection.x);
                an.SetFloat("YInput", moveDirection.y);
                an.SetBool("IsWalking", true);
            }
            else
            {
                an.SetBool("IsWalking", false);
            }
        }
    }
}
