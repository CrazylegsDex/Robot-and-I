/*
 * This script is the driver for checking and acting upon
 * input text for a TMP_InputField
 * This script has two primary methods for doing this.
 * 
 * 1. Get the input from the InputTextBox and compile it
 * 2. Act upon the results from the compilation.
 * 
 * Author: Robot and I Team
 * Credits: All credit for the Python Engine goes to the developers
 * of the IronPython3 GitHub repository. Thanks to everyone who has
 * pitched in to help this project succeed.
 * Last modification date: 10-24-2022
 */

using UnityEngine;
using IronPython.Hosting;
using TMPro;

public class PythonCodeCompiler : MonoBehaviour
{
    // Public variables
    public TMP_InputField playerInput; // References the Player's Input Field
    public TextMeshProUGUI programOutput; // References the TMP Output Field

    /*
     * This function is the driver to the sequence of events that are
     * required to compile and execute upon Python code
     */
    public void MainDriver()
    {
        // Local variables
        Microsoft.Scripting.Hosting.ScriptEngine scriptEngine;
        Microsoft.Scripting.Hosting.ScriptScope scriptScope;
        dynamic scriptFunction;

        // Add the player's code to a defined python function for runtime running
        string playerCode = @"
# Since Python delimits by whitespace, this is where code must start
# for whitespace to be properly delimitted.

def main():
    print('Where does this go?')
    print('If I can find this, I can pick it up?')
    return 'Hello World!'
";

        // Create the engine and define a scope
        scriptEngine = Python.CreateEngine();
        scriptScope = scriptEngine.CreateScope();

        // Execute the player's code using the engine and scope
        scriptEngine.Execute(playerCode, scriptScope);

        // If no compile errors, get the output from the function
        scriptFunction = scriptScope.GetVariable("main");
        Debug.Log(scriptFunction());

        // For temporary purposes
        programOutput.text = "Currently unemplemented.\n\n";
        programOutput.text += playerInput.text;
    }
}
