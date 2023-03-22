/*
 * This class uses the MonoCSharp Code Compiler
 * to check boolean truth values. This level
 * will allow the player to use the &&, ||,
 * and ^ operators and check those outputs with
 * the desired output.
 * 
 * Date: 03/09/2023
 * Author: Robot and I Team
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;

namespace CSharpLevels
{
    public class CSharp_L5 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TMP_InputField playerInput;

        // Private variables
        private bool displayLog;

        /*
         * This function drives the entire program. This function
         * is ran when the user clicks Go Fish.
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
            using TMPro;

            public class RuntimeScript : MonoBehaviour
            {
                // Required for addition to the GameObject
                public static RuntimeScript AddYourselfTo(GameObject host)
                {
                    // Add RuntimeScript to the host object
                    return host.AddComponent<RuntimeScript>();
                }

                // When the script is added to the HostGameObject and invoked, run the following code
                void Start()
                {
                    // Get references to the input & output text boxes
                    TMP_Text problemStatement = GameObject.Find(""Restrictions"").GetComponent<TMP_Text>();
                    TMP_Text output = GameObject.Find(""Error Output"").GetComponent<TMP_Text>();
                    TMP_InputField code = GameObject.Find(""User Input"").GetComponent<TMP_InputField>();

                    // Get Transforms of the fish
                    Transform Fish1 = GameObject.Find(""Fish1"").transform;
                    Transform Fish2 = GameObject.Find(""Fish2"").transform;
                    Transform Fish3 = GameObject.Find(""Fish3"").transform;
                    Transform Fish4 = GameObject.Find(""Fish4"").transform;

                    // Test that the user used the variables in his/her code
                    if ((code.text.Contains(""Pond1"") ||
                        code.text.Contains(""Pond2"")  ||
                        code.text.Contains(""Pond3"")  ||
                        code.text.Contains(""Pond4"")) &&
                        code.text.Contains(""GotEm""))
                    {
                        // If-Statement to the correct Problem Statement. There are 6 total problems to complete
                        // Reverse order due to changing to next if statement upon completion of previous.
                        // Causes multiple if-statements to run if done linearly
                        // Problem 6
                        if (problemStatement.text.Contains(""Logical ^ Operator""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            bool GotEm = false, Pond1 = false, Pond2 = false, Pond3 = false, Pond4 = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (GotEm)
                            {
                                // Test that the operator used is valid
                                if (code.text.Contains(""^""))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Epic Job!\r\nWell Done Bit!"";
                                    GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                                    NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                                    code.text = ""bool Bit_Is_Awesome = true;"";

                                    // Set the Fish z-axis. (greater than 0 is invisible)
                                    Fish1.position = new Vector3(997, 489, -4);  // Visible
                                    Fish2.position = new Vector3(1081, 489, -4); // Visible
                                    Fish3.position = new Vector3(1151, 489, -4); // Visible
                                    Fish4.position = new Vector3(1236, 489, -4); // Visible
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You must use \""^\"" for this problem.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of GotEm is incorrect
                                output.text = ""The value of GotEm is not true."";
                            }
                        }

                        // Problem 5
                        if (problemStatement.text.Contains(""Logical || Operator""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            bool GotEm = false, Pond1 = true, Pond2 = true, Pond3 = true, Pond4 = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of GotEm
                            if (GotEm)
                            {
                                // Test that the operator used is valid
                                if (code.text.Contains(""||""))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Great Work!"";
                                    problemStatement.text = ""For this problem, use the Logical ^ Operator."";
                                    code.text = """";

                                    // Set the Fish z-axis. (greater than 0 is invisible)
                                    Fish1.position = new Vector3(997, 489, 1);  // Invisible
                                    Fish2.position = new Vector3(1081, 489, 1); // Invisible
                                    Fish3.position = new Vector3(1151, 489, 1); // Invisible
                                    Fish4.position = new Vector3(1236, 489, 1); // Invisible
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You must use \""||\"" for this problem.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of GotEm is incorrect
                                output.text = ""The value of GotEm is not true."";
                            }
                        }

                        // Problem 4
                        if (problemStatement.text.Contains(""Logical && Operator""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            bool GotEm = false, Pond1 = true, Pond2 = false, Pond3 = false, Pond4 = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of GotEm
                            if (GotEm)
                            {
                                // Test that the operator used is valid
                                if (code.text.Contains(""&&""))
                                {
                                    // Set the input & output boxes
                                    output.text = ""010101110110111101110111\r\nTranslating. . .\r\nWow"";
                                    problemStatement.text = ""For this problem, use the Logical || Operator."";
                                    code.text = """";

                                    // Set the Fish z-axis. (greater than 0 is invisible)
                                    Fish2.position = new Vector3(1081, 489, -4); // Visible
                                    Fish3.position = new Vector3(1151, 489, -4); // Visible
                                    Fish4.position = new Vector3(1236, 489, -4); // Visible
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You must use \""&&\"" for this problem.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of GotEm is incorrect
                                output.text = ""The value of GotEm is not true."";
                            }
                        }

                        // Problem 3
                        if (problemStatement.text.Contains(""logical ^ operator""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            bool GotEm = false, Pond1 = false, Pond2 = true, Pond3 = true, Pond4 = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of GotEm
                            if (GotEm)
                            {
                                // Test that the operator used is valid
                                if (code.text.Contains(""^""))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Awesome!"";
                                    problemStatement.text = ""For this problem, use the Logical && Operator."";
                                    code.text = """";

                                    // Set the Fish z-axis. (greater than 0 is invisible)
                                    Fish1.position = new Vector3(997, 489, -4); // Visible
                                    Fish2.position = new Vector3(1081, 489, 1); // Invisible
                                    Fish3.position = new Vector3(1151, 489, 1); // Invisible
                                    Fish4.position = new Vector3(1236, 489, 1); // Invisible
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You must use \""^\"" for this problem.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of GotEm is incorrect
                                output.text = ""The value of GotEm is not true."";
                            }
                        }

                        // Problem 2
                        if (problemStatement.text.Contains(""logical || operator""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            bool GotEm = false, Pond1 = false, Pond2 = false, Pond3 = true, Pond4 = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of GotEm
                            if (GotEm)
                            {
                                // Test that the operator used is valid
                                if (code.text.Contains(""||""))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Congratulations!"";
                                    problemStatement.text = ""For this problem, use the logical ^ operator."";
                                    code.text = """";

                                    // Set the Fish z-axis. (greater than 0 is invisible)
                                    Fish2.position = new Vector3(1081, 489, -4); // Visible
                                    Fish4.position = new Vector3(1236, 489, -4); // Visible
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You must use \""||\"" for this problem.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of GotEm is incorrect
                                output.text = ""The value of GotEm is not true."";
                            }
                        }

                        // Problem 1
                        if (problemStatement.text.Contains(""logical && operator""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            bool GotEm = false, Pond1 = true, Pond2 = false, Pond3 = false, Pond4 = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of GotEm
                            if (GotEm)
                            {
                                // Test that the operator used is valid
                                if (code.text.Contains(""&&""))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Good Job Bit!"";
                                    problemStatement.text = ""For this problem, use the logical || operator."";
                                    code.text = """";

                                    // Set the Fish z-axis. (greater than 0 is invisible)
                                    Fish1.position = new Vector3(997, 489, 1);   // Invisible
                                    Fish4.position = new Vector3(1236, 489, 1);  // Invisible
                                    Fish3.position = new Vector3(1151, 489, -4); // Visible
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You must use \""&&\"" for this problem.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of GotEm is incorrect
                                output.text = ""The value of GotEm is not true."";
                            }
                        }
                    }
                    else
                    {
                        // Inform the player they need to use the variables PondX and GotEm in their program
                        output.text = ""Sorry, but you must use the variables PondX (where X is 1-4) and GotEm in your program"";
                    }

                    // Keep runtime clean and speedy
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
            if (result.Errors.HasErrors)
            {
                displayLog = false;
                programOutput.text = ""; // Clear the current output box

                // Display only 1 error
                CompilerErrorCollection error = result.Errors;
                if (error[0].ErrorNumber == "CS1525")
                    programOutput.text = "Syntax error";
                else
                    programOutput.text = String.Format("Line: {0}\nError: {1}", error[0].Line, error[0].ErrorText);
            }

            // Return the assembly
            return result.CompiledAssembly;
        }

        /*
         * This function, InputModification, checks and modifies the player's input.
         * This function will check and remove the "bool" declarations
         * for the player input.
         * This is required due to the pre-definition of the variables in the
         * runtime code string
         */
        private string InputModification(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;

            // Check for malicious code
            if (newCode.Contains("GameObject") || newCode.Contains("sleep") ||
                newCode.Contains("while") || newCode.Contains("for"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // Check if the player typed bool result
            if (newCode.Contains("bool GotEm"))
            {
                newCode = newCode.Replace("bool GotEm", "GotEm");
            }

            return newCode;
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