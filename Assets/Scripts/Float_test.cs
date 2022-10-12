using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Float_test : MonoBehaviour
{
    public TMP_InputField userInputF; // References the User's Input Field
    public TextMeshProUGUI programOutputF; // References the TMP Output Field
    public void Code_Compiler()
    {
        string path = Application.dataPath + "/Scripts/test.txt";
        // Save Text from input field into user input
        bool safe = true;
        try
        {
            float i = float.Parse(userInputF.text);
        }
        catch (Exception ex)
        {
            programOutputF.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);
            string bit = "0";
            File.AppendAllText(path, bit);

        }
        if (safe)
        {
            programOutputF.text = "Correct!";
            string bit = "1";
            File.AppendAllText(path, bit);
        }



    }
}
