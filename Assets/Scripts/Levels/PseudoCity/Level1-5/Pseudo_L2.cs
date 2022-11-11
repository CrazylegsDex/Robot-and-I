using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pseudo_L2 : MonoBehaviour
{
    public TMP_InputField aInput; // References the User's Input Field
    public TMP_InputField bInput;
    public TMP_InputField cInput;
    public TMP_InputField dInput;
    public TextMeshProUGUI aOutput; // References the TMP Output Field
    public TextMeshProUGUI bOutput; 
    public TextMeshProUGUI cOutput;
    public TextMeshProUGUI dOutput;
    public BoxCollider2D levelSprite;
    public void Code_Compiler()
    {
        //A
        int num = 0;//counts up everytime a try block receives valid input.
        int a, b, c, d;//Input values
        a = b = c = d = 0;
        
        bool safe = true;//goes false if the input in the try blocks is invalid
        try
        {// Save Text from input field into user input
            a = int.Parse(aInput.text);//tests for only integers
        }
        catch (Exception ex)//activates when the input is invalid
        {
            aOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (a == 1) 
            { //Correct integer inputed
                aOutput.text = "Correct!";
                num++;
            }
            else//Wrong integer inputed
            {
                aOutput.text = "Incorrect";
            }

        }
        //B
        safe = true;//resets safe for next input
        try
        {
            b = int.Parse(bInput.text);//tests for only integers
        }
        catch (Exception ex)
        {
            bOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (b == 4)
            {
                bOutput.text = "Correct!";
                num++;
            }
            else
            {
                bOutput.text = "Incorrect";
            }

        }
        //C
        safe = true;
        try
        {
            c = int.Parse(cInput.text);//tests for only integers
        }
        catch (Exception ex)
        {
            cOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (c == 1)
            {
                cOutput.text = "Correct!";
                num++;
            }
            else
            {
                cOutput.text = "Incorrect";
            }

        }
        //D
        safe = true;
        try
        {
            d = int.Parse(dInput.text);//tests for only integers
        }
        catch (Exception ex)
        {
            dOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (d == 3)
            {
                dOutput.text = "Correct!";
                num++;
            }
            else
            {
                dOutput.text = "Incorrect";
            }

        }

            if (num == 4)
        {
            levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
        }
    }
}
