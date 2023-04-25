using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Text;
using TMPro;

namespace PythonLevels
{
    public class Python_L13 : MonoBehaviour
    {
        // Public variables
        public TMP_InputField codeInput1;
        public TMP_InputField codeInput2;
        public TMP_InputField codeInput3;
        public TMP_InputField codeInput4;
        public TMP_InputField codeInput5;
        public TMP_InputField codeInput6;
        public TMP_InputField codeInput7;
        public TMP_InputField codeInput8;
        public TMP_InputField codeInput9;
        public TextMeshProUGUI programOutput;
        public string explanition;
        public GameObject lesson1;
        public GameObject lesson2;
        public BoxCollider2D levelSprite;
        private int count = 0;
        private int num = 0;
        public GameObject box;
        public GameObject bit;
        private Rigidbody2D rb;
        public GameObject[] flowerTests;
        /*
         * This function is the driver to the sequence of events that are
         * required to compile and execute upon Python code
         */
        void Start()
        {
            flowerTests = GameObject.FindGameObjectsWithTag("Box");
            box.SetActive(false);
        }
        void Update()
        {
            rb = box.GetComponent<Rigidbody2D>();
            if (rb.isKinematic)
            {
                foreach (GameObject go in flowerTests)//searches for "Grabbable" objects
                {
                    if (go.transform.position.x + 1 >= bit.transform.position.x && go.transform.position.x - 1 <= bit.transform.position.x && go.activeSelf)
                    {
                        go.SetActive(false);
                        num++;
                        
                    }
                    if (num == 4)
                    {
                        levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                    }
                }
            }
            
        }
            public void setText()
        {
            if (count == 0)
                programOutput.text = "load array \"a\" with values 1,2,3,4.\nNext, use a for loop to add every\n value in \"a\" by 1.\nThen, use a for loop to subtract every\n value in \"a\" by 2.";
            else
                programOutput.text = "Given \"le\" as length of \"b\" array.\nUse a for loop to reverse the order of b.";
        }

        public void setActives()
        {
            lesson1.SetActive(true);
            lesson2.SetActive(false);
        }


        public void MainDriver()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            // Modify the player's input code to have proper indentation
            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
def main():
    snake = 2
    red = 1
    a = " + codeInput1.text + @"
    be = " + codeInput1.text + @"
    c = " + codeInput1.text + @"
    go = c
    for " + codeInput2.text + @"
        go[j] = " + codeInput3.text + @"
        a[j] = " + codeInput3.text + @"
    for " + codeInput4.text + @"
        a[i] = " + codeInput5.text + @"
    x = a
    if be == [1,2,3,4] and go == [2,3,4,5] and x == [0,1,2,3]:
        print(""Correct"")
    else:
        print(""Incorrect\n1st a = "" + str(be) + ""\n2nd a = "" + str(go) + ""\n3rd a = "" + str(x))
    return";

            scriptEngine = Python.CreateEngine();
            scriptScope = scriptEngine.CreateScope();
            MemoryStream codeOutput = new MemoryStream(); // Unbounded stream of data storage
            scriptEngine.Runtime.IO.SetOutput(codeOutput, Encoding.Default);


            scriptEngine.Execute(playerCode, scriptScope);


            scriptFunction = scriptScope.GetVariable("main");
            scriptFunction(); // Execution of function "main"

            // Test if the player used a print statement
            if (codeOutput.Length > 0)
            {
                PythonPrint(codeOutput);
                codeOutput.Close();
            }
        }


        public void MainDriver2()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            // Modify the player's input code to have proper indentation
            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
def main():
    stripes = 8
    snake = 2
    red = 1
    a = [1,2,3,4,5]
    b = a
    le = len(b)
    " + codeInput6.text + @"
        " + codeInput7.text + @"
        " + codeInput8.text + @"
        " + codeInput9.text + @"
        snake = snake + 1
    if b == [5,4,3,2,1] and snake == 4:
        print(""Congratulations"")
    else:
        print(""Incorrect\nb = "" + str(b) + str(snake))
    return";

            scriptEngine = Python.CreateEngine();
            scriptScope = scriptEngine.CreateScope();
            MemoryStream codeOutput = new MemoryStream(); // Unbounded stream of data storage
            scriptEngine.Runtime.IO.SetOutput(codeOutput, Encoding.Default);


            scriptEngine.Execute(playerCode, scriptScope);


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

            if (stringData == "Correct\r\n")
            {
                if (count == 0)
                {
                    lesson1.SetActive(false);
                    lesson2.SetActive(true);
                    count++;
                }
            }

            // Allow the player to leave the level
            if (stringData == "Congratulations\r\n")
            {
                box.SetActive(true);
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
