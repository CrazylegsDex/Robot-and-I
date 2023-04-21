/*
 * This class uses the MonoCSharp Code Compiler.
 * This class will be run to allow the player to
 * learn about input and output in C#.
 * 
 * Date: 04/13/2023
 * Author: Robot and I Team
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
    public class CSharp_L10 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI problemNumber;
        public TextMeshProUGUI soundCheck;
        public TMP_InputField negotiateBox;
        public TMP_InputField responseBox;

        // Private variables
        private string negotiateText;
        private string responseText;
        private string declarations;
        private bool displayLog;

        // Use update to check the speedCheck input box
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
            
            // Call the correct InputModification for the problem Number
            if (Int32.Parse(problemNumber.text) % 2 == 0)
                InputModificationNegotiate();
            else
                InputModificationResponse();

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
                    TMP_Text output = GameObject.Find(""Output"").GetComponent<TMP_Text>();
                    TMP_Text problemNumber = GameObject.Find(""StatementNumber"").GetComponent<TMP_Text>();
                    TMP_Text soundBox = GameObject.Find(""SoundCheckBox"").GetComponent<TMP_Text>();

                    // Local variables and user's declaration section
                    int num;
                    string[] PirateText = new string[]
                    {
                        ""What's 1 + 1?"",
                        ""How do you spell supercalifragilisticexpialidocious?"",
                        ""What is the value of Total if I did this?\r\n\r\n"" +
                            ""if (true != false)\r\n"" +
                            ""    Total = 1024;\r\n"" +
                            ""else\r\n"" +
                            ""    Total = 42;"",
                        ""What is the base 10 value of 0010 1010?"",
                        ""Who taught you how to program Bit like this?""
                    };
                    " + declarations + @"

                    // Determine the problem number to jump to
                    switch(problemNumber.text)
                    {
                        case ""0"":
                            num = 0; // Array indexing for message number
                            " + negotiateText + @"
                            output.text = Message;
                            problemNumber.text = ""1"";
                            break;
                        case ""1"":
                            " + responseText + @"
                            if (Message == ""2"")
                            {
                                output.text = Message + ""\r\n\r\nGood job Bit"";
                                problemNumber.text = ""2"";
                                soundBox.text = ""Correct"";
                            }
                            else
                            {
                                output.text = ""Bit?...\r\n\r\nDid we lose an AND gate in that last level?"";
                                problemNumber.text = ""0"";
                                soundBox.text = ""Incorrect"";
                            }
                            break;
                        case ""2"":
                            num = 1; // Array indexing for message number
                            " + negotiateText + @"
                            output.text = Message;
                            problemNumber.text = ""3"";
                            break;
                        case ""3"":
                            " + responseText + @"
                            if (Message == ""supercalifragilisticexpialidocious"")
                            {
                                output.text = Message + ""\r\n\r\nHaha. That was an easy one wasn't it Bit?"";
                                problemNumber.text = ""4"";
                                soundBox.text = ""Correct"";
                            }
                            else
                            {
                                output.text = ""Hmmm. Maybe I should have taught more lessons on spelling?"";
                                problemNumber.text = ""0"";
                                soundBox.text = ""Incorrect"";
                            }
                            break;
                        case ""4"":
                            num = 2; // Array indexing for message number
                            " + negotiateText + @"
                            output.text = Message;
                            problemNumber.text = ""5"";
                            break;
                        case ""5"":
                            " + responseText + @"
                            if (Message == ""1024"")
                            {
                                output.text = Message + ""\r\n\r\nThat is true Bit!!"";
                                problemNumber.text = ""6"";
                                soundBox.text = ""Correct"";
                            }
                            else
                            {
                                output.text = ""Check your logic Bit.\r\n\r\nDoes true equal false?\r\nDoes that make the statement true?"";
                                problemNumber.text = ""0"";
                                soundBox.text = ""Incorrect"";
                            }
                            break;
                        case ""6"":
                            num = 3; // Array indexing for message number
                            " + negotiateText + @"
                            output.text = Message;
                            problemNumber.text = ""7"";
                            break;
                        case ""7"":
                            " + responseText + @"
                            if (Message == ""42"")
                            {
                                output.text = Message + ""\r\n\r\nYou got your Bit's right."";
                                problemNumber.text = ""8"";
                                soundBox.text = ""Correct"";
                            }
                            else
                            {
                                output.text = ""Wait... Are they playing Battleship?\r\nOh, that was a miss; Bit."";
                                problemNumber.text = ""0"";
                                soundBox.text = ""Incorrect"";
                            }
                            break;
                        case ""8"":
                            num = 4; // Array indexing for message number
                            " + negotiateText + @"
                            output.text = Message;
                            problemNumber.text = ""9"";
                            break;
                        case ""9"":
                            " + responseText + @"
                            if (Message == ""The Duke"")
                            {
                                output.text = Message + ""\r\n\r\nYou remembered me Bit :)\r\nI'm not crying. Just really proud of you. Good job Bit."";
                                GameObject.FindWithTag(""LevelChange"").GetComponent<BoxCollider2D>().isTrigger = true;
                                soundBox.text = ""Correct"";
                            }
                            else
                            {
                                output.text = ""BIT!.... How. How could you forget me?????\r\n\r\n:("";
                                problemNumber.text = ""0"";
                                soundBox.text = ""Incorrect"";
                            }
                            break;
                    }

                    // Keep runtime clean and speedy
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }
            }";
            Debug.Log(playerCode);

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
         * This function, InputModificationNegotiation, checks and modifies the player's input
         * on the Negotiate Side of input.
         * This function will check that the player declared the proper variables
         * and that the player used input and output for this level.
         * Once all checks/additions are complete, code execution continues to MainDriver();
         */
        private void InputModificationNegotiate()
        {
            // Assign the text box code to variables
            negotiateText = negotiateBox.text;
            responseText = "";
            int startIndex, endIndex;

            // Check for malicious code in either text box
            if (negotiateText.Contains("GameObject") || negotiateText.Contains("sleep") ||
                negotiateText.Contains("while") || negotiateText.Contains("for"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // Check for declared variables
            if (!negotiateText.Contains("Message"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must use Message in the Negotiate box.";
                throw new Exception("Sorry, but you must use Message in the Negotiate box.");
            }

            // Test that the user used Input and output statements.
            if (!negotiateText.Contains("System.Console.ReadLine()") ||
                !negotiateText.Contains("System.Console.WriteLine(Message)"))
            {
                programOutput.text = "Sorry, but you must use System.Console.ReadLine\r\nfor input and System.Console.WriteLine(Message) for output.";
                throw new Exception("Sorry, but you must use System.Console.ReadLine\r\nfor input and System.Console.WriteLine(Message) for output.");
            }

            // Unity will complain for multiple declarations of Message in the runtime code
            // Strip out the declarations line of message depending on how the user declared
            // message.
            // 1. string Message; (endIndex == 15)
            // 2. string Message = System.Console.ReadLine(); (endIndex > 15)
            startIndex = negotiateText.IndexOf("string"); // Start at the string declaration
            endIndex = negotiateText.IndexOf(';', startIndex) + 1; // Get the index of the semicolon.
            if (endIndex > 15)
                endIndex = negotiateText.IndexOf("Message", startIndex); // End the index after the string
            declarations = "string Message = \"\";";

            // Remove the declarations section from the player code
            negotiateText = negotiateText.Remove(startIndex, (endIndex - startIndex));

            // Replace the System.Console.ReadLine() statement to an array of statements
            negotiateText = negotiateText.Replace("System.Console.ReadLine()", "PirateText[num]");
        }

        /*
         * This function, InputModificationResponse, checks and modifies the player's input
         * on the Response Side of input.
         * This function will check that the player declared the proper variables
         * and that the player used input and output for this level.
         * Once all checks/additions are complete, code execution continues to MainDriver();
         */
        private void InputModificationResponse()
        {
            // Assign the text box code to variables
            negotiateText = "";
            responseText = responseBox.text;
            int startIndex, endIndex;

            // Check for malicious code in either text box
            if (responseText.Contains("GameObject") || responseText.Contains("sleep") ||
                responseText.Contains("while") || responseText.Contains("for"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }

            // Check for declared variables
            if (!responseText.Contains("Message"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must use Message in the Response box.";
                throw new Exception("Sorry, but you must use Message in the Response box.");
            }

            // Test that the user used Input and output statements.
            if (!responseText.Contains("System.Console.WriteLine(Message)"))
            {
                programOutput.text = "Sorry, but you must use System.Console.WriteLine(Message) for output.";
                throw new Exception("Sorry, but you must use System.Console.WriteLine(Message) for output.");
            }

            // Unity will complain for multiple declarations of Message in the runtime code
            // Strip out the declarations line of message depending on how the user declared
            // message.
            // 1. string Message; (endIndex == 15)
            // 2. string Message = "My Response"; (endIndex > 15)
            startIndex = responseText.IndexOf("string"); // Start at the string declaration
            endIndex = responseText.IndexOf(';', startIndex) + 1; // Get the index of the semicolon.
            if (endIndex > 15)
                endIndex = responseText.IndexOf("Message", startIndex); // End the index after the string
            declarations = "string Message = \"\";";

            // Remove the declarations section from the player code
            responseText = responseText.Remove(startIndex, (endIndex - startIndex));
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