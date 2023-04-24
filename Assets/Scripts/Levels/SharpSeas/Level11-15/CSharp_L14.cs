/*
 * This class uses the MonoCSharp Code Compiler.
 * This class will be run to allow the player to
 * learn about 1D arrays in C#.
 * 
 * Date: 04/22/2023
 * Author: Robot and I Team
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
    public class CSharp_L14 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI soundCheck;
        public TextMeshProUGUI answerCheck;
        public TMP_InputField playerInput;
        public GameObject BitsFriend;
        public List<Sprite> Friends;

        // Private variables
        private string declarations;
        private string userVariable;
        private const int ExecutionTime = 6; // Time in Seconds before the script gets killed
        private bool displayLog;

        // Use update to check the speedCheck and soundCheck input boxes
        private void Update()
        {
            // Check the soundCheck text box
            switch (soundCheck.text)
            {
                case "Correct":
                    Audio_Manager.Instance.PlaySound("Correct");
                    soundCheck.text = "";
                    break;
                    
                case "Incorrect":
                    Audio_Manager.Instance.PlaySound("Incorrect");
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
            string playerText = InputModification(playerInput.text);

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
                    completionBox = GameObject.Find(""Answer Check"").GetComponent<TMP_Text>();
                    soundBox = GameObject.Find(""SoundCheckBox"").GetComponent<TMP_Text>();

                    // Start two coroutines
                    // GameTimer - Starts a timer to destroy this script
                    // Main() - Runs the player's code
                    StartCoroutine(GameTimer());
                    StartCoroutine(Main());
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
                    completionBox.text = ""Error"";
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, Main, contains whatever the
                // player wrote from the input box.
                private IEnumerator Main()
                {
                    // The problem statement is already displayed. Define an array of animals
                    string[] Names = new string[]
                    {
                        ""Turtle"", ""Cow"", ""Horse"", ""Frog"", ""Giraffe"",
                        ""Moose"", ""Cat"", ""Elephant"", ""Eagle"", ""Snake"",
                        ""Seal"", ""Chicken"", ""Whale"", ""Pig"", ""Crocadille"",
                        ""Penguin"", ""Best Friend"", ""Shark"", ""Zebra"", ""Lion"",
                    };

                    // The player should define a 1D array named Friends
                    " + declarations + @"

                    // Fill the 1D array with data
                    for (int i = 0; i < 20; ++i)
                        Friends[i] = Names[i];

                    // Fill in the user's code
                    " + playerText + @"

                    // Put the user's answer in the completionBox
                    completionBox.text = " + userVariable + @".ToString();

                    // Check for the correct answer
                    if (completionBox.text == ""16"")
                    {
                        soundBox.text = ""Correct"";
                        GameObject.FindWithTag(""LevelChange"").GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        soundBox.text = ""Incorrect"";
                    }

                    // Required for coroutines to have a return
                    yield return null; 

                    // Remove the added script from the object
                    Destroy(gameObject.GetComponent<RuntimeScript>());
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

            // Start a co-routine to start looping through the friend Sprites
            StartCoroutine(FindFriends());

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
            if (result.Errors.HasErrors)
            {
                displayLog = false;
                programOutput.text = ""; // Clear the current output box

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
         * This function will check that the player declared the proper variables
         * and that the player used a switch statement for this level.
         * Once all checks are complete, this code is passed to the MainDriver();
         */
        private string InputModification(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;
            int startIndex, endIndex;

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
            if (!newCode.Contains("Friends"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must declare and use the array variable Friends.";
                throw new Exception("Sorry, but you must declare and use the array variable Friends.");
            }

            // Test that the user defined an array of size 20.
            if (!newCode.Contains("[20]"))
            {
                programOutput.text = "Sorry, but you must declare a string array of size 20 for this level.";
                throw new Exception("Sorry, but you must declare a string array of size 20 for this level.");
            }

            // The user should output the array index
            if (!newCode.Contains("System.Console.WriteLine"))
            {
                programOutput.text = "Sorry, but you must display the index of the found Friend using System.Console.WriteLine()";
                throw new Exception("Sorry, but you must display the index of the found Friend using System.Console.WriteLine()");
            }

            // Update any loops that the user may have been written.
            newCode = ModifyLoops(newCode);

            // Strip out the player's variable declaration section and put it in the declarations variable
            startIndex = newCode.IndexOf("string"); // Returns first index of string
            endIndex = newCode.IndexOf(';', startIndex) + 1; // End of this statement
            declarations = newCode[startIndex..endIndex];

            // Remove the declarations section from the player code
            newCode = newCode.Remove(startIndex, (endIndex - startIndex));

            // Find the System.Console.WriteLine() and the variable name the user used
            startIndex = newCode.IndexOf("System.Console.WriteLine");
            startIndex = newCode.IndexOf('(', startIndex) + 1; // Get the index of the left parenthesis
            endIndex = newCode.IndexOf(')', startIndex); // Get the index of the right parenthesis
            userVariable = newCode[startIndex..endIndex]; // Extract the variable name from the parenthesis

            return newCode;
        }

        /*
         * This function, ModifyLoops, checks the input string for any form of loop.
         * If the string contains a loop, it updates it so that the loop will
         * "yield return null". This function will return the new string.
         */
        private string ModifyLoops(string oldString)
        {
            // Create a string for the return
            string modifiedString = oldString;
            int startIndex, startCurlyBrace, endCurlyBrace;

            // If the player wrote a for-loop
            if (modifiedString.Contains("for"))
            {
                // Append on the line after for -> yield return null
                // Step 1: Find the starting index of the for-loop
                startIndex = modifiedString.IndexOf("for");

                // Step 2: Determine if the player wrote a curly brace (multi-line for-loop)
                startCurlyBrace = modifiedString.IndexOf('{', startIndex);
                if (startCurlyBrace != -1)
                {
                    // Step 3: Insert "yield return null" after the startCurlyBrace location
                    modifiedString = modifiedString.Insert(startCurlyBrace + 1, "yield return null;");
                }
                else // Single lined for-loop
                {
                    // Step 4: Add in curly braces and the "yield return null" code
                    // Left curlyBrace and the added code
                    startCurlyBrace = modifiedString.IndexOf(')', startIndex) + 1;
                    modifiedString = modifiedString.Insert(startCurlyBrace, "{yield return null;");

                    // Get an index for the end of this line
                    startIndex = modifiedString.IndexOf(';', startCurlyBrace) + 1;

                    // Get an index for the next semicolon (Single-lined for-loop, this is where to insert '}')
                    endCurlyBrace = modifiedString.IndexOf(';', startIndex) + 1;

                    // Insert the ending curly brace
                    modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                }
            }

            // If the player wrote a do-while loop
            if (modifiedString.Contains("do"))
            {
                // Append on the line after do -> yield return null
                // Step 1: Find the starting index of the do-while loop
                startIndex = modifiedString.IndexOf("do");

                // Step 2: Determine if the player wrote a curly brace (multi-line do-while loop)
                startCurlyBrace = modifiedString.IndexOf('{', startIndex);
                if (startCurlyBrace != -1)
                {
                    // Step 3: Insert "yield return null" after the startCurlyBrace location
                    modifiedString = modifiedString.Insert(startCurlyBrace + 1, "yield return null;");
                }
                else // Single lined do-while loop
                {
                    // Step 4: Add in curly braces and the "yield return null" code
                    // Left curlyBrace and the added code
                    modifiedString = modifiedString.Insert(startIndex + 2, "{yield return null;");

                    // Get an index for while part of the loop
                    endCurlyBrace = modifiedString.IndexOf("while", startIndex);

                    // Insert the ending curly brace before the while
                    modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                }
            }
            else
            {
                // If the player wrote a while-loop
                if (modifiedString.Contains("while"))
                {
                    // Append on the line after while -> yield return null
                    // Step 1: Find the starting index of the while-loop
                    startIndex = modifiedString.IndexOf("while");

                    // Step 2: Determine if the player wrote a curly brace (multi-line while-loop)
                    startCurlyBrace = modifiedString.IndexOf('{', startIndex);
                    if (startCurlyBrace != -1)
                    {
                        // Step 3: Insert "yield return null" after the startCurlyBrace location
                        modifiedString = modifiedString.Insert(startCurlyBrace + 1, "yield return null;");
                    }
                    else // Single lined while-loop
                    {
                        // Step 4: Add in curly braces and the "yield return null" code
                        // Left curlyBrace and the added code
                        startCurlyBrace = modifiedString.IndexOf(')', startIndex) + 1;
                        modifiedString = modifiedString.Insert(startCurlyBrace, "{yield return null;");

                        // Get an index for the end of this line
                        startIndex = modifiedString.IndexOf(';', startCurlyBrace) + 1;

                        // Get an index for the next semicolon (Single-lined while-loop, this is where to insert '}')
                        endCurlyBrace = modifiedString.IndexOf(';', startIndex) + 1;

                        // Insert the ending curly brace
                        modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                    }
                }
            }

            return modifiedString;
        }

        // This function, FindFriends, will loop through 20 various Sprites
        // This function will check the answerBox until an answer is found.
        // Then, this function will either show the correct or incorrect
        // Sprite as Bit's best friend
        private IEnumerator FindFriends()
        {
            int SpriteNumber = 0;
            int Number = UnityEngine.Random.Range(25, 101);

            // Loop through the Sprites until Number
            for (int i = 0; i < Number; ++i)
            {
                // Change the Sprite
                BitsFriend.GetComponent<SpriteRenderer>().sprite = Friends[SpriteNumber];
                SpriteNumber += 1;

                // If the SpriteNumber hits 20, reset it back to 0
                if (SpriteNumber == 20)
                    SpriteNumber = 0;

                // Yield statement for swapping Sprites
                yield return new WaitForSeconds(0.1f);
            }

            // Check the answer box to see if the user got the correct index
            switch (answerCheck.text)
            {
                case "16":
                    programOutput.text = "16\n\nYou found Jeffries best Friend Bit!! Amazing Job.";
                    BitsFriend.GetComponent<SpriteRenderer>().sprite = Friends[16];
                    break;

                case "Error":
                    BitsFriend.GetComponent<SpriteRenderer>().sprite = Friends[15];
                    break;

                default:
                    programOutput.text = answerCheck.text + "\n\nWhat are you doing Bit?\nJeffry is allergic to Cats, fix the Code...\nFix the Code!!";
                    BitsFriend.GetComponent<SpriteRenderer>().sprite = Friends[6];
                    break;
            }
        }

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