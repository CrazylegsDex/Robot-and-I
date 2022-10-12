using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class String_test : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField userInputS; // References the User's Input Field
    public TextMeshProUGUI programOutputS; // References the TMP Output Field
    public void Code_Compiler()
    {
        // Save Text from input field into user input
        string path = Application.dataPath + "/Scripts/test.txt";
        bool safe = true;
        try
        {
            string s = userInputS.text;
        }
        catch (Exception ex)
        {
            programOutputS.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);
            string bit = "0";
            File.AppendAllText(path, bit);

        }
        if (safe)
        {
            programOutputS.text = "Correct!";
            string bit = "1";
            File.AppendAllText(path, bit);

        }



    }
}
