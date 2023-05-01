/*
 * This script allows the player the ability to use the
 * CSharp compiler. This script will allow the player to
 * define and use their own function in our game.
 * 
 * Author: Robot and I Team
 * Last modification date: 04/25/2023
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;
using GameMechanics;

namespace CSharpLevels
{
    public class CSharp_L16 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI soundCheck;
        public TextMeshProUGUI answerCheck;
        public TMP_InputField playerCodeInput;
        public TMP_InputField playerFunctionInput;
        public GameObject Window;
        public List<GameObject> Numbers;

        // Private variables
        private bool displayLog;
        private const int ExecutionTime = 6; // Time in Seconds before the script gets killed

        // Use update to check the soundCheck input box
        private void Update()
        {
            // Check the soundCheck text box
            switch (soundCheck.text)
            {
                case "Correct":
                    //Audio_Manager.Instance.PlaySound("Correct");
                    soundCheck.text = "";
                    break;

                case "Incorrect":
                    //Audio_Manager.Instance.PlaySound("Incorrect");
                    soundCheck.text = "";
                    break;
            }
        }

        /*
         * This function drives the entire program. This function
         * is ran when the user clicks the run button.
         */
        public void MainDriver()
        {
            // Local variables
            Assembly resultAssembly;
            Type runtimeClass;
            MethodInfo runtimeFunction;
            Func<GameObject, MonoBehaviour> runtimeDelegate;

            // Check and modify the player's input before ran
            string modifiedPlayerCode = InputModificationCode(playerCodeInput.text);
            string modifiedPlayerFunction = InputModificationFunction(playerFunctionInput.text);

            // Add the player's code to a template for runtime scripting
            string playerCode = @"
            using UnityEngine;
            using System;
            using System.Collections;
            using TMPro;

            public class RuntimeScript : MonoBehaviour
            {
                // Global definition of variables
                private TMP_Text output, soundBox, completionBox;
                private int Timer = 0; // Infinite Loop Checker

                // Required for addition to the GameObject
                public static RuntimeScript AddYourselfTo(GameObject host)
                {
                    // Add RuntimeScript to the host object
                    return host.AddComponent<RuntimeScript>();
                }

                // When the script is added to the HostGameObject and invoked, run the following code
                void Start()
                {
                    // Must call GameObject.Find from within start
                    output = GameObject.Find(""Error Output"").GetComponent<TMP_Text>();
                    completionBox = GameObject.Find(""AnswerBox"").GetComponent<TMP_Text>();
                    soundBox = GameObject.Find(""SoundCheckBox"").GetComponent<TMP_Text>();

                    // Start a Coroutine and the Main function
                    // GameTimer - Starts a timer to destroy this script
                    // Main() - Runs the player's code
                    StartCoroutine(GameTimer());
                    Main();
                }

                // This function, GameTimer, times the execution of this script
                // until time has has been hit by ExecutionTime. If time is hit,
                // this function removes the script from the ScriptController
                // as a means of stopping infinite loops.
                private IEnumerator GameTimer()
                {
                    // Wait for seconds
                    yield return new WaitForSeconds(" + ExecutionTime + @");

                    // At end of time, if script is still running, display message and remove script
                    output.text = ""My CPU cannot handle long running code. Killing the execution process.\r\n"" + 
                          ""Please check that your code reaches an ending point in a timely manner :)"";
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, Main, contains whatever the
                // player wrote from the code input box.
                private void Main()
                {
                    // The problem statement is already displayed. Define the integer arrays
                    int[] Set1 = new int[5] { 25, 9, 87, 63, 91 };
                    int[] Set2 = new int[5] { 100, 42, 6, 900, 7 };
                    int[] Set3 = new int[5] { 2, 21, 98, 45, 50 };
                    int[] Set4 = new int[5] { 8, 800, 543, 770, 1 };
                    int[] Set5 = new int[5] { 20, 10, 60, 30, 70 };

                    // Fill in the player's code
                    " + modifiedPlayerCode + @"

                    // Check for the correct answer
                    if (Result1 == 275 && Result2 == 1055 && Result3 == 216 &&
                        Result4 == 2122 && Result5 == 190)
                    {
                        completionBox.text = ""Correct"";
                        soundBox.text = ""Correct"";
                        GameObject.FindWithTag(""LevelChange"").GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        completionBox.text = ""Incorrect"";
                        soundBox.text = ""Incorrect"";
                    }

                    // Remove the added script from the object
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function is defined by the player.
                // Due to the Problem Statement, this function should
                // be named Sum, take in a parameter of an int array
                // and finally should return an integer that is the sum
                // of the numbers in the passed array.
                " + modifiedPlayerFunction + @"
            }";

            // Compile the player's code and check for syntax issues
            displayLog = true;
            resultAssembly = CSharpCompile(playerCode);

            // Get the Start() method signature to invoke
            runtimeClass = resultAssembly.GetType("RuntimeScript");
            runtimeFunction = runtimeClass.GetMethod("AddYourselfTo"); // Now references the dynamic RuntimeScript
            runtimeDelegate = (Func<GameObject, MonoBehaviour>) // Typecast a delegate to whatever type this is
                                Delegate.CreateDelegate(typeof(Func<GameObject, MonoBehaviour>), runtimeFunction);

            // Invoke the script, indirectly running the Start() method
            runtimeDelegate.Invoke(gameObject);

            // Start the Coroutine for addition of numbers
            StartCoroutine(AddNumbers());
        }

        /*
         * This function performs the compilation sequence for C# code.
         * If the input code has syntax errors, this function will output
         * all the errors to the supplied output TextMeshPro box.
         * If the code is successfully compiled, control returns to
         * the MainDriver function.
         */
        private Assembly CSharpCompile(string sourceCode)
        {
            // Local variables for the compiler and compiler parameters
            CSharpCodeCompiler compiler = new CSharpCodeCompiler();
            CompilerParameters parameters = new CompilerParameters();
            CompilerResults result;

            /* Add in the .dll files for the compilation to take place
             * 
             * UnityEngine.dll = This contains methods from Unity namespaces
             * Microsoft.CSharp.dll = This assembly contains runtime C# code from your Assets folders
             * netstandard.dll = Other assembly that is required (.NetFramework specific)
             * 
             * NOTE: Path locations may vary based on install. Below code attempts to find these paths
             * using path location of the System.dll assembly location.
             */
            if (Application.isEditor)
            {
                string path1 = @"Data\PlaybackEngines\windowsstandalonesupport\Variations\win32_player_development_mono\Data\Managed\";
                string path2 = @"Data\Resources\PackageManager\ProjectTemplates\libcache\com.unity.template.2d-7.0.1\ScriptAssemblies\";
                string assemblyLocation = parameters.ReferencedAssemblies.GetType().Assembly.Location;
                string win32Location = assemblyLocation.Substring(0, assemblyLocation.IndexOf("System.dll")); // Snip off the "System.dll" information
                string engineLocation = assemblyLocation.Substring(0, assemblyLocation.IndexOf("Data")); // Extract base location for Data folder
                parameters.ReferencedAssemblies.Add(engineLocation + path1 + "UnityEngine.CoreModule.dll");
                parameters.ReferencedAssemblies.Add(engineLocation + path2 + "UnityEngine.UI.dll");
                parameters.ReferencedAssemblies.Add(engineLocation + path1 + "UnityEngine.Physics2DModule.dll");
                parameters.ReferencedAssemblies.Add(engineLocation + path2 + "Unity.TextMeshPro.dll");
                parameters.ReferencedAssemblies.Add(win32Location + "Microsoft.CSharp.dll");
                parameters.ReferencedAssemblies.Add(win32Location + "Facades\\netstandard.dll");
            }
            else
            {
                string assemblyLocation = parameters.ReferencedAssemblies.GetType().Assembly.Location;
                string folderPath = assemblyLocation.Substring(0, assemblyLocation.IndexOf("System.dll")); // Snip off the "System.dll" information
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.CoreModule.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.UI.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.Physics2DModule.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "Unity.TextMeshPro.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "Microsoft.CSharp.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "netstandard.dll");
            }

            // Set compiler parameters
            // NOTE: Set "IncludeDebugInformation" to false when pushed into production
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.IncludeDebugInformation = true;

            // Compile the sourceCode 
            result = compiler.CompileAssemblyFromSource(parameters, sourceCode);

            // Check if there were compilation errors
            programOutput.text = ""; // Clear the current output box
            if (result.Errors.HasErrors)
            {
                displayLog = false;

                // Display only 1 error
                CompilerErrorCollection error = result.Errors;
                programOutput.text = error[0].ErrorNumber switch
                {
                    // Syntax Errors
                    "CS1525" => $"Error: You have made a Syntax Error in your code.\n\n{error[0].ErrorText}",

                    // Variable Name Unknown. EX. int spellMeRight; => spelMeRight = 0;
                    "CS0103" => $"Error: You have a typo in one of your variable names, or you never declared it.\n\n{error[0].ErrorText}",

                    // Uninitialized Variable usage
                    "CS0165" => $"Error: You are trying to use a variable in your code that has not been assigned a value.\n\n{error[0].ErrorText}",

                    // Variable Type Mismatch. EX. if (string == int)
                    "CS0019" => $"Error: You are cannot compare two different data types together.\n\n{error[0].ErrorText}",

                    // Variable Type Mismatch. EX. int = double;
                    "CS0266" => $"Error: You are trying to assign a Double data type into an Integer data type.\n\n{error[0].ErrorText}",

                    // Variable Type Mismatch. EX. string = int;
                    "CS0029" => $"Error: You are cannot assign two different data types together.\n\n{error[0].ErrorText}",

                    // All Other Errors
                    _ => $"Line: {error[0].Line}\n\nError: {error[0].ErrorText}",
                };
            }

            // Return the assembly
            return result.CompiledAssembly;
        }

       /*
        * This function, InputModificationCode, checks and modifies the player's code input.
        * This function will check that the player declared the proper variables as required
        * by the problem statement. Once all checks are complete, this code is passed to the MainDriver();
        */
        private string InputModificationCode(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;

            // Check for malicious code
            if (newCode.Contains("GameObject") || newCode.Contains("sleep"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // Check for declared variables
            if (!newCode.Contains("Result1") || !newCode.Contains("Result2") ||
                !newCode.Contains("Result3") || !newCode.Contains("Result4") ||
                !newCode.Contains("Result5")
            )
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must use Result1, Result2, Result3, Result4 and Result5 in your code.";
                throw new Exception("Sorry, but you must use Result1, Result2, Result3, Result4 and Result5 in your code.");
            }

            // Check that the user called the function
            if (!newCode.Contains("Sum"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must call your defined function Sum.";
                throw new Exception("Sorry, but you must call your defined function Sum.");
            }

            // Update any loops that the user may have been written.
            newCode = ModifyLoops(newCode);

            return newCode;
        }

        /*
         * This function, InputModificationFunction, checks and modifies the player's function input.
         * This function will check that the player declared the proper variables as required
         * by the problem statement. Once all checks are complete, this code is passed to the MainDriver();
         */
        private string InputModificationFunction(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;

            // Check for malicious code
            if (newCode.Contains("GameObject") || newCode.Contains("sleep"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // Check the player defined the function correctly
            if (!newCode.Contains("int Sum"))
            {
                // Inform the player they must define their function correctly
                programOutput.text = "Sorry, but you must define a function named Sum, and it must return an integer.";
                throw new Exception("Sorry, but you must define a function named Sum, and it must return an integer.");
            }

            // Don't let the player use recursion
            if (newCode.Contains("=Sum") || newCode.Contains("= Sum"))
            {
                // Inform the player that this is too complex for them
                programOutput.text = "Woah Bit! Calling the function you are defining inside itself is called recursion and is a little too complex for you at this time.\r\nI recommend solving this problem another way.";
                throw new Exception("Woah Bit! Calling the function you are defining inside itself is called recursion and is a little too complex for you at this time.\r\nI recommend solving this problem another way.");
            }

            // Update any loops that the user may have been written.
            newCode = ModifyLoops(newCode);

            return newCode;
        }

        /*
         * This function, ModifyLoops, checks the input string for loops of any kind.
         * If the string contains a loop, it updates it so that that loop will count
         * and then kill the loop if it exceeds 100.
         * This function will return the new string when finished.
         */
        private string ModifyLoops(string oldString)
        {
            // Create a string for the return
            string modifiedString = oldString;
            int startIndex, startCurlyBrace, endCurlyBrace;

            // Define an additional code string to insert into the user's code
            string additionalCode = @"
            Timer += 1;
            if (Timer > 100)
            {
                Timer = 0;
                break;
            }
            ";

            // If the player wrote a for-loop
            if (modifiedString.Contains("for"))
            {
                // Append on the line after for -> additionalCode
                // Step 1: Find the starting index of the for-loop
                startIndex = modifiedString.IndexOf("for");

                // Step 2: Determine if the player wrote a curly brace (multi-line for-loop)
                startCurlyBrace = modifiedString.IndexOf('{', startIndex);
                if (startCurlyBrace != -1)
                {
                    // Step 3: Insert "additionalCode" after the startCurlyBrace location
                    modifiedString = modifiedString.Insert(startCurlyBrace + 1, additionalCode);
                }
                else // Single lined for-loop
                {
                    // Step 4: Add in curly braces and the "additionalCode" code
                    // Left curlyBrace index
                    startCurlyBrace = modifiedString.IndexOf(')', startIndex) + 1;

                    // Get an index for the end of this line
                    endCurlyBrace = modifiedString.IndexOf(';', startCurlyBrace) + 1;

                    // Insert the ending curly brace, then the beginning curly brace
                    modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                    modifiedString = modifiedString.Insert(startCurlyBrace, "{" + additionalCode);
                }
            }

            // If the player wrote a do-while loop
            if (modifiedString.Contains("do"))
            {
                // Append on the line after do -> additionalCode
                // Step 1: Find the starting index of the do-while loop
                startIndex = modifiedString.IndexOf("do");

                // Step 2: Determine if the player wrote a curly brace (multi-line do-while loop)
                startCurlyBrace = modifiedString.IndexOf('{', startIndex);
                if (startCurlyBrace != -1)
                {
                    // Step 3: Insert "additionalCode" after the startCurlyBrace location
                    modifiedString = modifiedString.Insert(startCurlyBrace + 1, additionalCode);
                }
                else // Single lined do-while loop
                {
                    // Step 4: Add in curly braces and the "additionalCode" code
                    // Get an index for while part of the loop
                    endCurlyBrace = modifiedString.IndexOf("while", startIndex);

                    // Insert the ending curly brace before the while
                    modifiedString = modifiedString.Insert(endCurlyBrace, "}");

                    // Left curlyBrace and the added code
                    modifiedString = modifiedString.Insert(startIndex + 2, "{" + additionalCode);
                }
            }
            else
            {
                // If the player wrote a while-loop
                if (modifiedString.Contains("while"))
                {
                    // Append on the line after while -> additionalCode
                    // Step 1: Find the starting index of the while-loop
                    startIndex = modifiedString.IndexOf("while");

                    // Step 2: Determine if the player wrote a curly brace (multi-line while-loop)
                    startCurlyBrace = modifiedString.IndexOf('{', startIndex);
                    if (startCurlyBrace != -1)
                    {
                        // Step 3: Insert "additionalCode" after the startCurlyBrace location
                        modifiedString = modifiedString.Insert(startCurlyBrace + 1, additionalCode);
                    }
                    else // Single lined while-loop
                    {
                        // Step 4: Add in curly braces and the "additionalCode" code
                        // Left curlyBrace index
                        startCurlyBrace = modifiedString.IndexOf(')', startIndex) + 1;

                        // Get an index for the end of this line
                        endCurlyBrace = modifiedString.IndexOf(';', startCurlyBrace) + 1;

                        // Insert the ending curly brace, then the beginning curly brace
                        modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                        modifiedString = modifiedString.Insert(startCurlyBrace, "{" + additionalCode);
                    }
                }
            }

            return modifiedString;
        }

        // This function, AddNumbers, will visually
        // add five numbers on the screen
        private IEnumerator AddNumbers()
        {
            // Get the current position of the numbers that will be moved
            Vector3 numPosition1 = Numbers[0].transform.position;
            Vector3 numPosition2 = Numbers[4].transform.position;

            // Get a text object for the Numbers to be modified
            TMP_Text numText1 = Numbers[0].GetComponent<TMP_Text>();
            TMP_Text numText2 = Numbers[4].GetComponent<TMP_Text>();

            // Wait two seconds for the player's code to finish
            // executing before continuing on in the code
            yield return new WaitForSecondsRealtime(1f);

            // Check the answer box to see if the user got the correct sums
            switch (answerCheck.text)
            {
                case "Correct":
                    // Move the leftmost number into the next right number
                    for (int i = 0; i < 70; ++i)
                    {
                        Numbers[0].transform.Translate(1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the addition of the numbers and set Number[1] inactive
                    numText1.text = "14029";
                    Numbers[1].SetActive(false);

                    // Move the rightmost number into the next left number
                    for (int i = 0; i < 70; ++i)
                    {
                        Numbers[4].transform.Translate(-1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the addition of the numbers and set Number[3] inactive
                    numText2.text = "11457";
                    Numbers[3].SetActive(false);

                    // Move the leftmost number into the next right number again
                    for (int i = 0; i < 65; ++i)
                    {
                        Numbers[0].transform.Translate(1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the addition of the numbers and set Number[2] inactive
                    numText1.text = "15029";
                    Numbers[2].SetActive(false);

                    // Move the rightmost number into the now leftmost number
                    for (int i = 0; i < 75; ++i)
                    {
                        Numbers[4].transform.Translate(-1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the addition of the numbers and set Number[0] inactive
                    numText2.text = "26486";
                    Numbers[0].SetActive(false);
                    programOutput.text = "Looks like you have mastered functions also Bit. You are almost finished with helping the Bittonian people. I am so proud of you! :)";
                    break;

                case "Incorrect":
                    // Move the leftmost number into the next right number
                    for (int i = 0; i < 70; ++i)
                    {
                        Numbers[0].transform.Translate(1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the incorrect addition of the numbers and set Number[1] inactive
                    numText1.text = "12050";
                    Numbers[1].SetActive(false);

                    // Move the rightmost number into the next left number
                    for (int i = 0; i < 70; ++i)
                    {
                        Numbers[4].transform.Translate(-1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the incorrect addition of the numbers and set Number[3] inactive
                    numText2.text = "10025";
                    Numbers[3].SetActive(false);

                    // Move the leftmost number into the next right number again
                    for (int i = 0; i < 65; ++i)
                    {
                        Numbers[0].transform.Translate(1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the incorrect addition of the numbers and set Number[2] inactive
                    numText1.text = "15075";
                    Numbers[2].SetActive(false);

                    // Move the rightmost number into the now leftmost number
                    for (int i = 0; i < 75; ++i)
                    {
                        Numbers[4].transform.Translate(-1f, 0, 0);
                        yield return new WaitForSeconds(0.05f);
                    }

                    // Change the text to the incorrect addition of the numbers and set Number[0] inactive
                    numText2.text = "25000";
                    Numbers[0].SetActive(false);
                    programOutput.text = "Bit... I have this strange suspicion that the addition you and I just witnessed is incorrect.\r\nYou should check the code you wrote to be certain.";
                    break;
            }

            // Wait a few seconds for dramatic effect and then reset everything.
            yield return new WaitForSecondsRealtime(2f);
            Numbers[0].transform.position = numPosition1;
            Numbers[4].transform.position = numPosition2;
            numText1.text = "5897";
            numText2.text = "4242";
            Numbers[0].SetActive(true);
            Numbers[1].SetActive(true);
            Numbers[2].SetActive(true);
            Numbers[3].SetActive(true);
        }

        public void SwapWindows() { Window.SetActive(!Window.activeSelf); }

        /*
         * This function is called whenever Unity sends output to the
         * log console, or whenever the player issues Debug.log()/print() statements.
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
         * display to the player. Code that is printed using Debug.log() or print(),
         * will be displayed. Any runtime messages from the compilation will also be
         * displayed from this function.
         */
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (displayLog) // Prevents duplicate error prints with compile time and log print
            {
                programOutput.text = logString + "\n\n";
            }
        }
    }
}