/*
 * This script checks the input text boxes and ensures that the player
 * put the correct code into the box
 * 
 * Author: Robot and I Team
 * Credits: DMGregory at Stackexchange.com - His helpful code example
 *     allowed this project to succeed in its efforts.
 * Last modification date: 10-17-2022
 */
using System;
using System.CodeDom.Compiler;
//using System.CodeDom.Compiler.CompilerError;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.CSharp;
using TMPro;

public class Pseudo_L5 : MonoBehaviour
{
    // Public variables
    public TMP_InputField playerInput1; // References the Player's Input Field
    public TMP_InputField playerInput2;
    public TMP_InputField playerInput3;
    public TMP_InputField playerInput4;
    public TMP_InputField playerInput5;
    public TMP_InputField playerInput6;
    public TMP_InputField playerInput7;
    public TMP_InputField playerInput8;

    //private bool displayLog;
    // This function is the driver to the compilation sequence
    public void MainDriver()
    {
        // Local variables
        Assembly resultAssembly;
        Type runtimeClass;
        MethodInfo runtimeFunction;
        Func<GameObject, MonoBehaviour> runtimeDelegate;
        //string CheckText = playerInput1.text;

        // Check that the player is using our variables
        /*if (CheckText == "x")
            programOutput1.text = "Good";
        else
            programOutput1.text = "Bad";*/
        //z = " + bool.Parse(playerInput1.text) + @";
        // Add the player's code to a template for runtime scripting
        string playerCode = @"    
            using UnityEngine;
            using TMPro;
            using System;
	        
	        
            public class RuntimeScript : MonoBehaviour
            {
                
                
                // This function adds a script to the host object
                // This script addition is required so that Unity can run
                // it during runtime.
                public static RuntimeScript AddYourselfTo(GameObject host)
                {
                    // Add RuntimeScript to the host object
                    return host.AddComponent<RuntimeScript>();
                }

                 void Start()
                {
                    // Great one-liner so you don't have to create three different variables
			        TMP_Text object1 = GameObject.Find(""OutputBoxA"").GetComponent<TMP_Text>();

                    int x = 4;
                    int y = 5;
                    int num = 0;
                    
			        " + playerInput1.text + @";
                    " + playerInput8.text + @";
                    bool z = x>3 && x<5;
                    bool a = y<7 && y>3;
			        if (z&&a){
				        object1.text = ""correct!"";
                        num++;
                    }
			        else{
                        
				        object1.text = ""incorrect"";
                    }
                    
                    
                    // Great one-liner so you don't have to create three different variables
			        TMP_Text object2 = GameObject.Find(""OutputBoxB"").GetComponent<TMP_Text>();

                    " + playerInput2.text + @";
                    " + playerInput3.text + @";

			        z = x<3 || x>3;
                    a = y>=7 || y<=5;

			        if (!(z||a)){
				        object2.text = ""correct!"";
                        num++;
                    }
			        else{
                        
				        object2.text = ""incorrect"";
                    }
                    x = 4;
                    y = 5;
                    TMP_Text object3 = GameObject.Find(""OutputBoxC"").GetComponent<TMP_Text>();

                    " + playerInput4.text + @";
                    " + playerInput5.text + @";

                    z= x!=y && x>2 && x<5;
			        a = y>3 && y<6 && y!=5;
			        if (z){
				        object3.text = ""correct!"";
                        num++;
                    }
			        else{
                        
				        object3.text = ""incorrect"";
                    }
                    x = 4;
                    y = 5;
                    TMP_Text object4 = GameObject.Find(""OutputBoxD"").GetComponent<TMP_Text>();

                    " + playerInput6.text + @";
                    " + playerInput7.text + @";

                    z = x%4==3 && x>4;
                    a = y/x==5 || y+x==9;
                    print(z);
                    print(a);
			        if (z||a){
				        object4.text = ""correct!"";
                        num++;
                    }
			        else{
                        
				        object4.text = ""incorrect"";
                    }
                    
                    if(num == 4){
                        //GameObject NPC = GameObject.FindWithTag(""ObjectMove"");
                        //NPC.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    Destroy(gameObject.GetComponent<RuntimeScript>());
                }              
    
            }";

        // Compile the player's code and check for syntax issues
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
     * all the errors to the console.
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
        string path1 = @"Data\PlaybackEngines\windowsstandalonesupport\Variations\win32_player_development_mono\Data\Managed\";
        string path2 = @"Data\Resources\PackageManager\ProjectTemplates\libcache\com.unity.template.2d-7.0.1\ScriptAssemblies\";
        string assemblyLocation = parameters.ReferencedAssemblies.GetType().Assembly.Location;
        string win32Location = assemblyLocation.Substring(0, assemblyLocation.IndexOf("System.dll")); // Snip off the "System.dll" information
        string engineLocation = assemblyLocation.Substring(0, assemblyLocation.IndexOf("Data")); // Extract base location for Data folder
        parameters.ReferencedAssemblies.Add(win32Location + "System.dll");
        parameters.ReferencedAssemblies.Add(engineLocation + path1 + "UnityEngine.CoreModule.dll");
        parameters.ReferencedAssemblies.Add(engineLocation + path2 + "UnityEngine.UI.dll");
        parameters.ReferencedAssemblies.Add(engineLocation + path2 + "Unity.TextMeshPro.dll");
        parameters.ReferencedAssemblies.Add(win32Location + "Microsoft.CSharp.dll");
        parameters.ReferencedAssemblies.Add(win32Location + "Facades\\netstandard.dll");


        // Set compiler parameters
        // NOTE: Set "IncludeDebugInformation" to false when pushed into production
        parameters.GenerateExecutable = false;
        parameters.GenerateInMemory = true;
        parameters.IncludeDebugInformation = false;

        // Compile the sourceCode 
        result = provider.CompileAssemblyFromSource(parameters, sourceCode);

        // Check if there were compilation errors
        if (result.Errors.HasErrors)
        {
            //displayLog = false;
            foreach (CompilerError error in result.Errors)
            {
                if (error.ErrorNumber == "CS1525")
                    Debug.Log("Syntax error\n\n");
                else
                    // Use the following if you want error codes along with the error text.
                    // String.Format("Error ({0}): ({1})", error.ErrorNumber, error.ErrorText)
                    Debug.Log(error.ErrorText + "\n");
            }
            //Debug.Log(playerInput1.text);
        }
        // Return the assembly
        return result.CompiledAssembly;
    }
}