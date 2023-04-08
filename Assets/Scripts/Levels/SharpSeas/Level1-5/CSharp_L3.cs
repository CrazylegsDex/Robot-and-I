/*
 * This class uses the MonoCSharp Code Compiler to
 * check math input operations. This level will allow
 * the player to create variables and add, subtract,
 * divide, multiply and modulus.
 * 
 * Date: 02/25/2023
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
    public class CSharp_L3 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TMP_InputField playerInput;

        // Private variables
        //private int problemNumber;
        private bool displayLog;

        /*
         * This function drives the entire program. This function
         * is ran when the user clicks Check Code.
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
                    TMP_Text problemStatement = GameObject.Find(""Problem Statement"").GetComponent<TMP_Text>();
                    TMP_Text programResults = GameObject.Find(""Results"").GetComponent<TMP_Text>();
                    TMP_InputField code = GameObject.Find(""User Input"").GetComponent<TMP_InputField>();

                    // Test that the user used the variables in his/her code
                    if (code.text.Contains(""a"") && code.text.Contains(""b"") && code.text.Contains(""result""))
                    {
                        // If-Statement to the correct Problem Statement. There are 6 total problems to complete
                        // Reverse order due to changing to next if statement upon completion of previous.
                        // Causes multiple if-statements to run if done linearly
                        // Problem 6
                        if (problemStatement.text.Contains(""integer -33""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            int a = -1, b = 100, result = 0;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (result == -33)
                            {
                                // Set the input & output boxes
                                programResults.text = ""Congratulations.\r\nLevel complete"";
                                GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                                NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                            }
                            else
                            {
                                // The value of result is incorrect
                                programResults.text = ""The value of result is incorrect.\r\nresult = "" + result;
                            }
                        }

                        // Problem 5
                        if (problemStatement.text.Contains(""integer 1250""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            int a = 5, b = 10, result = 0;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (result == 1250)
                            {
                                // Set the input & output boxes
                                programResults.text = ""One more to go"";
                                problemStatement.text = ""int a = -1, b = 100;\r\nName your variable result, and use a,b to get the resulting integer -33."";
                                code.text = """";
                            }
                            else
                            {
                                // The value of result is incorrect
                                programResults.text = ""The value of result is incorrect.\r\nresult = "" + result;
                            }
                        }

                        // Problem 4
                        if (problemStatement.text.Contains(""double 1.5""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            int a = 2;
                            double b = 3.0, result = 0;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (result == 1.5)
                            {
                                // Set the input & output boxes
                                programResults.text = ""Correct Again"";
                                problemStatement.text = ""int a = 5, b = 10;\r\nName your variable result, and use a,b to get the resulting integer 1250."";
                                code.text = """";
                            }
                            else
                            {
                                // The value of result is incorrect
                                programResults.text = ""The value of result is incorrect.\r\nresult = "" + result;
                            }
                        }

                        // Problem 3
                        if (problemStatement.text.Contains(""integer 1.""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            int a = 2, b = 3, result = 0;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (result == 1)
                            {
                                // Set the input & output boxes
                                programResults.text = ""Awesome Work"";
                                problemStatement.text = ""int a = 2; double b = 3;\r\nName your variable result, and use a,b to get the resulting double 1.5."";
                                code.text = """";
                            }
                            else
                            {
                                // The value of result is incorrect
                                programResults.text = ""The value of result is incorrect.\r\nresult = "" + result;
                            }
                        }

                        // Problem 2
                        if (problemStatement.text.Contains(""double 0.62""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            double a = 9.4, b = 7.3, result = 0;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Due to precision, round result off
                            result = Math.Round(result, 2);

                            // Test the value of result
                            if (result == 0.62)
                            {
                                // Set the input & output boxes
                                programResults.text = ""Great Job"";
                                problemStatement.text = ""int a = 2, b = 3;\r\nName your variable result, and use a,b to get the resulting integer 1."";
                                code.text = """";
                            }
                            else
                            {
                                // The value of result is incorrect
                                programResults.text = ""The value of result is incorrect.\r\nresult = "" + result;
                            }
                        }

                        // Problem 1
                        if (problemStatement.text.Contains(""integer 35""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            int a = 26, b = 59, result = 0;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (result == 35)
                            {
                                // Set the input & output boxes
                                programResults.text = ""Correct"";
                                problemStatement.text = ""double a = 9.4, b = 7.3;\r\nName your variable result, and use a,b to get the resulting double 0.62."";
                                code.text = """";
                            }
                            else
                            {
                                // The value of result is incorrect
                                programResults.text = ""The value of result is incorrect.\r\nresult = "" + result;
                            }
                        }
                    }
                    else
                    {
                        // Inform the player they need to use the variables a and b and result in their program
                        programResults.text = ""Sorry, but you must use the variables a, b and result in your program"";
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
                programOutput.text = error[0].ErrorNumber switch
                {
                    // Syntax Errors
                    "CS1525" => $"Error: You have made a Syntax Error in your code.",

                    // Variable Name Unknown. EX. int spellMeRight; => spelMeRight = 0;
                    "CS0103" => $"Error: You have typoed one of your variable names, or you never declared it.\n\n{error[0].ErrorText}",

                    // Uninitialized Variable usage
                    "CS0165" => $"Error: You are trying to use a variable in your code that has not been assigned a value.",

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
         * This function will check and remove the "int" or "double" declarations
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
                newCode.Contains("while")      || newCode.Contains("for"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it.";
                throw new Exception(@"I am not running code with that kind of language in it.");
            }

            // Check if the player typed int result
            if (newCode.Contains("int result"))
            {
                newCode = newCode.Replace("int result", "result");
            }

            // Check if the player typed double result
            if (newCode.Contains("double result"))
            {
                newCode = newCode.Replace("double result", "result");
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