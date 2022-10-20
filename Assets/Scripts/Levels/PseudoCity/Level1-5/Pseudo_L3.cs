using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pseudo_L3 : MonoBehaviour
{
    public TMP_InputField aInput; // References the User's Input Field
    public TMP_InputField bInput;
    public TMP_InputField cInput;
    public TMP_InputField dInput;
    public TextMeshProUGUI aOutput; // References the TMP Output Field
    public TextMeshProUGUI bOutput;
    public TextMeshProUGUI cOutput;
    public TextMeshProUGUI dOutput;
    public TextMeshProUGUI aContext; // References the code TMP Textboxes around each Field
    public TextMeshProUGUI bContext;
    public TextMeshProUGUI cContext;
    public TextMeshProUGUI dContext;
    public TextMeshProUGUI eContext;
    public GameObject blockobject;//object block the ability to complete the level
    public void Code_Compiler()
    {
        //A
        int num = 0;//counts up everytime a try block receives valid input.
        int end;//get assigned a value to its related section.
        if (eContext.text == "Addition")//compares section titles
        {
            end = 0;
        }
        else if (eContext.text == "Subtraction")
        {
            end = 1;
        }
        else if (eContext.text == "Multiplication")
        {
            end = 2;
        }
        else if (eContext.text == "Division")
        {
            end = 3;
        }
        else
        {
            end = 4;
        }
        double a, b, c, d;//Input values

        a = b = c = d = 0;
        
        bool safe = true;//goes false if the input in the try blocks is invalid
        try
        {// Save Text from input field into user input
            a = double.Parse(aInput.text);//tests for doubles
        }
        catch (Exception ex)//activates when the input is invalid
        {
            aOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {//Correct double inputed for section
            if (end == 0 && a == 3.0)//new value for each section
            {
                aOutput.text = "Correct!";
                num++;
            }
            else if (end == 1 && a == 1)
            {
                aOutput.text = "Correct!";
                num++;
            }
            else if (end == 2 && a == 49)
            {
                aOutput.text = "Correct!";
                num++;
            }
            else if (end == 3 && a == .5)
            {
                aOutput.text = "Correct!";
                num++;
            }
            else if (end == 4 && a == 2)
            {
                aOutput.text = "Correct!";
                num++;
            }
            else//Wrong double inputed in sectiion
            {
                aOutput.text = "Incorrect";
            }

        }
        //B
        safe = true;//resets safe for next input
        try
        {
            b = double.Parse(bInput.text);
        }
        catch (Exception ex)
        {
            bOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (end == 0 && b == 6)
            {
                bOutput.text = "Correct!";
                num++;
            }
            else if (end == 1 && b == 8)
            {
                bOutput.text = "Correct!";
                num++;
            }
            else if (end == 2 && b == 37)
            {
                bOutput.text = "Correct!";
                num++;
            }
            else if (end == 3 && b == 0)
            {
                bOutput.text = "Correct!";
                num++;
            }
            else if (end == 4 && b == 2)
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
            c = double.Parse(cInput.text);
        }
        catch (Exception ex)
        {
            cOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (end == 0 && c == 3.3)
            {
                cOutput.text = "Correct!";
                num++;
            }
            else if (end == 1 && c == -1.2)
            {
                cOutput.text = "Correct!";
                num++;
            }
            else if (end == 2 && c == 9)
            {
                cOutput.text = "Correct!";
                num++;
            }
            else if (end == 3 && c == 9.5)
            {
                cOutput.text = "Correct!";
                num++;
            }
            else if (end == 4 && c == 2)
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
            d = double.Parse(dInput.text);
        }
        catch (Exception ex)
        {
            dOutput.text = "Invalid";
            safe = false;
            Debug.Log(ex.Message);

        }
        if (safe)
        {
            if (end == 0 && d == 28)
            {
                dOutput.text = "Correct!";
                num++;
            }
            else if (end == 1 && d == 3.4)
            {
                dOutput.text = "Correct!";
                num++;
            }
            else if (end == 2 && d == 52)
            {
                dOutput.text = "Correct!";
                num++;
            }
            else if (end == 3 && d == 6)
            {
                dOutput.text = "Correct!";
                num++;
            }
            else if (end == 4 && d == 2)
            {
                dOutput.text = "Correct!";
                num++;
            }
            else
            {
                dOutput.text = "Incorrect";
            }

        }
        if (num == 4 && end == 4)//allows the level to be completed after last section is all correct.
        {
            Debug.Log("works");
            blockobject.SetActive(false); //hides the platform blocking level progress
        }
        else if (num == 4 && end == 0)//Updates code text after first section is all correct.
        {
            aContext.text = "A. x= 2-1\noutput(x)\n\noutput:";
            bContext.text = "B. x= 20-5\ny= 3+4 \nx-=y\noutput(x)\n\noutput:";
            cContext.text = "C. x= 5.5\ny= 6.7 \ny-=x\noutput(y)\n\noutput:";
            dContext.text = "D. x= 10+5\ny= 5-3.3 \nz=x-y\ny= y-z+x\noutput(y)\n\noutput:";
            eContext.text = "Subtraction"; 
        }
        else if (num == 4 && end == 1)//Updates code text after second section is all correct.
        {
            aContext.text = "A. x= 2+5\ny= 10-3\nx*=y\noutput(x)\n\noutput:";
            bContext.text = "B. x= 5*3\ny= 10*2.2 \nx+=y\noutput(x)\n\noutput:";
            cContext.text = "C. x= 1+2*3\ny= (1+2)*3 \nz=-1*(1+2*3)\noutput(z+y+x)\n\noutput:";
            dContext.text = "D. x= 3*4-1\ny= 4+1*5 \nz=2*2+2\ny= 2*(y+z+x)\noutput(y)\n\noutput:";
            eContext.text = "Multiplication";
        }
        else if (num == 4 && end == 2)//Updates code text after third section is all correct.
        {
            aContext.text = "A. x= 2.0/4\noutput(x)\n\noutput:";
            bContext.text = "B. y= 2/4\noutput(y)\n\noutput:";
            cContext.text = "C. x= 1.1*3\ny= 2.2+5 \nz=  10.5-2\nz+= (x+y)\noutput(z/2)\n\noutput:";
            dContext.text = "D. x= 3*3\ny= 2+5 \nz= (x+y)*2\nz+=1\noutput(z/5)\n\noutput:";
            eContext.text = "Division";
        }
        else if (num == 4 && end == 3)//Updates code text after fouth section is all correct.
        {
            aContext.text = "A. x= 2%4\noutput(x)\n\noutput:";
            bContext.text = "B. x= 3%2+1\noutput(x)\n\noutput:";
            cContext.text = "C. x= 3.3*3+3%2\ny= 20%7 \nz= x+y-.9\noutput(z/8)\n\noutput:";
            dContext.text = "D. x= 24%5\ny= 15%2 \nz=5%x+y\noutput(z)\n\noutput:";
            eContext.text = "Modulus";
        }

    }
}
