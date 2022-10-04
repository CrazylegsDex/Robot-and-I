using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Compile_Code : MonoBehaviour
{
    public TMP_InputField userInput; // References the User's Input Field
    public TextMeshProUGUI programOutput; // References the TMP Output Field
    public void Code_Compiler()
    {
        // Save Text from input field into user input
        programOutput.text = userInput.text;
    }
}
