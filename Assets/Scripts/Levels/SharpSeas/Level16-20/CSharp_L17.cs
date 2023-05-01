/*
 * This class uses the MonoCSharp Code Compiler.
 * This class will be run to allow the player to
 * learn about pointers in C#.
 * 
 * Date: 04/30/2023
 * Author: Robot and I Team
 */

using UnityEngine;
using Cinemachine;
using Modified.Mono.CSharp;
using System;
using System.Collections;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;
using GameMechanics;

namespace CSharpLevels
{
    public class CSharp_L17 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI soundCheck;
        public TextMeshProUGUI answerCheck;
        public TMP_InputField playerInput;
        public GameObject Bit;
        public CinemachineVirtualCamera Camera;

        // Private variables
        private const int ExecutionTime = 6; // Time in Seconds before the script gets killed
        private bool displayLog;

        // Use update to check the soundCheck input box
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

            if (answerCheck.text != "")
            {
                StartCoroutine(PreviewHouse(Int32.Parse(answerCheck.text)));
                answerCheck.text = "";
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
                    completionBox = GameObject.Find(""AnswerCheck"").GetComponent<TMP_Text>();
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
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, Main, contains whatever the
                // player wrote from the input box.
                private IEnumerator Main()
                {
                    // The problem statement is already displayed. Define an array of houses
                    string[] Houses = new string[]
                    {
                        ""Straw House:\r\nArea is full of trash. Mutated chickens roam and live happily in the area cornering unsuspecting visitors.\r\n"",
                        ""Cobblestone House:\r\nHouse is underwater. Fish, crabs and turtles rule the sea and visitors are not welcome.\r\n"",
                        ""Wood House:\r\nFriends and family welcome all who visit. Area is clean and perfect living conditions.\r\n"",
                        ""Brick House:\r\nDeep in the jungle, amidst the snakes, mosquitoes and flies lies this house. Enter at your own risk."",
                    };
                    bool foundAddress = false;

                    // Define an indexer and 2D array to simulate pointers
                    int TheIndexer = 0, RandRow = UnityEngine.Random.Range(0, 5);
                    string[ , ] SystemAddresses = new string[5, 4]
                    {
                        { ""0x6ffdd0"", ""0x6ffdd4"", ""0x6ffdd8"", ""0x6ffddc"" },
                        { ""0xab9e48"", ""0xab9e4c"", ""0xab9e50"", ""0xab9e54"" },
                        { ""0xcdcdcc"", ""0xcdcdd0"", ""0xcdcdd4"", ""0xcdcdd8"" },
                        { ""0x174a64"", ""0x174a68"", ""0x174a6c"", ""0x174a70"" },
                        { ""0xfa25d0"", ""0xfa25d4"", ""0xfa25d8"", ""0xfa25dc"" },
                    };

                    // Player's code goes here
                    " + playerText + @"

                    // Loop through each index in the array
                    for (int i = 0; !foundAddress && i < 4; ++i)
                    {
                        // If the output text contains the address on a specific index
                        if (output.text.Contains(SystemAddresses[RandRow, i]))
                        {
                            // Set the answer box and end loop
                            completionBox.text = i.ToString();
                            foundAddress = true;

                            // Check for correct answer
                            if (i == 2)
                            {
                                soundBox.text = ""Correct"";
                                GameObject.FindWithTag(""LevelChange"").GetComponent<BoxCollider2D>().isTrigger = true;
                            }
                            else
                                soundBox.text = ""Incorrect"";
                        }
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

                    //*******************************************************************************
                    // The following Error codes are for specific errors the compiler gives
                    // with regards to using pointers in C#. These error messages will remain
                    // on this level in case the user gets passed my many string manipulation checks.
                    //*******************************************************************************

                    // Managed Type Error. EX. C# will not let you declare a pointer to a string, it is a managed data type
                    "CS0208" => "Error: Bit, you have somehow managed to write code I do not understand.\n\n" +
                                "Please try to update your code to something that is simpler and follows the " +
                                "code shown during the lesson dialogue.",

                    // Unsafe Context Only Error. EX. C# code that uses pointers must have an "unsafe" block around it
                    "CS0214" => "Error: Bit, you have somehow managed to write code I do not understand.\n\n" +
                                "Please try to update your code to something that is simpler and follows the " +
                                "code shown during the lesson dialogue.",

                    // Unsafe Compilation Option Error. EX. C# code can only run pointers when the "unsafe" option is used in the build parameters
                    "CS0227" => "Error: Bit, you have somehow managed to write code I do not understand.\n\n" +
                                "Please try to update your code to something that is simpler and follows the " +
                                "code shown during the lesson dialogue.",

                    // Unsafe Code in Iterator Error. EX. C# cannot run unsafe code inside a coroutine (IEnumerator return)
                    "CS1629" => "Error: Bit, you have somehow managed to write code I do not understand.\n\n" +
                                "Please try to update your code to something that is simpler and follows the " +
                                "code shown during the lesson dialogue.",

                    // All Other Errors
                    _ => $"Line: {error[0].Line}\n\nError: {error[0].ErrorText}",
                };
            }

            // Return the assembly
            return result.CompiledAssembly;
        }

        /*
         * This function, InputModification, checks and modifies the player's input.
         * This function will attempt to strip out the player's code and replace it
         * with other predefined code that will imitate a pointer.
         * Once all checks are complete, this code is passed to the MainDriver();
         */
        private string InputModification(string playerCode)
        {
            // Create a string for the return, define string manipulation variables
            string newCode = playerCode, copy, temp, userVariable;
            int startIndex, endIndex, startCurlyBrace, endCurlyBrace;

            // Check for malicious code
            if (newCode.Contains("GameObject") || newCode.Contains("sleep"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // The player must declare their pointer with the asterisk either on the datatype or variable name.
            if (!newCode.Contains("string*") && !newCode.Contains("string *"))
            {
                programOutput.text = "Bit, please declare your pointer using:\r\nstring* VariableName || string *VariableName";
                throw new Exception("Bit, please declare your pointer using:\r\nstring* VariableName || string *VariableName");
            }

            // Get a temp variable of the declaration line of the pointer
            startIndex = newCode.IndexOf("string*");
            if (startIndex == -1)
                startIndex = newCode.IndexOf("string *");
            endIndex = newCode.IndexOf(";", startIndex) + 1;
            temp = newCode[startIndex..endIndex];
            copy = temp; // Copy the substring into copy

            // Determine if the player used &House or simply House for address assignment
            if (copy.Contains("&"))
                copy = copy.Replace("&", ""); // Replace the ampersand
            copy = copy.Replace("*", "[]"); // Replace the asterisk

            // Swap out the pointer declaration with the array assignment
            newCode = newCode.Replace(temp, copy);

            // Determine the variable name the player used
            startIndex = copy.IndexOf("[]") + 2;
            if (!copy.Contains("=")) // User defined and initialized variable on different lines
                endIndex = copy.IndexOf(";");
            else
                endIndex = copy.IndexOf("="); // User defined and initialized variable on same line
            userVariable = newCode[startIndex..endIndex];
            userVariable = userVariable.Trim(); // Trim off the whitespace from the variable name

            // The player cannot increment the pointer in any other notation than ++
            if (newCode.Contains(userVariable + "+=") || newCode.Contains(userVariable + " +=") ||
                newCode.Contains("= " + userVariable) || newCode.Contains("=" + userVariable))
            {
                programOutput.text = "Bit, why be different? Please increment your pointer using:\r\n++VariableName || VariableName++";
                throw new Exception("Bit, why be different? Please increment your pointer using:\r\n++VariableName || VariableName++");
            }

            // The player cannot use &pointer
            if (newCode.Contains("&" + userVariable))
            {
                programOutput.text = "Bit, why be different? Please display the address through:\r\nVariableName || &Houses[index]";
                throw new Exception("Bit, why be different? Please display the address through:\r\nVariableName || &Houses[index]");
            }

            // Replace dereference of the pointer with array notation and a pre-defined variable indexer
            while (newCode.Contains("*" + userVariable))
            {
                // Get a temp variable of the line where the asterisk occurred
                startIndex = newCode.IndexOf("*" + userVariable);
                endIndex = newCode.IndexOf(";", startIndex) + 1;
                temp = newCode[startIndex..endIndex];
                copy = temp; // Copy the substring into copy

                // Remove the asterisk and replace the variable name with array notation
                copy = copy.Replace("*", "");
                copy = copy.Replace(userVariable, userVariable + "[TheIndexer]");

                // Swap out the pointer notation with array notation
                newCode = newCode.Replace(temp, copy);
            }

            // Replace the address referencing of the pointer with an array of predefined addresses
            while (newCode.Contains("System.Console.WriteLine(" + userVariable + ")"))
            {
                // Get a temp variable of the line where the display occured
                startIndex = newCode.IndexOf("System.Console.WriteLine(" + userVariable + ")");
                endIndex = newCode.IndexOf(";", startIndex) + 1;
                temp = newCode[startIndex..endIndex];
                copy = temp; // Copy the substring into copy

                // Replace the variable name with predefined addressing in array notation
                copy = copy.Replace(userVariable, "SystemAddresses[RandRow, TheIndexer]");

                // Swap out the code in the original string
                newCode = newCode.Replace(temp, copy);
            }

            // Replace the address referencing of the array with an array of predefined addresses
            while (newCode.Contains("System.Console.WriteLine(&Houses["))
            {
                // Get a temp variable of the line where the display occured
                startIndex = newCode.IndexOf("System.Console.WriteLine(&Houses[");
                endIndex = newCode.IndexOf(";", startIndex) + 1;
                temp = newCode[startIndex..endIndex];
                copy = temp; // Copy the substring into copy

                // Get the number the player used inside of the brackets
                startCurlyBrace = copy.IndexOf("[") + 2;
                endCurlyBrace = copy.IndexOf("]");

                // Replace the &Houses[X] with SystemAddresses[RandRow, X]
                copy = copy.Replace("&Houses[", "SystemAddresses[RandRow, " + copy[startCurlyBrace..endCurlyBrace]);

                // Swap out the code in the original string
                newCode = newCode.Replace(temp, copy);
                break;
            }

            // Replace the System.Console.WriteLine statement with output.text =
            while (newCode.Contains("System.Console.WriteLine"))
            {
                // Get a temp variable of the line where the statement is
                startIndex = newCode.IndexOf("System.Console.WriteLine");
                endIndex = newCode.IndexOf(";", startIndex) + 1;
                temp = newCode[startIndex..endIndex];
                copy = temp; // Copy the substring

                // Get the parenthesis and remove them, then replace the statement
                startCurlyBrace = copy.IndexOf("(");
                endCurlyBrace = copy.IndexOf(")") - 1; // Correct calculation when removing startCurlyBrace
                copy = copy.Remove(startCurlyBrace, 1); // Removes left parenthesis
                copy = copy.Remove(endCurlyBrace, 1); // Removes right parenthesis
                copy = copy.Replace("System.Console.WriteLine", "output.text += "); // Replace the statement

                // Replace the System.Console.WriteLine statement with the output variable
                newCode = newCode.Replace(temp, copy);
            }

            // Replace the pointer incrementation notation to array indexing incrementation
            while (newCode.Contains("++" + userVariable) || newCode.Contains(userVariable + "++"))
            {
                // Get a temp variable of the line where the incrementation is
                startIndex = newCode.IndexOf("++" + userVariable);
                if (startIndex == -1)
                    startIndex = newCode.IndexOf(userVariable + "++");
                endIndex = newCode.IndexOf(";", startIndex);
                temp = newCode[startIndex..endIndex];
                copy = temp; // Copy the substring into copy

                // Replace the variable name with TheIndexer
                copy = copy.Replace(userVariable, "TheIndexer");

                // Swap out copy with the original string
                newCode = newCode.Replace(temp, copy);
            }

            newCode = ModifyLoops(newCode);

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

        // This Coroutine, PreviewHouse, will move bit to
        // the appropriate house given by HouseNum. Then
        // this script will preview the contents of the
        // selected house given by the same variable.
        private IEnumerator PreviewHouse(int HouseNum)
        {
            // Variables to be used in the code
            Vector3 bitLocation = Bit.transform.position;
            Vector3 cameraLocation = Camera.transform.position;
            int[] houseCenter = new int[] { 130, 237, 332, 436 };
            Vector3[] sceneLocationStart = new Vector3[]
            {
                new Vector3(249f, 445f, -15f),
                new Vector3(249f, 145f, -15f),
                new Vector3(1242f, 150f, -15f),
                new Vector3(1242f, 445f, -15f)
            };
            int[] sceneLocationEnd = new int[] { 758, 758, 1679, 1689 };

            // Suck Bit into the center of the chosen house
            while (Bit.transform.position.x < (houseCenter[HouseNum] - 1) || Bit.transform.position.x > (houseCenter[HouseNum] + 1))
            {
                // If Bit is less than location
                if (Bit.transform.position.x < houseCenter[HouseNum])
                    Bit.transform.Translate(1f, 0, 0);
                else // Bit is greater than location
                    Bit.transform.Translate(-1f, 0, 0);

                // Sucking effect
                yield return new WaitForSeconds(0.029f);
            }

            // Move the Camera to the correct scene
            Camera.GetComponent<CinemachineConfiner2D>().enabled = false;
            Camera.transform.position = sceneLocationStart[HouseNum];

            // Move the Camera forward until hit location end
            while (Camera.transform.position.x < sceneLocationEnd[HouseNum])
            {
                // Move camera to the right
                Camera.transform.Translate(1f, 0, 0);

                // Keep the camera slow
                yield return new WaitForSeconds(0.039f);
            }

            // Wait 3 seconds before returning everything back to normal
            yield return new WaitForSecondsRealtime(3f);
            Bit.transform.position = bitLocation;
            Camera.transform.position = cameraLocation;
            Camera.GetComponent<CinemachineConfiner2D>().enabled = true;

            // Set the output box
            switch (HouseNum)
            {
                case 2:
                    programOutput.text = "Bit, you have played and completed every level in the game. " +
                                        "You have surpassed all my expectations and I am truly proud of you.\r\n" +
                                        "Bit, our journey sadly ends here. I hope you continue to expound upon your learning, " +
                                        "there is still a lot I have not taught you.\r\nThe people of Bittonia than you " +
                                        "for helping them in their many problems. They hope you come back to levels you enjoyed and " +
                                        "continue to learn things you may have missed.\r\nUntil next time :) ~ The Duke";
                    break;

                case 0:
                case 1:
                case 3:
                    programOutput.text = "How was the house hunting trip Bit?\r\nIt was awful you say?\r\nI am sorry to hear that.\r\n" +
                                        "I don't think Dexter would enjoy that house either. You should try another House.";
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