using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Char_test : MonoBehaviour
{
    public TMP_InputField userInput; // References the User's Input Field
    public TextMeshProUGUI programOutput; // References the TMP Output Field
    public void Code_Compiler()
    {
        string path = Application.dataPath + "/Scripts/test.txt";
        //if(!File.Exists(path)){
        File.WriteAllText(path, " ");
        // }

        // Save Text from input field into user input
        bool safe = true;
        try
        {
            char i = char.Parse(userInput.text);
        }
        catch (Exception ex)
        {
            programOutput.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);
            string bit = "0";
            File.AppendAllText(path, bit);

        }
        if (safe)
        {

            programOutput.text = "Correct!";
            string bit = "1";
            File.AppendAllText(path, bit);
        }

    }
}
