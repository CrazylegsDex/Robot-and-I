using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Csharp_L2 : MonoBehaviour
{
    public TMP_InputField aInput; // References the User's Input Field
    public TMP_InputField bInput;
    public TMP_InputField cInput;
    public TMP_InputField dInput;
    public TextMeshProUGUI aOutput; // References the TMP Output Field
    public TextMeshProUGUI bOutput;
    public TextMeshProUGUI cOutput;
    public TextMeshProUGUI dOutput;
    public GameObject blockobject;//object block the ability to complete the level
    public void Code_Compiler()
    {
        //A
        int num = 0;//counts up everytime a try block receives valid input.
        int a, b, c, d;//Input values
        a = b = c = d = 0;

       bool safe = true;//goes false if the input in the try blocks is invalid
        if (!(String.IsNullOrEmpty(aInput.text)))//Checks if values were inputed skips if no value
        {
            try
            {// Save Text from input field into user input
                a = int.Parse(aInput.text);//tests for only integers
            }
            catch (Exception ex)//activates when the input is invalid
            {
                aOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                aOutput.text = "Invalid";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                if (a == 1)
                { //Correct integer inputed
                    aOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    aOutput.text = "Correct!";
                    num++;
                }
                else//Wrong integer inputed
                {
                    aOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    aOutput.text = "Incorrect";
                }

            }
        }
        //B
        safe = true;//resets safe for next input
        if (!(String.IsNullOrEmpty(bInput.text)))//Checks if values were inputed skips if no value
        {
            try
            {
                b = int.Parse(bInput.text);//tests for only integers
            }
            catch (Exception ex)
            {
                bOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                bOutput.text = "Invalid";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                if (b == 4)
                {
                    bOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    bOutput.text = "Correct!";
                    num++;
                }
                else
                {
                    bOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    bOutput.text = "Incorrect";
                }

            }
        }
        //C
        safe = true;
        if (!(String.IsNullOrEmpty(cInput.text)))//Checks if values were inputed skips if no value
        {
            try
            {
                c = int.Parse(cInput.text);//tests for only integers
            }
            catch (Exception ex)
            {
                cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                cOutput.text = "Invalid";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                if (c == 97)
                {
                    cOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    cOutput.text = "Correct!";
                    num++;
                }
                else
                {
                    cOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    cOutput.text = "Incorrect";
                }

            }
        }
        //D
        safe = true;
        if (!(String.IsNullOrEmpty(dInput.text)))//Checks if values were inputed skips if no value
        {
            try
            {
                d = int.Parse(dInput.text);//tests for only integers
            }
            catch (Exception ex)
            {
                dOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                dOutput.text = "Invalid";
                safe = false;
                Debug.Log(ex.Message);

            }
            if (safe)
            {
                if (d == 15)
                {
                    dOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    dOutput.text = "Correct!";
                    num++;
                }
                else
                {
                    dOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    dOutput.text = "Incorrect";
                }

            }
        }

        if (num == 4)
        {
            Debug.Log("works");
            blockobject.SetActive(false); //hides the platform blocking level progress
        }
    }
}
