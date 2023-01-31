/* This script is the driver to sending dialogue to the
 * Dialogue box in the Islands. This script is placed on
 * an NPC and filled in with data by the developer.
 * This script handles:
 * Moving in and out of the NPC trigger area
 * Sending the dialogue to the Dialogue box
 * Contains methods for the Back, Continue and Start buttons.
 * 
 * Author: Robot and I Team
 * Date Modification: 12-28-2022
 */

using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace GameMechanics
{
    public class Island_Dialogue : MonoBehaviour
    {
        // Get references to the Dialogue Box
        public GameObject DialogueBox; // The Dialogue Box in Canvas
        public TextMeshProUGUI NameObject; // The Name child in DialogueBox
        public TextMeshProUGUI DialogueObject; // The Dialogue child in DialogueBox

        // Variables to be filled in by the developer
        public string NPC_Name;
        public string LevelToLoad;
        public bool levelComplete = false; // TODO: Implement during save/load features
        // Text Area creates a wider text input box for the developer
        [TextArea(5, 30)] public string[] sentences;

        // A public variable for Canvas_Dialogue to access, but the Developer cannot see
        [HideInInspector]
        public bool isActive;

        // Private queue for the sentences
        private Queue<string> Dialogue;

        private void Start()
        {
            // Creates a new Dialogue queue for
            // each NPC this script is attached to
            Dialogue = new Queue<string>();

            // Set its active variable to false
            isActive = false;
        }

        // Detect if the player hit the NPC
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) // Did the "Player" collide into the current object
            {
                // Set this NPC-script as active
                isActive = true;

                // Clear the Queue and load up all the sentences
                Dialogue.Clear();
                foreach (string sentence in sentences)
                    Dialogue.Enqueue(sentence);

                // Set the information in the DialogueBox
                NameObject.text = NPC_Name;
                DialogueObject.text = Dialogue.Dequeue();

                // Set the DialogueBox Animation so the player can see it
                DialogueBox.SetActive(true);
            }
        }

        // Detect if the player left the NPC
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) // Did the "Player" leave contact with the current object
            {
                // Set this NPC-script as now inactive
                isActive = false;

                // Set the DialogueBox Animation to disappear and clear the queue
                DialogueBox.SetActive(false);
                Dialogue.Clear();
            }
        }

        // Send the next sentence when the user hits the continue button
        public void DisplayNextSentence()
        {
            if (Dialogue.Count > 0)
            {
                DialogueObject.text = Dialogue.Dequeue();
            }
        }

        // Start the level
        public void StartLevel()
        {
            DialogueBox.SetActive(false);
            StartCoroutine(Load_Level.SceneLoader(LevelToLoad));
        }

        // Closes the DialogueBox
        public void CloseDialogue()
        {
            DialogueBox.SetActive(false);
        }
    }
}