using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Text;
using TMPro;

namespace PythonLevels
{
    public class Python_L6 : MonoBehaviour
    {
        // Public variables
        public TMP_InputField codeInput1;
		public TMP_InputField codeInput2;
		public TMP_InputField codeInput3;
		public TMP_InputField codeInput4;
		public TMP_InputField codeInput5;
		public GameObject if_1;
		public GameObject if_2;
		public GameObject if_3;
		public GameObject if_4;
		public GameObject if_5;
        public TextMeshProUGUI programOutput;
		public string explanition;
        public BoxCollider2D levelSprite;
		public GameObject net;
		private int count = 0;
		
		

        /*
         * This function is the driver to the sequence of events that are
         * required to compile and execute upon Python code
         */
		public void setText(){
			programOutput.text = explanition;
		}
		
		public void startActives(){
			if_2.SetActive(false);
			if_3.SetActive(false);
			if_4.SetActive(false);
			if_5.SetActive(false);
		}
		
		public void driverSelector(){
			if(count == 0){
				MainDriver1();
			}else if(count == 1){
				MainDriver2();
			}else if(count == 2){
				MainDriver3();
			}else if(count == 3){
				MainDriver4();
			}else{
				if(count == 4){
					MainDriver5();
				}
			}
		}
		
        public void MainDriver1()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            // Modify the player's input code to have proper indentation
            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
def main():
	firefly = 2
	catchable = False
	catch = False
	if(" + codeInput1.text + @"):
	    catchable = True
	if(catchable == True):
		print(""Correct"")
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
	firefly = 2
	catchable = False
	catch = False
	if(" + codeInput2.text + @"):
	    catchable = True
	if(catchable == True):
		print(""Correct"")
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
		
		public void MainDriver3()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            // Modify the player's input code to have proper indentation
            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
def main():
	firefly = 6
	catchable = False
	catch = False
	if(" + codeInput3.text + @"):
	    catchable = True
	if(catchable == True):
		print(""Correct"")
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
		
		public void MainDriver4()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            // Modify the player's input code to have proper indentation
            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
def main():
	firefly = 2
	catchable = False
	catch = False
	if(" + codeInput1.text + @"):
	    catchable = True
	if(" + codeInput2.text + @"):
	    catchable = True
	if(" + codeInput3.text + @"):
	    catchable = True
	if(" + codeInput4.text + @"):
	    catch = True
	if(catch == True):
		print(""Correct"")
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
		
		public void MainDriver5()
        {
            // Local variables
            ScriptEngine scriptEngine;
            ScriptScope scriptScope;
            dynamic scriptFunction;
            // Modify the player's input code to have proper indentation
            // Add the player's code to a defined python function for runtime running
            string playerCode = @"
def main():
	firefly = 2
	catchable = False
	catch = True
	if(" + codeInput5.text + @"):
	    catch = False
	if(catch == False):
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
			if(stringData == "Correct\r\n"){
				count++;
			}
			if(count == 1){
				if_1.SetActive(false);
				if_2.SetActive(true);
			}
			if(count == 2){
				if_2.SetActive(false);
				if_3.SetActive(true);
			}
			if(count == 3){
				if_3.SetActive(false);
				if_4.SetActive(true);
			}
			if(count == 4){
				if_4.SetActive(false);
				if_5.SetActive(true);
			}
			
			
			//programOutput.text = Convert.ToString(count);
			
            // Allow the player to leave the level
            if (stringData == "Congratulations\r\n")
            {
                levelSprite.isTrigger = true; 
				net.SetActive(true);// Sets levelSprite to trigger complete
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
