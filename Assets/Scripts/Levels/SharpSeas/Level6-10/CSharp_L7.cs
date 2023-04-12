/*
 * This class uses the MonoCSharp Code Compiler.
 * This class will be run to allow the player to
 * learn about if-else statements in C#.
 * 
 * Date: 03/30/2023
 * Author: Robot and I Team
 */

using UnityEngine;
using UnityEngine.Audio;
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
    public class CSharp_L7 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
		public TextMeshProUGUI soundCheck;
        public TextMeshProUGUI completionBox;
        public TMP_InputField playerInput;
        public List<GameObject> GoodFood;
        public List<GameObject> BadFood;

        // Private variables
        private string declarations;
        private bool displayLog;

        // Use update to check the completion box.
        // Correct shows the dog food sorted correctly
        // Incorrect shows the dog food sorted incorrectly
        private void Update()
        {
		 /*	check if the text box
		  * under the screen has the work correct, incorrect or a 
		  * blank space. If it's correct, play the correlating sound
		  * effect and change the text to a blank. If it's incorrect, 
		  * play the correlating sound effect and change the text to a
		  * blank. If it's blank, play nothing.
		  */
			if (soundCheck.text == "incorrect")
			{
				Audio_Manager.Instance.PlaySound("Incorrect");
				soundCheck.text = " ";
			}
			else 
			{
				if (soundCheck.text == "correct")
				{
					Audio_Manager.Instance.PlaySound("Correct");
					soundCheck.text = " ";
				}
			}
		 
            // Check if completionBox is correct
            if (completionBox.text == "Correct")
            {
                StartCoroutine(DisplayCorrect());
                completionBox.text = "";
            }

            // Check if completionBox is incorrect
            if (completionBox.text == "Incorrect")
            {
                StartCoroutine(DisplayIncorrect());
                completionBox.text = "";
            }
        }

        /*
         * This function drives the entire program. This function
         * is ran when the user clicks Sort.
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
                    TMP_Text output = GameObject.Find(""Error Output"").GetComponent<TMP_Text>();
                    TMP_Text completionBox = GameObject.Find(""Completed"").GetComponent<TMP_Text>();
					TMP_Text check = GameObject.Find(""check"").GetComponent<TMP_Text>();
                    TMP_InputField code = GameObject.Find(""User Input"").GetComponent<TMP_InputField>();

                    // The problem statement is already displayed.
                    // Create the variables and use the player's defined variables
                    string[] inputFood = new string[]
                    { ""Green"", ""Red"", ""Red"", ""Green"", ""Green"",
                      ""Red"", ""Green"", ""Green"", ""Red"", ""Green"" };
                    int counter = 0;
                    " + declarations + @"

                    // Put the program in a loop
                    while (counter < 10)
                    {
                        // Assign the string to the DogFood
                        DogFood = inputFood[counter];

                        // Insert the player's code into the program
                        " + playerText + @"

                        // Increment counter
                        counter += 1;
                    }

                    // Test that Red, Green and Total are correct
                    if (Green == 6 && Red == 4 && Total == 10)
                    {
                        // Display a message and set the completed text box
                        output.text = ""Your code successfully performed against all the test cases. Good Work!!"";
                        completionBox.text = ""Correct"";
						check.text = ""correct"";
                        GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                        NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        // Display an appropriate message to the user and set the completed text box
                        output.text = ""Your code failed during one of the test cases. Try Again, practice makes perfect."";
                        completionBox.text = ""Incorrect"";
						check.text = ""incorrect"";
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
         * This function will check that the player declared the proper variables
         * and that the player used an if-else statement for this level.
         * Once all checks are complete, this code is passed to the MainDriver();
         */
        private string InputModification(string playerCode)
        {
            // Create a string for the return
            string newCode = playerCode;
            int startIndex, endIndex;

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

            // Check for declared variables
            if (!newCode.Contains("DogFood") || !newCode.Contains("Red") ||
                !newCode.Contains("Green") || !newCode.Contains("Total"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must declare and use the variables DogFood, Red, Green and Total.";
                throw new Exception("Sorry, but you must declare and use the variables DogFood, Red, Green and Total.");
            }

            // Test that the user used an if-else statement
            if (!newCode.Contains("if") || !newCode.Contains("else"))
            {
                programOutput.text = "Sorry, but you must use an if-else statement for this level.";
                throw new Exception("Sorry, but you must use an if-else statement for this level.");
            }

            // Strip out the player's variable declaration section and put it in the declarations variable
            startIndex = 0; // Start of input text
            endIndex = newCode.IndexOf("if"); // Player Code starts at if
            declarations = newCode[startIndex..endIndex];

            // Remove the declarations section from the player code
            newCode = newCode.Remove(startIndex, (endIndex - startIndex));

            return newCode;
        }

        // This function, DisplayCorrect, will display dog food
        // falling from the sky in the correct boxes to the user
        private IEnumerator DisplayCorrect()
        {
            Vector3 startLocation;

            // Loop through each of the DogFood Objects
            for (int i = 0; i < 10; ++i)
            {
                // Get the starting location of the dog food and set it active
                startLocation = GoodFood[i].transform.position;
                GoodFood[i].SetActive(true);

                // Loop through moving the DogFood down the Screen
                for (int j = 0; j < 210; ++j)
                {
                    // Move the GoodFood object down by 1
                    GoodFood[i].transform.Translate(0, -1, 0);

                    // Yield statement allows the DogFood to simulate moving through space in frames
                    yield return null;
                }

                // End of move, set dog food inactive and reset position
                GoodFood[i].SetActive(false);
                GoodFood[i].transform.position = startLocation;
            }
        }

        // This function, DisplayIncorrect, will display dog food
        // falling from the sky in the wrong boxes to the user
        private IEnumerator DisplayIncorrect()
        {
            Vector3 startLocation;

            // Loop through each of the DogFood Objects
            for (int i = 0; i < 10; ++i)
            {
                // Get the starting location of the dog food and set it active
                startLocation = BadFood[i].transform.position;
                BadFood[i].SetActive(true);

                // Loop through moving the DogFood down the Screen
                for (int j = 0; j < 210; ++j)
                {
                    // Move the BadFood object down by 1
                    BadFood[i].transform.Translate(0, -1, 0);

                    // Yield statement allows the DogFood to simulate moving through space in frames
                    yield return null;
                }

                // End of move, set dog food inactive and reset position
                BadFood[i].SetActive(false);
                BadFood[i].transform.position = startLocation;
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