/* This script is the driver to sending dialogue to the
 * "TV Screen" in the Levels. This script is placed on
 * the "Start Lesson Button" and filled in with data by the developer.
 * This script handles:
 * Sending the dialogue to the TV Screen
 * Contains methods for the Start Lesson and Continue buttons
 * and also handles moving objects in and out of active state.
 * 
 * Author: Robot and I Team
 * Date Modification: 12-28-2022
 */

using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace GameMechanics
{
    public class Level_Dialogue : MonoBehaviour
    {
        // Get references to objects in the scene
        public GameObject DialogueObject;
        public TextMeshProUGUI NameObject;
        public TextMeshProUGUI TMP_DialogueObject;
        public GameObject StartButton;
        public GameObject LessonObject;

        // Variables to be filled in by the developer
        public string ProfessorName;
        // Text Area creates a wider text input box for the developer
        [TextArea(5,30)] public string[] sentences;

        // Private queue for the sentences
        private Queue<string> Dialogue;

        private void Start()
        {
            // Creates a new Dialogue queue for
            // each object this script is attached to
            Dialogue = new Queue<string>();
        }

        // Start the Lesson Dialogue
        public void StartDialogue()
        {
            // Clear the Queue and load up all the sentences
            Dialogue.Clear();
            foreach (string sentence in sentences)
                Dialogue.Enqueue(sentence);

            // Set the information in the "TV Screen"
            NameObject.text = ProfessorName;
            TMP_DialogueObject.text = Dialogue.Dequeue();

            // Make the "StartLesson" button disappear
            StartButton.SetActive(false);
        }

        // Send the next sentence when the user hits the continue button
        public void DisplayNextSentence()
        {
            // Check if more sentences to display
            if (Dialogue.Count > 0)
            {
                TMP_DialogueObject.text = Dialogue.Dequeue();
            }
            else // Close the TV and start the Lesson
            {
                DialogueObject.SetActive(false);
                LessonObject.SetActive(true);
            }
        }
    }
}