/* This script is the driver to sending dialogue to the
 * "TV Screen" in the Levels. This script is placed on
 * the "Start Lesson Button" and filled in with data by the developer.
 * This script handles:
 * Sending the dialogue to the TV Screen
 * Contains methods for the Start Lesson, Continue and Back buttons
 * and also handles moving objects in and out of active state.
 * 
 * Author: Robot and I Team
 * Date Modification: 04-30-2022
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

        // Two private stacks for the sentences
        private Stack<string> PrimaryStack; // Holds sentences for Continue Button
        private Stack<string> SecondaryStack; // Holds sentences for Back Button

        private void Start()
        {
            // Creates the stacks for each object this script is attached to
            PrimaryStack = new Stack<string>();
            SecondaryStack = new Stack<string>();
        }

        // Start the Lesson Dialogue
        public void StartDialogue()
        {
            // Clear the Stacks and load up all the sentences
            // into Primary Stack (in reverse)
            PrimaryStack.Clear();
            SecondaryStack.Clear();
            for (int i = sentences.Length - 1; i >= 0; --i)
                PrimaryStack.Push(sentences[i]);

            // Set the information in the "TV Screen"
            NameObject.text = ProfessorName;
            TMP_DialogueObject.text = PrimaryStack.Pop();

            // Make the "StartLesson" button disappear
            StartButton.SetActive(false);
        }

        // Send the next sentence when the user hits the continue button
        public void DisplayNextSentence()
        {
            // Check if more sentences to display
            if (PrimaryStack.Count > 0)
            {
                // Push the current sentence onto Secondary Stack for storage
                // Then pop from Primary Stack into the Dialogue Object
                SecondaryStack.Push(TMP_DialogueObject.text);
                TMP_DialogueObject.text = PrimaryStack.Pop();
            }
            else // Close the TV and start the Lesson
            {
                DialogueObject.SetActive(false);
                LessonObject.SetActive(true);
            }
        }

        // Send the previous sentence when the user hits the back button
        public void DisplayPreviousSentence()
        {
            // Check if there are sentences to display
            if (SecondaryStack.Count > 0)
            {
                // Push the current sentence onto Primary stack for storage
                // Then pop from Secondary Stack into the Dialogue Object
                PrimaryStack.Push(TMP_DialogueObject.text);
                TMP_DialogueObject.text = SecondaryStack.Pop();
            }
        }
    }
}