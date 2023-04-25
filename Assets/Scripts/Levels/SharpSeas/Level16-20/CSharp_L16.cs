/*
 * This script allows the player the ability to use the
 * CSharp compiler. This script will allow the player to
 * define and use their own function in our game.
 * 
 * Author: Robot and I Team
 * Last modification date: 04/21/2023
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;

namespace CSharpLevels
{
    public class CSharp_L16 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TMP_InputField playerCodeInput;
        public TMP_InputField playerFunctionInput;
        public GameObject Window;

        // Private variables
        private bool displayLog;
        private const int ExecutionTime = 6; // Time in Seconds before the script gets killed

        /*
         * This function is the driver to the sequence of events that are
         * required to:
         * Grabs the input from the TextMeshPro box.
         * Add the player's input TextMeshPro input code to a template code string 
         * Parse/Compile the code written in the box.
         * Check for errors, and notify the player if any.
         * Display the output from the code to a TextMeshPro box if any.
         */
        public void MainDriver()
        {
            // Local variables
            Assembly resultAssembly;
            Type runtimeClass;
            MethodInfo runtimeFunction;
            Func<GameObject, MonoBehaviour> runtimeDelegate;

            // Check and modify the player's input before ran
            string playerText = InputModification(playerCodeInput.text);

            // Add the player's code to a template for runtime scripting
            string playerCode = @"
            using UnityEngine; // Access to unity objects
            using System.Collections; // Access to coroutines

            public class RuntimeScript : MonoBehaviour
            {
                // Reference to the Bit object
                private GameObject Bit;

                // This function adds a script to the host object
                // This script addition is required so that Unity can
                // run it during runtime.
                public static RuntimeScript AddYourselfTo(GameObject host)
                {
                    // Add RuntimeScript to the host object
                    return host.AddComponent<RuntimeScript>();
                }

                // When the script is added to the HostGameObject
                // and invoked, run the following code
                void Start()
                {
                    // Initialize Bit object reference
                    Bit = GameObject.FindWithTag(""Player"");

                    // Display a friendly message to the player
                    print(""I am now processing your code input"");

                    // Start two coroutines
                    // GameTimer - Starts a timer to destroy this script
                    // MoveItMoveBit - Runs the player's code
                    StartCoroutine(GameTimer());
                    StartCoroutine(MoveItMoveBit());
                }

                // This function, GameTimer, times the execution of this script,
                // until time has has been hit by ExecutionTime. If time is hit,
                // this function removes the script from the ScriptController
                // as a means of stopping infinite loops.
                private IEnumerator GameTimer()
                {
                    // Wait for seconds
                    yield return new WaitForSeconds(" + ExecutionTime + @");

                    // At end of time, if script is still running,
                    // display message and remove script
                    print(""My CPU cannot handle long running code. Killing the execution process.\r\n"" + 
                          ""Please check that your code reaches an ending point in a timely manner :)"");
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, MoveItMoveBit, contains whatever the
                // player wrote from the input box.
                private IEnumerator MoveItMoveBit()
                {
                    // Updated string of player text
                    " + playerText + @"

                    // Required for coroutines to have a return
                    // Waits 2 seconds, then will destroy the game object.
                    yield return new WaitForSeconds(0.5f); 

                    // Remove the added script from the object
                    print(""Code execution has completed"");
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, MoveBit, is designed to take the current
                // Bit object and move him one unit to the right.
                public void MoveBit()
                {
                    // Move bit forward by 1 X-Unit
                    Bit.transform.Translate(1, 0, 0); // X, Y, Z Translation move
                }
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
                parameters.ReferencedAssemblies.Add(win32Location + "Microsoft.CSharp.dll");
                parameters.ReferencedAssemblies.Add(win32Location + "Facades\\netstandard.dll");
            }
            else
            {
                string assemblyLocation = parameters.ReferencedAssemblies.GetType().Assembly.Location;
                string folderPath = assemblyLocation.Substring(0, assemblyLocation.IndexOf("System.dll")); // Snip off the "System.dll" information
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.CoreModule.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.UI.dll");
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
         * This function, InputModification, checks and modifies the player's input.
         * This function will perform 4 checks on the player's input code.
         * 1. Check that the player has not written malicious code
         *    If so, kill the code before it is ran.
         * 2-4. If the player wrote a for, while, or do-while loop, properly
         *      modify the input code to yield resources for the kill code check.
         */
        private string InputModification(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;
            int startIndex, startCurlyBrace, endCurlyBrace;

            // Check for malicious code
            if (newCode.Contains("GameObject") || newCode.Contains("sleep"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }
            
            // If the player wrote a for-loop
            if (newCode.Contains("for"))
            {
                // Append on the line after for -> yield return null
                // Step 1: Find the starting index of the for-loop
                startIndex = newCode.IndexOf("for");

                // Step 2: Determine if the player wrote a curly brace (multi-line for-loop)
                startCurlyBrace = newCode.IndexOf('{', startIndex);
                if (startCurlyBrace != -1)
                {
                    // Step 3: Insert "yield return null" after the startCurlyBrace location
                    newCode = newCode.Insert(startCurlyBrace + 1, "yield return null;");
                }
                else // Single lined for-loop
                {
                    // Step 4: Add in curly braces and the "yield return null" code
                    // Left curlyBrace and the added code
                    startCurlyBrace = newCode.IndexOf(')', startIndex) + 1;
                    newCode = newCode.Insert(startCurlyBrace, "{yield return null;");

                    // Get an index for the end of this line
                    startIndex = newCode.IndexOf(';', startCurlyBrace) + 1;

                    // Get an index for the next semicolon (Single-lined for-loop, this is where to insert '}')
                    endCurlyBrace = newCode.IndexOf(';', startIndex) + 1;

                    // Insert the ending curly brace
                    newCode = newCode.Insert(endCurlyBrace, "}");
                }
            }

            // If the player wrote a do-while loop
            if (newCode.Contains("do"))
            {
                // Append on the line after do -> yield return null
                // Step 1: Find the starting index of the do-while loop
                startIndex = newCode.IndexOf("do");

                // Step 2: Determine if the player wrote a curly brace (multi-line do-while loop)
                startCurlyBrace = newCode.IndexOf('{', startIndex);
                if (startCurlyBrace != -1)
                {
                    // Step 3: Insert "yield return null" after the startCurlyBrace location
                    newCode = newCode.Insert(startCurlyBrace + 1, "yield return null;");
                }
                else // Single lined do-while loop
                {
                    // Step 4: Add in curly braces and the "yield return null" code
                    // Left curlyBrace and the added code
                    newCode = newCode.Insert(startIndex + 2, "{yield return null;");

                    // Get an index for while part of the loop
                    endCurlyBrace = newCode.IndexOf("while", startIndex);

                    // Insert the ending curly brace before the while
                    newCode = newCode.Insert(endCurlyBrace, "}");
                }
            }
            else
            {
                // If the player wrote a while-loop
                if (newCode.Contains("while"))
                {
                    // Append on the line after while -> yield return null
                    // Step 1: Find the starting index of the while-loop
                    startIndex = newCode.IndexOf("while");

                    // Step 2: Determine if the player wrote a curly brace (multi-line while-loop)
                    startCurlyBrace = newCode.IndexOf('{', startIndex);
                    if (startCurlyBrace != -1)
                    {
                        // Step 3: Insert "yield return null" after the startCurlyBrace location
                        newCode = newCode.Insert(startCurlyBrace + 1, "yield return null;");
                    }
                    else // Single lined while-loop
                    {
                        // Step 4: Add in curly braces and the "yield return null" code
                        // Left curlyBrace and the added code
                        startCurlyBrace = newCode.IndexOf(')', startIndex) + 1;
                        newCode = newCode.Insert(startCurlyBrace, "{yield return null;");

                        // Get an index for the end of this line
                        startIndex = newCode.IndexOf(';', startCurlyBrace) + 1;

                        // Get an index for the next semicolon (Single-lined while-loop, this is where to insert '}')
                        endCurlyBrace = newCode.IndexOf(';', startIndex) + 1;

                        // Insert the ending curly brace
                        newCode = newCode.Insert(endCurlyBrace, "}");
                    }
                }
            }

            return newCode;
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