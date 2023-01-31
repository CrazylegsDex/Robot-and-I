/*
 * This script allows the player the ability to use the
 * IronPython compiler. This script will allow the player
 * to write their own function and call it to test that
 * the conditional is correct.
 * 
 * Author: Robot and I Team
 * Last modification date: 11-15-2022
 */

using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Text;
using TMPro;

namespace PythonLevels
{
    public class Python_L17 : MonoBehaviour
    {
        // Public variables
        public TMP_InputField functionInput;
        public TMP_InputField codeInput;
        public TextMeshProUGUI programOutput;
        public BoxCollider2D levelSprite;

        /*
         * This function is the driver to the sequence of events that are
         * required to compile and execute upon Python code
         */
        public void MainDriver()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            string newPlayerInput;

            // Modify the player's input code to have proper indentation
            newPlayerInput = StringManipulation(functionInput.text);

            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
# Since Python delimits by whitespace, this is where code
# must start for whitespace to be properly delimitted.

# Define a function swap
def swap(num1, num2):
" + newPlayerInput + @"

# Define code to start at main
def main():
    val1 = 777
    val2 = 555
    r1, r2 = " + codeInput.text + @"
    if r1 == val2 and r2 == val1:
        print(""Congratulations"")
    else:
        print(""Try again"")
    return";

            // Create the engine, define a scope, and redirect print output
            scriptEngine = Python.CreateEngine();
            scriptScope = scriptEngine.CreateScope();
            MemoryStream codeOutput = new MemoryStream(); // Unbounded stream of data storage
            scriptEngine.Runtime.IO.SetOutput(codeOutput, Encoding.Default);

            // Compile the player's code using the engine and scope
            //programOutput.text = ""; // Clear the current output box
            scriptEngine.Execute(playerCode, scriptScope);

            // Get a handle to the function, then execute the Python function
            scriptFunction = scriptScope.GetVariable("main");
            scriptFunction(); // Execution of function "main"

            // Test if the player used a print statement
            if (codeOutput.Length > 0)
            {
                PythonPrint(codeOutput);
                codeOutput.Close();
            }
        }

        /*
         * This function executes if stdout data was found
         * in the executed Python code.
         * This function will retrieve that stdout data and send
         * it to the program's output text box.
         */
        private void PythonPrint(MemoryStream data)
        {
            // Local variables
            ASCIIEncoding encoding = new ASCIIEncoding();
            int dataLength = (int)data.Length;
            string stringData = "";
            byte[] byteData;
            char[] charData;

            // Set the memory stream to start reading from the start of data
            data.Seek(0, SeekOrigin.Begin);

            // Read the data into a byte array
            byteData = new byte[dataLength];
            data.Read(byteData, 0, dataLength);

            // Extract the byte data into character data
            charData = new char[encoding.GetCharCount(byteData, 0, dataLength)];
            encoding.GetDecoder().GetChars(byteData, 0, dataLength, charData, 0); // Decode the data into ASCII

            // Move the character data into a string for output into the textbox
            for (int i = 0; i < dataLength; ++i)
                stringData += charData[i];

            // Display the printed message
            programOutput.text = stringData;

            // Allow the player to leave the level
            if (stringData == "Congratulations\r\n")
            {
                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
            }
        }

        /*
         * This function modifies the player's input code string
         * to conform to Python's indentation of functions.
         * This function will accomplish this task by going through
         * the following checklist.
         * 1. Split the input string up into an array delimited by \n
         * 2. If the array has more than one element, indent all indices
         *    by 4 spaces
         * 3. If the array is a single element, return the array with an
         *    indention of 4 spaces
         */
        private string StringManipulation(string playerText)
        {
            // Local variables
            string returnString = "";
            string[] stringArray = playerText.Split("\n");

            // Check if the string was split, aka player put 2 or more lines of code
            if (stringArray.Length > 1)
            {
                // For each string in the array, indent the string by 4 spaces
                for (int i = 0; i < stringArray.Length; ++i)
                {
                    // PadLeft will pad spaces to the left IFF the specified length
                    // is greater than the length of the string itself
                    returnString += stringArray[i].PadLeft(stringArray[i].Length + 4) + "\n";
                }
            }
            else
                returnString = playerText.PadLeft(playerText.Length + 4);

            return returnString;
        }

        /*
         * This function is called whenever Unity sends output to the
         * log console, or whenever the player creates a Runtime error that
         * inherently gets sent to the Unity log console.
         * This function grabs the log and sends the information to the Handle
         */
        private void OnEnable() { Application.logMessageReceived += HandleLog; }

        /*
         * This function acts like a destructor for the Unity logs.
         * This function effectively removes the messages from the handle that
         * are captured during "OnEnable". This function is called whenever the
         * object is disabled.
         */
        void OnDisable() { Application.logMessageReceived -= HandleLog; }

        /*
         * This function is called whenever text is put into or taken away from
         * the Console logs. Essentially, this function will act like a runtime
         * display to the player. Any runtime messages from the compilation will also be
         * displayed from this function.
         */
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            programOutput.text = "";
            programOutput.text += logString;
        }
    }
}
