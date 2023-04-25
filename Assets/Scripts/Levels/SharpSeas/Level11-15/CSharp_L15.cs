/*
 * This class uses the MonoCSharp Code Compiler.
 * This class will be run to allow the player to
 * learn about 2D arrays in C#.
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
    public class CSharp_L15 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI soundCheck;
        public TextMeshProUGUI answerCheck;
        public TMP_InputField playerInput;
        public List<GameObject> Books;

        // Private variables
        private string declarations;
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
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, Main, contains whatever the
                // player wrote from the input box.
                private IEnumerator Main()
                {
                    // The problem statement is already displayed. Define a 2D array of Books
                    string[ , ] Titles = new string[3, 5]
                    {
                        { ""Charles Dickens"", ""Oliver Twist"", ""Trumpet of the Swan"", ""Prince and the Pauper"", ""Sherlock Holmes"" },
                        { ""Nancy Drew"", ""Mouse and the Motorcycle"", ""Secrets of Droon"", ""Favorite Book"", ""Hardey Boys"" },
                        { ""Hamlet"", ""C++ Control Structures Through Objects"", ""Hansel & Gretel"", ""Tom Sawyer"", ""Moby Dick"" }
                    };

                    // The player should define a 2D array named Books
                    " + declarations + @"

                    // Fill the 2D array with data
                    for (int rows = 0; rows < 3; ++rows)
                    {
                        for (int columns = 0; columns < 5; ++columns)
                        {
                            Books[rows, columns] = Titles[rows, columns];
                        }
                    }

                    // Fill in the user's code
                    " + playerText + @"

                    // Check for the correct answer
                    if (location[0] == 1 && location[1] == 3)
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
            StartCoroutine(FindBook());

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
            if (!newCode.Contains("Books") || !newCode.Contains("location"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must declare and use the 2D and 1D arrays \"Books\" & \"location\".";
                throw new Exception("Sorry, but you must declare and use the 2D and 1D arrays \"Books\" & \"location\".");
            }

            // Test that the user defined the two arrays of proper size.
            if (!(newCode.Contains("[3, 5]") || newCode.Contains("[3,5]")) || !newCode.Contains("[2]"))
            {
                programOutput.text = "Sorry, but you must declare a 2D string array of size 3x5 and a 1D string array of size 2.";
                throw new Exception("Sorry, but you must declare a 2D string array of size 3x5 and a 1D string array of size 2.");
            }

            // Inform the user that this level is restricted to for-loops only
            if (newCode.Contains("while"))
            {
                programOutput.text = "Bit, I recommend and insist that you do not try to use a while/do-while loop for this level.";
                throw new Exception("Bit, I recommend and insist that you do no try to use a while/do-while loop for this level.");
            }

            // Update any loops that the user may have been written.
            newCode = ModifyLoops(newCode);

            // Strip out the player's variable declaration section and put it in the declarations variable
            startIndex = newCode.IndexOf("string"); // Returns first index of string
            endIndex = newCode.IndexOf("new", startIndex) + 3; // Returns first index of new (should be two instances of new)
            endIndex = newCode.IndexOf("new", endIndex); // Returns second index of new
            endIndex = newCode.IndexOf(';', endIndex) + 1; // Get the index of the end of this line
            declarations = newCode[startIndex..endIndex];

            // Remove the declarations section from the player code
            newCode = newCode.Remove(startIndex, (endIndex - startIndex));

            return newCode;
        }

        /*
         * This function, ModifyLoops, checks the input string for for-loops. Both singe and nested for-loops.
         * If the string contains a for-loop, it updates it so that that loop will have a "yield return null" in it.
         * This function will return the new string when finished.
         */
        private string ModifyLoops(string oldString)
        {
            // Create a string for the return
            string modifiedString = oldString;
            int startIndexOuter, startIndexInner, startCurlyBrace, endCurlyBrace;

            // Determine if the user wrote a for-loop
            startIndexOuter = modifiedString.IndexOf("for");
            if (startIndexOuter != -1)
            {
                // Determine if the user nested a for-loop
                startIndexInner = modifiedString.IndexOf("for", startIndexOuter + 3);
                if (startIndexInner != 1)
                {
                    // Determine if the player wrote a curly brace (multi-line for-loop)
                    startCurlyBrace = modifiedString.IndexOf('{', startIndexInner);
                    if (startCurlyBrace != -1)
                    {
                        // Insert "yield return null" after the startCurlyBrace location
                        modifiedString = modifiedString.Insert(startCurlyBrace + 1, "yield return null;");
                    }
                    else // Single lined for-loop
                    {
                        // Add in curly braces and the "yield return null" code
                        // Left curlyBrace and the added code
                        startCurlyBrace = modifiedString.IndexOf(')', startIndexInner) + 1;
                        modifiedString = modifiedString.Insert(startCurlyBrace, "{yield return null;");

                        // Get an index for the end of this line
                        startIndexInner = modifiedString.IndexOf(';', startCurlyBrace) + 1;

                        // Get an index for the next semicolon (Single-lined for-loop, this is where to insert '}')
                        endCurlyBrace = modifiedString.IndexOf(';', startIndexInner) + 1;

                        // Insert the ending curly brace
                        modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                    }
                }
                else
                {
                    // Determine if the player wrote a curly brace (multi-line for-loop)
                    startCurlyBrace = modifiedString.IndexOf('{', startIndexOuter);
                    if (startCurlyBrace != -1)
                    {
                        // Insert "yield return null" after the startCurlyBrace location
                        modifiedString = modifiedString.Insert(startCurlyBrace + 1, "yield return null;");
                    }
                    else // Single lined for-loop
                    {
                        // Add in curly braces and the "yield return null" code
                        // Left curlyBrace and the added code
                        startCurlyBrace = modifiedString.IndexOf(')', startIndexOuter) + 1;
                        modifiedString = modifiedString.Insert(startCurlyBrace, "{yield return null;");

                        // Get an index for the end of this line
                        startIndexOuter = modifiedString.IndexOf(';', startCurlyBrace) + 1;

                        // Get an index for the next semicolon (Single-lined for-loop, this is where to insert '}')
                        endCurlyBrace = modifiedString.IndexOf(';', startIndexOuter) + 1;

                        // Insert the ending curly brace
                        modifiedString = modifiedString.Insert(endCurlyBrace, "}");
                    }
                }
            }

            return modifiedString;
        }

        // This function, FindBook, will create a display of books
        // twisting and scattering around the screen. 
        private IEnumerator FindBook()
        {
            // Get the current position of each Book
            Vector3 book1 = Books[1].transform.position;
            Vector3 book2 = Books[2].transform.position;
            Vector3 book3 = Books[3].transform.position;
            Vector3 book4 = Books[4].transform.position;
            Vector3 book5 = Books[5].transform.position;
            
            // Move the books up like they are levatating
            for (int i = 0; i < 100; ++i)
            {
                Books[0].transform.Translate(0, 1f, 0);
                yield return new WaitForSeconds(0.01f);
            }

            // Spin the books in circles majestically
            for (int i = 0; i < 360; ++i)
            {
                Books[0].transform.Rotate(0, 10f, 0);
                yield return new WaitForSeconds(0.007f);
            }

            // Check the answer box to see if the user got the correct indices
            switch (answerCheck.text)
            {
                case "Correct":
                    // Set all the Books but the middle one to inactive.
                    yield return new WaitForSecondsRealtime(1f);
                    Books[4].SetActive(false);
                    yield return new WaitForSecondsRealtime(1f);
                    Books[5].SetActive(false);
                    yield return new WaitForSecondsRealtime(1f);
                    Books[2].SetActive(false);
                    yield return new WaitForSecondsRealtime(1f);
                    Books[3].SetActive(false);
                    yield return new WaitForSecondsRealtime(1f);

                    // Levitate the Books back down
                    for (int i = 0; i < 100; ++i)
                    {
                        Books[0].transform.Translate(0, -1f, 0);
                        yield return new WaitForSeconds(0.007f);
                    }

                    programOutput.text = "Thats it!! Thats Shelby's favorite book Bit. You are getting really good at this Bit.";
                    break;

                case "Incorrect":
                    // Make the Books "Explode"
                    yield return new WaitForSecondsRealtime(1f);
                    for (int i = 0; i < 150; ++i)
                    {
                        Books[4].transform.Translate(-5f, 0, 0);
                        yield return null;
                    }
                    for (int i = 0; i < 150; ++i)
                    {
                        Books[5].transform.Translate(5f, 0, 0);
                        yield return null;
                    }
                    for (int i = 0; i < 150; ++i)
                    {
                        Books[2].transform.Translate(0, 5f, 0);
                        yield return null;
                    }
                    for (int i = 0; i < 150; ++i)
                    {
                        Books[3].transform.Translate(0, -5f, 0);
                        yield return null;
                    }
                    Books[1].SetActive(false);
                    programOutput.text = "That can't be right Bit. I am sure Shelby's favorite book is not \"Python for Dummies\".\r\nCheck your code. You must've made an error somewhere.";
                    break;
            }

            // Wait a few seconds for dramatic effect and then reset everything.
            yield return new WaitForSecondsRealtime(2f);
            Books[1].transform.position = book1;
            Books[2].transform.position = book2;
            Books[3].transform.position = book3;
            Books[4].transform.position = book4;
            Books[5].transform.position = book5;
            Books[1].SetActive(true);
            Books[2].SetActive(true);
            Books[3].SetActive(true);
            Books[4].SetActive(true);
            Books[5].SetActive(true);
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