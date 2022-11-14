// This script provides control and completion for CSharp levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace CSharpLevels
{
    public class CSharp_L1 : MonoBehaviour
    {
        public TMP_InputField userInput; // References the User's Input Field
        public TMP_InputField charInput;
        public TMP_InputField stringInput;
        public TMP_InputField boolInput;
        public TMP_InputField floatInput;
        public TMP_InputField doubleInput;

        public TextMeshProUGUI programOutput; // References the TMP Output Field
        public TextMeshProUGUI charOutput;
        public TextMeshProUGUI stringOutput;
        public TextMeshProUGUI boolOutput;
        public TextMeshProUGUI floatOutput;
        public TextMeshProUGUI doubleOutput;
        public BoxCollider2D levelSprite;

        public void Code_Compiler()
        {
            //Ints
            int num = 0;//counts up everytime a try block receives valid input.

            Debug.Log(userInput.text);
            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(userInput.text)))//Checks if values were inputed skips if no value
            {
                try
                { // Saves Text from input field into user input
                    int i = int.Parse(userInput.text);//tests for only integers
                }
                catch (Exception ex)//activates when the input is invalid
                {
                    programOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
                    programOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
            //Char
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(charInput.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    string c = charInput.text;//tests for strings
                }
                catch (Exception ex)
                {
                    stringOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    charOutput.text = "Incorrect";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    string c = charInput.text;
                    if (c.StartsWith("\'") && c.EndsWith("\'") && c.Length == 3)
                    {
                        stringOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                        charOutput.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        stringOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        charOutput.text = "Incorrect";
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
            //Char
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(doubleInput.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    double d = double.Parse(doubleInput.text);//tests for strings
                }
                catch (Exception ex)
                {
                    doubleOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    doubleOutput.text = "Incorrect";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    doubleOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    doubleOutput.text = "Correct!";
                    num++;
                }
            }

            if (num == 6)
            {
                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
            }
            else
            {
                levelSprite.isTrigger = false;
            }
        }
    }
}