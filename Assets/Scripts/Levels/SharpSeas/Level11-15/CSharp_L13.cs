/*
 * This script allows the player the ability to use the
 * CSharp compiler. This script will allow the player to
 * compute an arithmetic table of values.
 * 
 * Author: Robot and I Team
 * Last modification date: 04/14/2023
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;
using GameMechanics;

namespace CSharpLevels
{
    public class CSharp_L13 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI soundCheck;
        public TMP_InputField playerInput;
        public GameObject multiplicationTable, errorOutput;

        // Private variables
        private string declarations;
        private bool displayLog;
        private const int ExecutionTime = 6; // Time in Seconds before the script gets killed

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
                private TMP_Text output, soundBox;
                private GameObject outputBox, NPC;

                // Define 5 arrays of size 5 to hold each TMP_Text output box
                private TMP_Text[] AddOp = new TMP_Text[5];
                private TMP_Text[] SubOp = new TMP_Text[5];
                private TMP_Text[] MultOp = new TMP_Text[5];
                private TMP_Text[] DivOp = new TMP_Text[5];
                private TMP_Text[] ModOp = new TMP_Text[5];

                // Required for addition to Runtime Script
                public static RuntimeScript AddYourselfTo(GameObject host)
                {
                    // Add RuntimeScript to the host object
                    return host.AddComponent<RuntimeScript>();
                }

                // When the script is added to the HostGameObject and invoked, run the following code
                void Start()
                {
                    // Must call GameObject.Find from within start
                    outputBox = GameObject.Find(""Error Output"");
                    output = outputBox.GetComponent<TMP_Text>();
                    soundBox = GameObject.Find(""SoundCheckBox"").GetComponent<TMP_Text>();
                    NPC = GameObject.FindWithTag(""LevelChange"");
                    Color32 temp = new Color32(255, 255, 255, 255); // Get a temp color for initial set

                    // Find and set each Operation box
                    for (int i = 1; i <= 5; ++i)
                    {
                        // Get a reference to the text box
                        AddOp[i - 1] = GameObject.Find(""Addition"" + i.ToString()).GetComponent<TMP_Text>();
                        SubOp[i - 1] = GameObject.Find(""Subtraction"" + i.ToString()).GetComponent<TMP_Text>();
                        MultOp[i - 1] = GameObject.Find(""Multiplication"" + i.ToString()).GetComponent<TMP_Text>();
                        DivOp[i - 1] = GameObject.Find(""Division"" + i.ToString()).GetComponent<TMP_Text>();
                        ModOp[i - 1] = GameObject.Find(""Modulus"" + i.ToString()).GetComponent<TMP_Text>();

                        // Set the color of the text box
                        AddOp[i - 1].color = temp;
                        SubOp[i - 1].color = temp;
                        MultOp[i - 1].color = temp;
                        DivOp[i - 1].color = temp;
                        ModOp[i - 1].color = temp;
                    }

                    // Set the Titles to blank also
                    GameObject.Find(""Addition"").GetComponent<TMP_Text>().color = temp;
                    GameObject.Find(""Subtraction"").GetComponent<TMP_Text>().color = temp;
                    GameObject.Find(""Multiplication"").GetComponent<TMP_Text>().color = temp;
                    GameObject.Find(""Division"").GetComponent<TMP_Text>().color = temp;
                    GameObject.Find(""Modulus"").GetComponent<TMP_Text>().color = temp;

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
                    int IndexCounter = 0;
                    double CorrectAnswer = 0;
                    string Operation = """";
                    bool FailedTest = false;
                    Color32 IncorrectColor = new Color32(255, 0, 0, 255); // RGBA Format
                    " + declarations + @"

                    // Assign a random number for 'Number' between -99 and +99
                    Number = UnityEngine.Random.Range(-99f, 99f);

                    // Updated string of player text
                    " + playerText + @"

                    // Check the error code
                    if (!FailedTest)
                    {
                        output.text = ""Bit, you certainly know your \""bits\"" well. You have done amazing work today"";
                        soundBox.text = ""Correct"";
                        NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        if (Operation == ""for-loop"")
                        {
                            output.text = ""Oh no Bit. Your for-loop ran past five. Your output may be right, "" +
                                  ""but we cannot let you proceed until your loop executes properly"";
                        }
                        else
                            output.text = ""It looks like your arithmetic needs work. Your "" + Operation + "" column is incorrect Bit."";
                        soundBox.text = ""Incorrect"";
                    }

                    // Set the output box to inactive for focus on the multiplicationTable
                    outputBox.SetActive(false);

                    // Required for coroutines to have a return
                    // Waits half a second, then will destroy the game object.
                    yield return new WaitForSeconds(0.5f); 

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
         * and that the player used a for-loop for this level. This function will
         * also add in extra code to the user's input that will allow the level
         * to work properly.
         * Once all checks/additions are complete, this code is passed to the MainDriver();
         */
        private string InputModification(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;
            int startIndex, startCurlyBrace, endIndex, endCurlyBrace;

            // The additionalCode adds in checks for correct and incorrect arithmetic
            // The ErrorNumber is set to the check that failed for display
            // The additionalCode also adds in pretty print of the numbers
            string additionalCode = @"
            CorrectAnswer = Number + (Number + IndexCounter);
            if (!FailedTest && CorrectAnswer != Addition)
            {
                FailedTest = true;
                Operation = ""Addition"";
                AddOp[0].color = IncorrectColor;
                AddOp[1].color = IncorrectColor;
                AddOp[2].color = IncorrectColor;
                AddOp[3].color = IncorrectColor;
                AddOp[4].color = IncorrectColor;
            }
            CorrectAnswer = Number - (Number + IndexCounter);
            if (!FailedTest && CorrectAnswer != Subtraction)
            {
                FailedTest = true;
                Operation = ""Subtraction"";
                SubOp[0].color = IncorrectColor;
                SubOp[1].color = IncorrectColor;
                SubOp[2].color = IncorrectColor;
                SubOp[3].color = IncorrectColor;
                SubOp[4].color = IncorrectColor;
            }
            CorrectAnswer = Number * (Number + IndexCounter);
            if (!FailedTest && CorrectAnswer != Multiplication)
            {
                FailedTest = true;
                Operation = ""Multiplication"";
                MultOp[0].color = IncorrectColor;
                MultOp[1].color = IncorrectColor;
                MultOp[2].color = IncorrectColor;
                MultOp[3].color = IncorrectColor;
                MultOp[4].color = IncorrectColor;
            }
            CorrectAnswer = Number / (Number + IndexCounter);
            if (!FailedTest && CorrectAnswer != Division)
            {
                FailedTest = true;
                Operation = ""Division"";
                DivOp[0].color = IncorrectColor;
                DivOp[1].color = IncorrectColor;
                DivOp[2].color = IncorrectColor;
                DivOp[3].color = IncorrectColor;
                DivOp[4].color = IncorrectColor;
            }
            CorrectAnswer = Number % (Number + IndexCounter);
            if (!FailedTest && CorrectAnswer != Modulus)
            {
                FailedTest = true;
                Operation = ""Modulus"";
                ModOp[0].color = IncorrectColor;
                ModOp[1].color = IncorrectColor;
                ModOp[2].color = IncorrectColor;
                ModOp[3].color = IncorrectColor;
                ModOp[4].color = IncorrectColor;
            }
            if (IndexCounter > 4)
            {
                if (!FailedTest)
                {
                    FailedTest = true;
                    Operation = ""for-loop"";
                    GameObject.Find(""Addition"").GetComponent<TMP_Text>().color = IncorrectColor;
                    GameObject.Find(""Subtraction"").GetComponent<TMP_Text>().color = IncorrectColor;
                    GameObject.Find(""Multiplication"").GetComponent<TMP_Text>().color = IncorrectColor;
                    GameObject.Find(""Division"").GetComponent<TMP_Text>().color = IncorrectColor;
                    GameObject.Find(""Modulus"").GetComponent<TMP_Text>().color = IncorrectColor;
                }
            }
            else
            {
                AddOp[IndexCounter].text = String.Format(""{0:0.##}"", Addition);
                yield return new WaitForSeconds(0.1f);
                SubOp[IndexCounter].text = String.Format(""{0:0.##}"", Subtraction);
                yield return new WaitForSeconds(0.1f);
                MultOp[IndexCounter].text = String.Format(""{0:0.##}"", Multiplication);
                yield return new WaitForSeconds(0.1f);
                DivOp[IndexCounter].text = String.Format(""{0:0.##}"", Division);
                yield return new WaitForSeconds(0.1f);
                ModOp[IndexCounter].text = String.Format(""{0:0.##}"", Modulus);
                yield return new WaitForSeconds(0.1f);
                IndexCounter++;
            }";
            
            // Check for malicious code
            if (newCode.Contains("GameObject") || newCode.Contains("sleep"))
            {
                // Set the multiplicationTable inactive
                multiplicationTable.SetActive(false);

                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // Check for unwanted loops
            if (newCode.Contains("while"))
            {
                // Set the multiplicationTable inactive
                multiplicationTable.SetActive(false);

                // Both are required due to Unity issues with display
                programOutput.text = "You are not allowed to use that kind of loop for this level. Sorry :)";
                throw new Exception("You are not allowed to use that kind of loop for this level. Sorry :)");
            }

            // Check for declared variables
            if (
                !newCode.Contains("Addition") || !newCode.Contains("Subtraction") ||
                !newCode.Contains("Multiplication") || !newCode.Contains("Division") ||
                !newCode.Contains("Modulus") || !newCode.Contains("Number"))
            {
                // Set the multiplicationTable inactive
                multiplicationTable.SetActive(false);

                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must declare and use the variables Number, Addition, Subtraction, Multiplication, Division and Modulus.";
                throw new Exception("Sorry, but you must declare and use the variables Number, Addition, Subtraction, Multiplication, Division and Modulus.");
            }

            // Test that the user used a for-loop.
            if (!newCode.Contains("for"))
            {
                // Set the multiplicationTable inactive
                multiplicationTable.SetActive(false);

                programOutput.text = "Sorry, but you must use a for-loop for this level.";
                throw new Exception("Sorry, but you must use a for-loop for this level.");
            }

            // Test that the user is printing each of the variables
            if (
                !newCode.Contains("System.Console.WriteLine(Addition)") || !newCode.Contains("System.Console.WriteLine(Subtraction)") ||
                !newCode.Contains("System.Console.WriteLine(Multiplication)") || !newCode.Contains("System.Console.WriteLine(Division)") ||
                !newCode.Contains("System.Console.WriteLine(Modulus)"))
            {
                // Set the multiplicationTable inactive
                multiplicationTable.SetActive(false);

                programOutput.text = "Sorry, but you must display Addition, Subtraction, Multiplication, Division and Modulus using individual System.Console.WriteLine statements for this level.";
                throw new Exception("Sorry, but you must display Addition, Subtraction, Multiplication, Division and Modulus using individual System.Console.WriteLine statements for this level.");
            }

            // Strip out the player's variable declaration section and put it in the declarations variable
            startIndex = 0; // Start of input text
            endIndex = newCode.IndexOf("for"); // Player Code starts at for
            declarations = newCode[startIndex..endIndex];

            // Remove the declarations section from the player code
            newCode = newCode.Remove(startIndex, (endIndex - startIndex));

            // Put in additionalCode into the player's input string
            // Step 1: Find the starting index of the for-loop
            startIndex = newCode.IndexOf("for");

            // Step 2: Determine if the player wrote a curly brace (multi-line for-loop)
            startCurlyBrace = newCode.IndexOf('{', startIndex);
            if (startCurlyBrace != -1)
            {
                // Step 3: Insert additionalCode before the "endCurlyBrace" location
                endCurlyBrace = newCode.IndexOf('}', startIndex);
                newCode = newCode.Insert(endCurlyBrace, additionalCode);
            }
            else // Single lined for-loop
            {
                // Step 4: Add in curly braces and the additionalCode
                // Left curlyBrace
                startCurlyBrace = newCode.IndexOf(')', startIndex) + 1;
                newCode = newCode.Insert(startCurlyBrace, "{");

                // Get an index for the end of this line
                startIndex = newCode.IndexOf(';', startCurlyBrace) + 1;

                // Get an index for the next semicolon (Single-lined for-loop, this is where to insert '}')
                endCurlyBrace = newCode.IndexOf(';', startIndex) + 1;

                // Insert the ending curly brace and the additionalCode
                newCode = newCode.Insert(endCurlyBrace, additionalCode + "}");
            }

            return newCode;
        }

        // This function, ActivateTextBoxes, sets  both the
        // multiplicationTable and errorOutput text boxes to
        // active for the runtime script to grab
        public void ActivateTextBoxes()
        {
            multiplicationTable.SetActive(true);
            errorOutput.SetActive(true);
        }

        // This function, SwapWindow, changes the active status of the
        // multiplicationTable and errorOutput to the opposite value
        public void SwapWindow()
        {
            // Set the active status to not its current active status
            multiplicationTable.SetActive(!multiplicationTable.activeSelf);
            errorOutput.SetActive(!errorOutput.activeSelf);
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