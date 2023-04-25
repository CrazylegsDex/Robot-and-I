/*
 * This class uses the MonoCSharp Code Compiler.
 * This class will be run to allow the player to
 * learn about switch statements in C#.
 * 
 * Date: 04/13/2023
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
    public class CSharp_L9 : MonoBehaviour
    {
        // Public variables
        public TextMeshProUGUI programOutput;
        public TextMeshProUGUI soundCheck;
        public TextMeshProUGUI speedCheck;
        public TMP_InputField playerInput;
        public List<GameObject> Crabs;

        // Private variables
        private string declarations;
        private string[] speeds;
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

            // Check the speedCheck text box
            if (speedCheck.text != "")
            {
                // Get the array of values in the text box
                speeds = speedCheck.text.Split(",");

                // Loop through the speeds array
                for (int i = 0; i < 4; ++i)
                {
                    // Replace the speed value with a new one
                    speeds[i] = speeds[i] switch
                    {
                        // Left is original, right is new value
                        "1" => "0.6",
                        "2" => "0.7",
                        "3" => "0.8",
                        "4" => "0.9",
                        "5" => "1",
                        _ => "1"
                    };
                }
                
                // Start the Race Coroutine and clear the text box
                StartCoroutine(StartRace());
                speedCheck.text = "";
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
                    TMP_Text soundBox = GameObject.Find(""SoundCheckBox"").GetComponent<TMP_Text>();

                    // The problem statement is already displayed.
                    // Create the variables and use the player's defined variables
                    string[] nameTest = new string[] { ""Chitin"", ""Red"", ""Clamps"", ""Jenny"" };
                    int[] speeds = new int[4];
                    int winSpeed = 0, errorNumber = 0;
                    " + declarations + @"

                    // Put the program in a loop
                    for (int i = 0; i < 4; ++i)
                    {
                        // Assign the *input* variable data
                        CrabName = nameTest[i];
                        CrabSpeed = 0;

                        // Insert the player's code into the program
                        " + playerText + @"

                        // Assign the winSpeed to Clamps, else assign the speed to the speed array
                        if (CrabName == ""Clamps"")
                        {
                            winSpeed = CrabSpeed;
                            speeds[i] = -1;
                        }
                        else
                            speeds[i] = CrabSpeed;

                        // Test the CrabSpeed variable is within range
                        if (CrabSpeed < 1 || CrabSpeed > 5)
                        {
                            errorNumber = 1;
                        }

                        // Send the speed to the completionBox
                        completionBox.text += CrabSpeed.ToString() + "","";
                    }

                    // Test the speed array with the winSpeed if valid speeds
                    if (errorNumber == 0)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            // Crab is faster than desired winner (precedence over tie)
                            if (speeds[i] > winSpeed)
                                errorNumber = 2;

                            // Crab is same speed as desired winner (tie)
                            if (speeds[i] == winSpeed && errorNumber == 0)
                                errorNumber = 3;
                        }
                    }

                    // Test if Clamps won
                    if (errorNumber == 0)
                    {
                        // Send winner to the output box
                        completionBox.text += ""Winner"";
                        soundBox.text = ""Correct"";
                        GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                        NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        // Case statement on ErrorNumber
                        switch (errorNumber)
                        {
                            case 1:
                                completionBox.text = """";
                                soundBox.text = ""Incorrect"";
                                output.text = ""The crabs cannot move slower than 1 or faster than 5. Please try again."";
                                break;
                            case 2:
                                completionBox.text += ""Fail1"";
                                soundBox.text = ""Incorrect"";
                                break;
                            case 3:
                                completionBox.text += ""Fail2"";
                                soundBox.text = ""Incorrect"";
                                break;
                        }
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
            if (!newCode.Contains("CrabName") || !newCode.Contains("CrabSpeed"))
            {
                // Inform the player they need to use certain variables
                programOutput.text = "Sorry, but you must declare and use the variables CrabName and CrabSpeed.";
                throw new Exception("Sorry, but you must declare and use the variables CrabName and CrabSpeed.");
            }

            // Test that the user used a switch-statement.
            if (!newCode.Contains("switch"))
            {
                programOutput.text = "Sorry, but you must use a switch-statement for this level.";
                throw new Exception("Sorry, but you must use a switch-statement for this level.");
            }

            // Strip out the player's variable declaration section and put it in the declarations variable
            startIndex = 0; // Start of input text
            endIndex = newCode.IndexOf("switch"); // Player Code starts at switch
            declarations = newCode[startIndex..endIndex];

            // Remove the declarations section from the player code
            newCode = newCode.Remove(startIndex, (endIndex - startIndex));

            return newCode;
        }

        // This function, StartRace, will count down
        // on the output box a timer. At the end of the timer,
        // the Crabs will race to the finish line with the desired
        // speed.
        private IEnumerator StartRace()
        {
            // Get the starting crab location for reset.
            Vector3 startLocation = Crabs[0].transform.position;
            int finish = 0;

            // Dramatic effect text, show racers
            programOutput.text = "The Race will begin in: ";
            Crabs[0].SetActive(true);
            Crabs[1].SetActive(true);
            Crabs[2].SetActive(true);
            Crabs[3].SetActive(true);
            yield return new WaitForSecondsRealtime(1f);

            // Start the Countdown
            for (int i = 3; i > 0; --i)
            {
                // Count down from 3 each second
                programOutput.text += i.ToString() + " ";
                yield return new WaitForSecondsRealtime(1f);
            }

            // Set race text
            programOutput.text = "GO";

            // Loop through the distance until the finish line
            for (int i = 0; i < 680; ++i)
            {
                // Move each Crab forward by movement speed
                Crabs[0].transform.Translate(float.Parse(speeds[0]), 0, 0);
                Crabs[1].transform.Translate(float.Parse(speeds[1]), 0, 0);
                Crabs[2].transform.Translate(float.Parse(speeds[2]), 0, 0);
                Crabs[3].transform.Translate(float.Parse(speeds[3]), 0, 0);

                // Check the location of each crab. If greater than finish line, set inactive
                for (int j = 0; j < 4; ++j)
                {
                    // Location of the Finish Line
                    if (Crabs[j].transform.position.x > 1371 && Crabs[j].activeInHierarchy)
                    {
                        Crabs[j].SetActive(false);
                        finish++; // Add one for track of how many crabs still running
                    }
                }

                // If all the Crabs are finished, end the race
                if (finish == 4)
                {
                    // Set i to 680 instead of using break inside loop
                    i = 680;
                }

                // Yield statement allows the Crab to simulate moving through space in frames
                yield return new WaitForSeconds(0.007f);
            }

            // At end of race, reset Crabs
            Crabs[0].transform.position = startLocation;
            Crabs[1].transform.position = startLocation;
            Crabs[2].transform.position = startLocation;
            Crabs[3].transform.position = startLocation;

            // Set the output box
            switch (speeds[4])
            {
                case "Winner":
                    programOutput.text = "Clamps WON!!! Amazing job Bit :)";
                    break;

                case "Fail1":
                    programOutput.text = "Oh no! Clamps did not win. Try again Bit :(";
                    break;

                case "Fail2":
                    programOutput.text = "Soooo close! Clamps tied for first instead of winning. Try again Bit :|";
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