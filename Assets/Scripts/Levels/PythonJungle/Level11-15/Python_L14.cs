using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Text;
using TMPro;

namespace PythonLevels
{
    public class Python_L14 : MonoBehaviour
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
        public GameObject[] snakeTests;

        void Start()
        {
            snakeTests = GameObject.FindGameObjectsWithTag("Box");
            foreach (GameObject go in snakeTests)//searches for "Box" objects
            {
                if(!go.name.Contains("(0)"))
                    go.SetActive(false);
            }
            box.SetActive(false);
        }
        void Update()
        {
            rb = box.GetComponent<Rigidbody2D>();
            if (rb.isKinematic)
            {
                foreach (GameObject go in snakeTests)//searches for "Box" objects
                {
                    if (go.transform.position.x + 1 >= bit.transform.position.x && go.transform.position.x - 1 <= bit.transform.position.x && go.activeSelf)
                    {
                        if (go.transform.position.y + 5 >= bit.transform.position.y && go.transform.position.y - 5 <= bit.transform.position.y)
                        {
                            if (num < 6)
                                go.SetActive(false);
                            num++;
                        }
                        
                    }
                    if (go.name.Contains("(" + num + ")"))
                        go.SetActive(true);
                    if (num == 6)
                    {
                        levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                    }
                }
            }

        }
        /*
         * This function is the driver to the sequence of events that are
         * required to compile and execute upon Python code
         */
        public void setText()
        {
            if (count == 0)
                programOutput.text = "Create a 2d array \"a\" with values 6,8,10,12\nin the first set and values 4,3,6,5\nin the second set.\n use a for loop to divide the values\nin the first set by 2. \n Then, multiply the second set by 3.";
            else
                programOutput.text = "Given \"le\" as length of \"b\" array.\nUse a for loop to swap each value between b[0] and b[1].";
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
    go = c[0]
    x = c[1]
    for " + codeInput2.text + @"
        go[j] = " + codeInput3.text + @"
        a[0][j] = " + codeInput3.text + @"
    for " + codeInput4.text + @"
        x[i] = " + codeInput5.text + @"
        a[1][i] = " + codeInput5.text + @"
    if be == [[6,8,10,12],[4,3,6,5]] and go == [3,4,5,6] and x == [12,9,18,15]:
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
    snake = 0
    red = 1
    a = [[1,2,3,4,5],[6,7,8,9,10]]
    b = a
    le = len(b[0])
    " + codeInput6.text + @"
        " + codeInput7.text + @"
        " + codeInput8.text + @"
        " + codeInput9.text + @"
        snake = snake + 1
    if b == [[6,7,8,9,10],[1,2,3,4,5]] and snake == 5:
        print(""Congratulations"")
    else:
        print(""Incorrect\nb = "" + str(b))
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
