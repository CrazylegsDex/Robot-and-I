using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bool_test : MonoBehaviour
{
    public TMP_InputField userInputB; // References the User's Input Field
    public TextMeshProUGUI programOutputB; // References the TMP Output Field
    public void Code_Compiler()
    {
        string path = Application.dataPath + "/Scripts/test.txt";
        // Save Text from input field into user input
        bool safe = true;
        try
        {
            bool i = bool.Parse(userInputB.text);
        }
        catch (Exception ex)
        {
            programOutputB.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);
            string bit = "0";
            File.AppendAllText(path, bit);

        }
        if (safe)
        {
            programOutputB.text = "Correct!";
            string bit = "1";
            File.AppendAllText(path, bit);
        }



    }
}
