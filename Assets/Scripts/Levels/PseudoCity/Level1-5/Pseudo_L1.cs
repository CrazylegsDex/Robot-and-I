using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pseudo_L1 : MonoBehaviour
{
    public TMP_InputField userInput; // References the User's Input Field
    public TMP_InputField stringInput;
    public TMP_InputField boolInput;
    public TMP_InputField floatInput;
    public TextMeshProUGUI programOutput; // References the TMP Output Field
    public TextMeshProUGUI stringOutput; 
    public TextMeshProUGUI boolOutput; 
    public TextMeshProUGUI floatOutput;
    public BoxCollider2D levelSprite;


    public void Code_Compiler()
    {
        //Ints
        int num = 0;//counts up everytime a try block receives valid input.

        
        bool safe = true;//goes false if the input in the try blocks is invalid
        try
        { // Saves Text from input field into user input
            int i = int.Parse(userInput.text);//tests for only integers
        }
        catch (Exception ex)//activates when the input is invalid
        {
            programOutput.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {

            programOutput.text = "Correct!";
            num++;
 
        }
        //String
        safe = true;//resets safe for next input
        try
        {
            string s = stringInput.text;//tests for strings
        }
        catch (Exception ex)
        {
            stringOutput.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            string s = stringInput.text;
            if (s.StartsWith("\"") && s.EndsWith("\""))
            {
                stringOutput.text = "Correct!";
                num++;
            }
            else
                stringOutput.text = "Incorrect";
        }
        //bool
        safe = true;
        try
        {
            bool i = bool.Parse(boolInput.text);//tests for bools
        }
        catch (Exception ex)
        {//below makes the use of 1 or 0 for bool acceptable.
            if (1 == int.Parse(boolInput.text) || 0 == int.Parse(boolInput.text))
            {
                boolOutput.text = "Correct!";
                num++;
                safe = false;
            }
            else
            {
                boolOutput.text = "Incorrect";
                safe = false;
            }
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            boolOutput.text = "Correct!";
            num++;
        }
        safe = true;
        //float
        try
        {
            float i = float.Parse(floatInput.text);//tests for floats
        }
        catch (Exception ex)
        {
            floatOutput.text = "Incorrect";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            floatOutput.text = "Correct!";
            num++;
        }
        if (num == 4)
        {
            levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
        }
    }
}
