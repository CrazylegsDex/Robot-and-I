/*
 * This script is the driver for checking and acting upon input text for a TMP_InputField
 * This script has two primary methods for doing this.
 * 
 * 1. Get the input from the InputTextBox and compile it
 * 2. Act upon the results from the compilation.
 * 
 * Author: Robot and I Team
 * Last modification date: 10-10-2022
 */

using UnityEngine;
using TMPro;

public class PythonCodeCompiler : MonoBehaviour
{
    // Public variables
    public TMP_InputField playerInput; // References the Player's Input Field
    public TextMeshProUGUI programOutput; // References the TMP Output Field

    /*
     * This function is the driver to the sequence of events that are
     * required to accomplish the results for this script.
     * In other words, this function will attempt to accomplish the task
     * it is designed to when it is implemented.
     */
    public void MainDriver()
    {
        programOutput.text = "Currently unemplemented.\n\n";
        programOutput.text += playerInput.text;
    }
}