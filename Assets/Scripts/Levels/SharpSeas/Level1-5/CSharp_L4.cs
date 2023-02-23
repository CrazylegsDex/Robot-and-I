/*
 * This class uses the MonoCSharp Code Compiler
 * to check boolean truth values. This level
 * will allow the player to use the <, <=,
 * >, >=, != and == operators and check those
 * outputs with the desired output.
 * 
 * Date: 02/23/2023
 * Author: Robot and I Team
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;

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

                // When the script is added to the HostGameObject
                // and invoked, run the following code
                void Start()
                {
                    // Get references to the output text boxes
                    TMP_Text operators = GameObject.Find(""Available Operators"").GetComponent<TMP_Text>();
                    print(""operators.text"");
                    TMP_Text problemStatement = GameObject.Find(""Problem Statement"").GetComponent<TMP_Text>();
                    print(""problemStatement.text"");
                    TMP_Text result = GameObject.Find(""Result"").GetComponent<TMP_Text>();
                    print(""result.text"");
                    TMP_Text invalid = GameObject.Find(""Error Output"").GetComponent<TMP_Text>();
                    print(""invalid.text"");
                    print(""" + playerInput.text + @""");
                    problemStatement.text = ""Test1"";
                    result.text = ""true"";
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
            foreach (CompilerError error in result.Errors)
            {
                if (error.ErrorNumber == "CS1525")
                    programOutput.text += "Syntax error\n\n";
                else
                    // Use the following if you want error codes along with the error text.
                    // String.Format("Error ({0}): ({1})", error.ErrorNumber, error.ErrorText)
                    programOutput.text += error.ErrorText + "\n";
            }
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
