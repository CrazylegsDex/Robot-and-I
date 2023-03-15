/*
 * This class uses the MonoCSharp Code Compiler
 * to allow the player to write full C# code.
 * This class will be used with simple teaching
 * simple if-statements to the player.
 * 
 * Date: 03/10/2023
 * Author: Robot and I Team
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;

namespace CSharpLevels
{
    public class CSharp_L6 : MonoBehaviour
    {
        // Public variables
        public GameObject Bit;
        public GameObject LessonObject;
        public Sprite ChestSprite;
        public List<GameObject> Chest;
        public TextMeshProUGUI programOutput;
        public TMP_InputField playerInput;

        // Private variables
        private bool displayLog;

        // Use update to check Bit location on the map
        private void Update()
        {
            // Perform actions based on Bit's location
            switch (Bit.transform.position.x)
            {
                // First Chest Case
                case float x when x > 302f && x < 305f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[0].transform.position = new Vector3(341f, 340f);
                    break;

                // Second Chest Case
                case float x when x > 512f && x < 515f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[1].transform.position = new Vector3(550f, 340f);
                    break;

                // Third Chest Case
                case float x when x > 734f && x < 737f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[2].transform.position = new Vector3(770f, 340f);
                    break;

                // Fourth Chest Case
                case float x when x > 972f && x < 975f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[3].transform.position = new Vector3(1007f, 340f);
                    break;

                // Fifth Chest Case
                case float x when x > 1219f && x < 1222f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[4].transform.position = new Vector3(1253f, 340f);
                    break;

                // Sixth Chest Case
                case float x when x > 1460f && x < 1463f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[5].transform.position = new Vector3(1493f, 340f);
                    break;

                // Seventh Chest Case
                case float x when x > 1678f && x < 1681f:
                    // Set the Lesson to visible and move the chest up
                    LessonObject.SetActive(true);
                    Chest[6].transform.position = new Vector3(1708f, 340f);
                    break;
            }
        }

        /*
         * This function creates a new script and attaches it to
         * the scene at runtime. Then this function invokes that same
         * script immediately after.
         */
        public void RunCode()
        {
            // Local variables
            Assembly resultAssembly;
            Type runtimeClass;
            MethodInfo runtimeFunction;
            Func<GameObject, MonoBehaviour> runtimeDelegate;

            // Check player's input before run
            InputCheck();

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
                    TMP_Text output = GameObject.Find(""Error Output"").GetComponent<TMP_Text>();
                    output.text = """"; // Clear the output text box
                    TMP_InputField code = GameObject.Find(""User Input"").GetComponent<TMP_InputField>();

                    // Get the LessonObject to set inactive
                    GameObject lesson = GameObject.Find(""Lesson"");

                    // Create the chest variables
                    int chest2 = 42, chest4 = -4096, chest7 = 1024;
                    string chest1 = ""Smithy"", chest3 = ""Gold"", chest5 = ""Silver"";
                    bool chest6 = false;

                    // Test that the user used the variables in his/her code
                    if (code.text.Contains(""if"") && code.text.Contains(""chest"") && code.text.Contains(""found""))
                    {
                        // If-Statement to the correct Problem Statement. There are 7 total problems to complete
                        // Problem 1
                        if (code.text.Contains(""Smithy""))
                        {
                            // Create the variables
                            int found = 0;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 1)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                lesson.SetActive(false);
                                code.text = ""// Variable name is chest2\r\n"";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }

                        // Problem 2
                        if (code.text.Contains(""42""))
                        {
                            // Create the variables
                            int found = 1;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 2)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                lesson.SetActive(false);
                                code.text = ""// Variable name is chest3\r\n"";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }

                        // Problem 3
                        if (code.text.Contains(""Gold""))
                        {
                            // Create the variables
                            int found = 2;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 3)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                lesson.SetActive(false);
                                code.text = ""// Variable name is chest4\r\n"";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }

                        // Problem 4
                        if (code.text.Contains(""-4096""))
                        {
                            // Create the variables
                            int found = 3;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 4)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                lesson.SetActive(false);
                                code.text = ""// Variable name is chest5\r\n"";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }

                        // Problem 5
                        if (code.text.Contains(""Silver""))
                        {
                            // Create the variables
                            int found = 4;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 5)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                lesson.SetActive(false);
                                code.text = ""// Variable name is chest6\r\n"";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }

                        // Problem 6
                        if (code.text.Contains(""false""))
                        {
                            // Create the variables
                            int found = 5;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 6)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                lesson.SetActive(false);
                                code.text = ""// Variable name is chest7\r\n"";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }

                        // Problem 7
                        if (code.text.Contains(""1024""))
                        {
                            // Create the variables
                            int found = 6;

                            // Insert the player's code into the program
                            " + playerInput.text + @"

                            // Test the value of result
                            if (found == 7)
                            {
                                // Set the input code inactive and set the Chest
                                // Sprite graphic to open
                                GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                                NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                                lesson.SetActive(false);
                                code.text = """";
                            }
                            else
                            {
                                // The if-statement is incorrect
                                output.text = ""Your if-statement is either false, or you are not incrementing found by 1."";
                            }
                        }
                    }
                    else
                    {
                        // Inform the player they need to use if-statement and the variables chest and found in their code
                        output.text = ""Sorry, but you must use an if-statement and the variables \""chest\"" and \""found\"" in your program."";
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
         * This function, InputCheck, checks that the player did
         * not write malicious code inside of the input box. If the
         * player did write something bad, this function will abort
         * with an error message.
         */
        private void InputCheck()
        {
            // Check for malicious code
            if (playerInput.text.Contains("GameObject") || playerInput.text.Contains("sleep") ||
                playerInput.text.Contains("while") || playerInput.text.Contains("for"))
            {
                // Both are required due to Unity issues with display.
                programOutput.text = @"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.";
                throw new Exception(@"I am not running code with that kind of language in it. " +
                    "You should consider trying not to overwrite my programming.");
            }
        }

        /*
         * This function, OpenChest, sets the graphic for a chest
         * to the open chest graphic. This function should only
         * be called from the dynamic runtime script in the
         * "RunCode" function.
         */
        public void OpenChest(int chestNumber)
        {
            // Change the Graphic of the appropriate chest number
            switch (chestNumber)
            {
                case 1:
                    Chest[0].GetComponent<SpriteRenderer>().sprite = ChestSprite;
                    break;
                case 2:
                    Chest[1].GetComponent<SpriteRenderer>().sprite = ChestSprite;
                    break;
                case 3:
                    Chest[2].GetComponent<SpriteRenderer>().sprite = ChestSprite;
                    break;
                case 4:
                    Chest[3].GetComponent<SpriteRenderer>().sprite = ChestSprite;
                    break;
                case 5:
                    Chest[4].GetComponent<SpriteRenderer>().sprite = ChestSprite;
                    break;
                case 6:
                    Chest[5].GetComponent<SpriteRenderer>().sprite = ChestSprite;
                    break;
                case 7:
                    Chest[6].GetComponent<SpriteRenderer>().sprite = ChestSprite;
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