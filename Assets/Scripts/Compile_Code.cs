/*
 * This script is the driver for checking and acting upon input text for a TMP_InputField
 * This script has two primary methods for doing this.
 * 
 * 1. Parse the input text and check if it conforms to the language desired
 * 2. If the language conforms, this script will produce the desired action
 * from the input text.
 *    Else, this script will send the error information to a TextMeshProUGUI
 * so that the user can try again.
 * 
 * Author: Robot and I Team
 * Last modification date: 10-07-2022
 */

using UnityEngine;
using TMPro;

public class Compile_Code : MonoBehaviour
{
    // Public variables
    public string language; // What language are we parsing
    public TMP_InputField userInput; // References the User's Input Field
    public TextMeshProUGUI programOutput; // References the TMP Output Field

    public void SyntaxCheck()
    {
        // Goals:
        // 1. What did the user write - Complete
        // 2. Does the language conform to the target language - In Progress
        // 3. What do I need to do and how do I do it
        if (language == "C++")
        {
            // Do language parser stuff here
            programOutput.text = "C++ code\n";
            programOutput.text += userInput.text;
        }
        else if (language == "Python")
        {
            // Do language parser stuff here
            programOutput.text = "Currently not yet implemented";
        }
    }
}
