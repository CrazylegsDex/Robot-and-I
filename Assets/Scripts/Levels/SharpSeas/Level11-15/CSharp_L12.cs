/*
 * This script allows the player the ability to use the
 * CSharp compiler. This script will allow the player to
 * use a prewritten function named "MoveBit" that will
 * move Bit one unit to the right.
 * 
 * Author: Robot and I Team
 * Last modification date: 11-15-2022
 */

using UnityEngine;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;

namespace CSharpLevels
{
    public class CSharp_L12 : MonoBehaviour
    {
        // Public variables
        public TMP_InputField playerInput; // References the Player's Input Field
        public TextMeshProUGUI programOutput; // References the TMP Output Field

        // Private variables
        private bool displayLog;
        private const int ExecutionTime = 6; // Time in Seconds before the script gets killed

        /*
         * This function is the driver to the sequence of events that are
         * required to:
         * Get input from a TextMeshPro box (Update a template code string with this input).
         * Parse/Compile the code written in the box.
         * Check for errors, and notify the player if any.
         * Display the output from the code to a TextMeshPro box if any.
         * If required, act upon a specific object in the level.
         */
        public void MainDriver()
        {
            // Local variables
            Assembly resultAssembly;
            Type runtimeClass;
            MethodInfo runtimeFunction;
            Func<GameObject, MonoBehaviour> runtimeDelegate;

            // Replace all references of MoveBit() to yield return MoveBit()
            string playerText = playerInput.text.Replace("MoveBit()", "yield return MoveBit()");

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
                    print(""It seems your code has run for too long."");
                    print(""Check to ensure that you are not running an infinite loop."");
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
                    yield return new WaitForSeconds(2.0f); 

                    // Remove the added script from the object
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function, MoveBit, is designed to take the current
                // Bit object and move him one unit to the right.
                public IEnumerator MoveBit()
                {
                    // Move bit forward by 1 X-Unit
                    Bit.transform.Translate(1, 0, 0); // X, Y, Z Translation move

                    // Wait a single frame before returning
                    yield return null;
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
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            CompilerResults result;

            /* Add in the .dll files for the compilation to take place
             * 
             * System.dll = System namespace for common types like collections
             * UnityEngine.dll = This contains methods from Unity namespaces
             * Microsoft.CSharp.dll = This assembly contains runtime C# code from your Assets folders
             * netstandard.dll = Other assembly that is required (.NetFramework specific)
             * 
             * NOTE: Path locations may vary based on install. WILL encounter errors on build.
             * Refer to the C# compiler documentation for what to do in this instance.
             */
            if (Application.isEditor)
            {
                string path1 = @"Data\PlaybackEngines\windowsstandalonesupport\Variations\win32_player_development_mono\Data\Managed\";
                string path2 = @"Data\Resources\PackageManager\ProjectTemplates\libcache\com.unity.template.2d-7.0.1\ScriptAssemblies\";
                string assemblyLocation = parameters.ReferencedAssemblies.GetType().Assembly.Location;
                string win32Location = assemblyLocation.Substring(0, assemblyLocation.IndexOf("System.dll")); // Snip off the "System.dll" information
                string engineLocation = assemblyLocation.Substring(0, assemblyLocation.IndexOf("Data")); // Extract base location for Data folder
                parameters.ReferencedAssemblies.Add(win32Location + "System.dll");
                parameters.ReferencedAssemblies.Add(engineLocation + path1 + "UnityEngine.CoreModule.dll");
                parameters.ReferencedAssemblies.Add(engineLocation + path2 + "UnityEngine.UI.dll");
                parameters.ReferencedAssemblies.Add(win32Location + "Microsoft.CSharp.dll");
                parameters.ReferencedAssemblies.Add(win32Location + "Facades\\netstandard.dll");
            }
            else
            {
                string assemblyLocation = parameters.ReferencedAssemblies.GetType().Assembly.Location;
                string folderPath = assemblyLocation.Substring(0, assemblyLocation.IndexOf("System.dll")); // Snip off the "System.dll" information
                parameters.ReferencedAssemblies.Add(folderPath + "System.dll");
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
            result = provider.CompileAssemblyFromSource(parameters, sourceCode);

            // Check if there were compilation errors
            if (result.Errors.HasErrors)
            {
                displayLog = false;
                programOutput.text = ""; // Clear the current output box
                foreach (CompilerError error in result.Errors)
                {
                    if (error.ErrorNumber == "CS1525")
                        programOutput.text += "Syntax error\n\n";
                    else
                        // Use the following if you want error codes along with the error text.
                        // String.Format("Error ({0}): ({1})", error.ErrorNumber, error.ErrorText)
                        programOutput.text += error.ErrorText + "\n";
                }
                programOutput.text += playerInput.text;
            }
            else
            {
                programOutput.text = @"There is a function named ""MoveBit"" that will move bit forward by 1 foot.
To complete this level, write code that will move Bit 226 feet to the NPC.";
            }

            // Return the assembly
            return result.CompiledAssembly;
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