using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Python_L1 : MonoBehaviour
{
    public TMP_InputField userInput; // References the User's Input Field
    public TMP_InputField stringInput;
    public TMP_InputField boolInput;
    public TMP_InputField floatInput;
    public TextMeshProUGUI programOutput; // References the TMP Output Field
    public TextMeshProUGUI stringOutput;
    public TextMeshProUGUI boolOutput;
    public TextMeshProUGUI floatOutput;
    public GameObject blockobject;//object block the ability to complete the level
    public void Code_Compiler()
    {
        //Ints
        int num = 0;//counts up everytime a try block receives valid input.


        bool safe = true;//goes false if the input in the try blocks is invalid
        if (!(String.IsNullOrEmpty(userInput.text)))//Checks if values were inputed skips if no value
        {
            try
            { // Saves Text from input field into user input
                int i = int.Parse(userInput.text);//tests for only integers
            }
            catch (Exception ex)//activates when the input is invalid
            {
                programOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow //R,G,B, Transparency. 
                programOutput.text = "Incorrect";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                programOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                programOutput.text = "Correct!";
                num++;

            }
        }

        //String
        safe = true;//resets safe for next input
        if (!(String.IsNullOrEmpty(stringInput.text))) //Checks if values were inputed skips if no value
        {
            try
            {
                string s = stringInput.text;//tests for strings
            }
            catch (Exception ex)
            {
                stringOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                stringOutput.text = "Incorrect";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                string s = stringInput.text;

                if (s.StartsWith("\"") && s.EndsWith("\"") && s.Length != 1)
                {
                    stringOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    stringOutput.text = "Correct!";
                    num++;
                }
                else
                {
                    stringOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    stringOutput.text = "Incorrect";
                }
            }
        }
        //bool
        safe = true;
        if (!(String.IsNullOrEmpty(boolInput.text)))//Checks if values were inputed skips if no value
        {
            try
            {
                bool i = bool.Parse(boolInput.text);//tests for bools
            }
            catch (Exception ex)
            {//below makes the use of 1 or 0 for bool acceptable.
                if (1 == int.Parse(boolInput.text) || 0 == int.Parse(boolInput.text))
                {
                    boolOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    boolOutput.text = "Correct!";
                    num++;
                }
                else
                {
                    boolOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    boolOutput.text = "Incorrect";

                }
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                boolOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                boolOutput.text = "Correct!";
                num++;
            }
        }
        safe = true;
        //float
        if (!(String.IsNullOrEmpty(floatInput.text)))//Checks if values were inputed skips if no value
        {
            try
            {
                float i = float.Parse(floatInput.text);//tests for floats
            }
            catch (Exception ex)
            {
                floatOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                floatOutput.text = "Incorrect";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                floatOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                floatOutput.text = "Correct!";
                num++;
            }
        }

        if (num == 4)
        {
            Debug.Log("works");
            blockobject.SetActive(false); //hides the platform blocking level progress
        }
    }
}
