/*
 * This class uses the MonoCSharp Code Compiler
 * to check boolean truth values. This level
 * will allow the player to use the <, <=,
 * >, >=, != and == operators and check those
 * outputs with the desired output.
 * 
 * Date: 02/27/2023
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
    public class CSharp_L4 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TMP_InputField playerInput;

        // Private variables
        private bool displayLog;

        /*
         * This function drives the entire program. This function
         * is ran when the user clicks Check Answer.
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
                    TMP_Text result = GameObject.Find(""Result"").GetComponent<TMP_Text>();
                    TMP_Text output = GameObject.Find(""Error Output"").GetComponent<TMP_Text>();
                    TMP_InputField code = GameObject.Find(""User Input"").GetComponent<TMP_InputField>();

                    // Test that the user used the variables in his/her code
                    if (code.text.Contains(""cup"") && code.text.Contains(""bowl"") && code.text.Contains(""resultingLogic""))
                    {
                        // If-Statement to the correct Problem Statement. There are 12 total problems to complete
                        // Reverse order due to changing to next if statement upon completion of previous.
                        // Causes multiple if-statements to run if done linearly
                        // Problem 12
                        if (problemStatement.text.Contains(""int cup = 42;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial12"");
                            int cup = 42, bowl = 64;
                            bool resultingLogic = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""You did it BIT!!!!\r\nYou have completed the level."";
                                    GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                                    NPC.GetComponent<BoxCollider2D>().isTrigger = true;

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 11
                        if (problemStatement.text.Contains(""double cup = 97.6;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial11"");
                            double cup = 97.6, bowl = 98.1;
                            bool resultingLogic = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (!resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Amazing work"";
                                    problemStatement.text = ""int cup = 42;\r\nint bowl = 64;"";
                                    result.text = ""true"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 10
                        if (problemStatement.text.Contains(""int cup = 1024;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial10"");
                            int cup = 1024, bowl = -1024;
                            bool resultingLogic = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""You are really good at this."";
                                    problemStatement.text = ""double cup = 97.6;\r\ndouble bowl = 98.1;"";
                                    result.text = ""false"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 9
                        if (problemStatement.text.Contains(""int cup = 65536;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial9"");
                            int cup = 65536, bowl = -4096;
                            bool resultingLogic = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Great Job!!"";
                                    problemStatement.text = ""int cup = 1024;\r\nint bowl = -1024;"";
                                    result.text = ""true"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 8
                        if (problemStatement.text.Contains(""double cup = -77;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial8"");
                            double cup = -77, bowl = -99;
                            bool resultingLogic = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (!resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Hurry Bit. Just a few more to go."";
                                    problemStatement.text = ""int cup = 65536;\r\nint bowl = -4096;"";
                                    result.text = ""true"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 7
                        if (problemStatement.text.Contains(""double cup = 3.14;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial7"");
                            double cup = 3.14, bowl = 3.14;
                            bool resultingLogic = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Another one down."";
                                    problemStatement.text = ""double cup = -77;\r\ndouble bowl = -99;"";
                                    result.text = ""false"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 6
                        if (problemStatement.text.Contains(""int cup = 255;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial6"");
                            int cup = 255, bowl = 22;
                            bool resultingLogic = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (!resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Half way there. Keep it up!"";
                                    problemStatement.text = ""double cup = 3.14;\r\ndouble bowl = 3.14;"";
                                    result.text = ""true"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 5
                        if (problemStatement.text.Contains(""int cup = -1;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial5"");
                            int cup = -1, bowl = 0;
                            bool resultingLogic = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (!resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Good work"";
                                    problemStatement.text = ""int cup = 255;\r\nint bowl = 22;"";
                                    result.text = ""false"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 4
                        if (problemStatement.text.Contains(""int cup = 105;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial4"");
                            int cup = 105;
                            double bowl = 104.9;
                            bool resultingLogic = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (!resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Wooop!!"";
                                    problemStatement.text = ""int cup = -1;\r\nint bowl = 0;"";
                                    result.text = ""false"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 3
                        if (problemStatement.text.Contains(""double cup = 3.4;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial3"");
                            double cup = 3.4, bowl = 3.5;
                            bool resultingLogic = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Awesome work"";
                                    problemStatement.text = ""int cup = 105;\r\ndouble bowl = 104.9;"";
                                    result.text = ""false"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 2
                        if (problemStatement.text.Contains(""int cup = -100;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial2"");
                            int cup = -100, bowl = 100;
                            bool resultingLogic = false;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Congratulations"";
                                    problemStatement.text = ""double cup = 3.4;\r\ndouble bowl = 3.5;"";
                                    result.text = ""true"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }

                        // Problem 1
                        if (problemStatement.text.Contains(""int cup = 0;""))
                        {
                            // The problem statement is already displayed.
                            // Create the variables
                            GameObject trash = GameObject.Find(""Burial1"");
                            int cup = 0, bowl = 0;
                            bool resultingLogic = true;

                            // Insert the player's code into the program
                            " + playerText + @"

                            // Test the value of result
                            if (!resultingLogic)
                            {
                                // Test that the operator used is valid
                                if (TestSetOperator(code.text))
                                {
                                    // Set the input & output boxes
                                    output.text = ""Good Job Bit!"";
                                    problemStatement.text = ""int cup = -100;\r\nint bowl = 100;"";
                                    result.text = ""true"";
                                    code.text = """";

                                    // Remove the Cup or Bowl
                                    trash.SetActive(false);
                                }
                                else
                                {
                                    // Display an appropriate message to the user
                                    output.text = ""You cannot use that relational operator anymore.\r\nPlease try again."";
                                }
                            }
                            else
                            {
                                // The value of result is incorrect
                                output.text = ""The value of resultingLogic is incorrect."";
                            }
                        }
                    }
                    else
                    {
                        // Inform the player they need to use the variables cup and bowl and resultingLogic in their program
                        output.text = ""Sorry, but you must use the variables cup, bowl and resultingLogic in your program"";
                    }

                    // Keep runtime clean and speedy
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }

                // This function tests if the relational operator is available to
                // use. If it is, it will be removed from the available operators
                // and the return is true. If the operator is not available to use,
                // this function simply returns false.
                private bool TestSetOperator(string input)
                {
                    // Get a GameObject of the available operators
                    TMP_Text operators = GameObject.Find(""Available Operators"").GetComponent<TMP_Text>();
                    int index = 0;

                    // For simplicity of code sake, test each operator one at a time
                    if (input.Contains("">="") && operators.text.Contains("">=""))
                    {
                        // Remove the operator from the available ones
                        index = operators.text.IndexOf("">="");
                        operators.text = operators.text.Remove(index, 2);
                        return true;
                    }

                    if (input.Contains(""<="") && operators.text.Contains(""<=""))
                    {
                        // Remove the operator from the available ones
                        index = operators.text.IndexOf(""<="");
                        operators.text = operators.text.Remove(index, 2);
                        return true;
                    }

                    if (input.Contains(""!="") && operators.text.Contains(""!=""))
                    {
                        // Remove the operator from the available ones
                        index  = operators.text.IndexOf(""!="");
                        operators.text = operators.text.Remove(index, 2);
                        return true;
                    }

                    if (input.Contains(""=="") && operators.text.Contains(""==""))
                    {
                        // Remove the operator from the available ones
                        index = operators.text.IndexOf(""=="");
                        operators.text = operators.text.Remove(index, 2);
                        return true;
                    }

                    if (input.Contains("">"") && operators.text.Contains("">""))
                    {
                        // Remove the operator from the available ones
                        index = operators.text.IndexOf("">"");
                        operators.text = operators.text.Remove(index, 1);
                        return true;
                    }

                    if (input.Contains(""<"") && operators.text.Contains(""<""))
                    {
                        // Remove the operator from the available ones
                        index = operators.text.IndexOf(""<"");
                        operators.text = operators.text.Remove(index, 1);
                        return true;
                    }

                    // Return false if no statement true
                    return false;
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
            if (newCode.Contains("bool resultingLogic"))
            {
                newCode = newCode.Replace("bool resultingLogic", "resultingLogic");
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