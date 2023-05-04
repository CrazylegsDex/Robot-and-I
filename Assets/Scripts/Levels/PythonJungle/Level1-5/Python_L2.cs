using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine.Audio;
using GameMechanics; // Pulls in the interface from GameMechanics

namespace PythonLevels
{
    public class Python_L2 : MonoBehaviour
    {
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
		public GameObject blank;
		public GameObject color;
		[TextArea(5, 30)] public string explanition;
		public BoxCollider2D levelSprite;
		
		public void setText(){
            programOutput.text = explanition;
        }
		
		public void setcolor(){
			color.SetActive(false);
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
    count = 0
    var1 = " + codeInput1.text + @"
    var2 = " + codeInput2.text + @"
    var3 = " + codeInput3.text + @"
    var4 = " + codeInput4.text + @"
    var5 = " + codeInput5.text + @"
    var6 = " + codeInput6.text + @"
    var7 = " + codeInput7.text + @"
    var8 = " + codeInput8.text + @"
    var9 = " + codeInput9.text + @"
    if(var1 == 5):
        count+=1
    if(var2 == 7):
        count+=1
    if(var3 == 3):
        count+=1
    if(var4 == 4):
        count+=1
    if(var5 == 2):
        count+=1
    if(var6 == 9):
        count+=1
    if(var7 == 6):
        count+=1
    if(var8 == 8):
        count+=1
    if(var9 == 1):
        count+=1
    if(count == 9):
        print(""Congratulations"")
    else:
        print(""Incorrect"")
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

            // Allow the player to leave the level
            if (stringData == "Congratulations\r\n")
            {
				Audio_Manager.Instance.PlaySound("Correct");
				blank.SetActive(false);
				color.SetActive(true);
				levelSprite.isTrigger = true;
            }
			else
				Audio_Manager.Instance.PlaySound("Incorrect");

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