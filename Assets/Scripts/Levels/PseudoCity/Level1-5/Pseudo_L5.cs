/*
 * This script allows the player the ability to use the
 * CSharp compiler. This script will allow the player to
 * type into a TMP text box and then it will use that
 * information so the player can complete the level.
 * 
 * Author: Robot and I Team
 * Last modification date: 12-27-2022
 */

using UnityEngine;
using Modified.Mono.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using TMPro;

namespace PseudoLevels
{
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

        // This function is the driver to the compilation sequence
        public void MainDriver()
        {
            // Local variables
            Assembly resultAssembly;
            Type runtimeClass;
            MethodInfo runtimeFunction;
            Func<GameObject, MonoBehaviour> runtimeDelegate;

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
			        TMP_Text object1 = GameObject.Find(""OutputBoxA"").GetComponent<TMP_Text>();

                    int x = 0;
                    int y = 0;
                    int num = 0;
                    bool a;
                    bool z;
                    bool safe = true;
                    double state = 0;
                    
			        " + playerInput1.text + @";
                    " + playerInput8.text + @";
                    try{
                        state = 2/x;
                        state = 2/y;
                    }
                    catch(Exception ex){
                        object1.text = """";
                        safe = false; 
                    }


                    if(safe){
                        z = x>3 && x<5;
                        a = y<7 && y>3;
			            if (z&&a){
                            object1.color = new Color32(0, 255, 255, 255);//changes font color to cyan
				            object1.text = ""correct!"";
                            num++;
                        }
			            else{
                            object1.color = new Color32(255, 200, 0, 255);//changes font color to yellow
				            object1.text = ""incorrect"";
                        }
                    }
                    
                    x = 0;
                    y = 0;
                    safe = true;
                    
			        TMP_Text object2 = GameObject.Find(""OutputBoxB"").GetComponent<TMP_Text>();

                    " + playerInput2.text + @";
                    " + playerInput3.text + @";

                    try{
                        state = 2/x;
                        state = 2/y;
                    }
                    catch(Exception ex){
                        object2.text = """";
                        safe = false; 
                    }

                    if(safe){
			            z = x<3 || x>3;
                        a = y>=7 || y<=5;

			            if (!(z||a)){
                            object2.color = new Color32(0, 255, 255, 255);//changes font color to cyan
				            object2.text = ""correct!"";
                            num++;
                        }
			            else{
                            object2.color = new Color32(255, 200, 0, 255);//changes font color to yellow
				            object2.text = ""incorrect"";
                        }
                    }
                    x = 0;
                    y = 0;
                    safe = true;
                    TMP_Text object3 = GameObject.Find(""OutputBoxC"").GetComponent<TMP_Text>();
                    
                    " + playerInput4.text + @";
                    " + playerInput5.text + @";

                    try{
                        state = 2/x;
                        state = 2/y;
                    }
                    catch(Exception ex){
                        object3.text = """";
                        safe = false; 
                    }

                    if(safe){
                        z= x!=y && x>2 && x<5;
			            a = y>3 && y<6 && y!=5;
			            if (z){
                            object3.color = new Color32(0, 255, 255, 255);//changes font color to cyan
				            object3.text = ""correct!"";
                            num++;
                        }
			            else{
                            object3.color = new Color32(255, 200, 0, 255);//changes font color to yellow
				            object3.text = ""incorrect"";
                        }
                    }
                    x = 0;
                    y = 0;
                    safe = true;
                    TMP_Text object4 = GameObject.Find(""OutputBoxD"").GetComponent<TMP_Text>();
                    
                    " + playerInput6.text + @";
                    " + playerInput7.text + @";

                    try{
                        state = 2/x;
                        state = 2/y;
                    }
                    catch(Exception ex){
                        object4.text = """";
                        safe = false; 
                    }
                    if(safe){
                        z = x%4==3 && x>4;
                        a = y/x==5 || y+x==9;
			            if (z||a){
                            object4.color = new Color32(0, 255, 255, 255);//changes font color to cyan
				            object4.text = ""correct!"";
                            num++;
                        }
			            else{
                            object4.color = new Color32(255, 200, 0, 255);//changes font color to yellow
				            object4.text = ""incorrect"";
                        }
                    }
                    
                    if(num == 4){
                        GameObject NPC = GameObject.FindWithTag(""LevelChange"");
                        NPC.GetComponent<BoxCollider2D>().isTrigger = true;
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
                parameters.ReferencedAssemblies.Add(win32Location + "System.dll");
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
                parameters.ReferencedAssemblies.Add(folderPath + "System.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.CoreModule.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.UI.dll");
		        parameters.ReferencedAssemblies.Add(folderPath + "UnityEngine.Physics2DModule.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "Microsoft.CSharp.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "netstandard.dll");
                parameters.ReferencedAssemblies.Add(folderPath + "Unity.TextMeshPro.dll");
            }

            // Set compiler parameters
            // NOTE: Set "IncludeDebugInformation" to false when pushed into production
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.IncludeDebugInformation = false;

            // Compile the sourceCode 
            result = compiler.CompileAssemblyFromSource(parameters, sourceCode);

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
            }
            // Return the assembly
            return result.CompiledAssembly;
        }
    }
}
