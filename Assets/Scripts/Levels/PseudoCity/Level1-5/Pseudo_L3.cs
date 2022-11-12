// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
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
        public BoxCollider2D levelSprite;

        public void Code_Compiler()
        {
            int num = 0;//counts up everytime a try block receives valid input.
            int end;//get assigned a value to its related section.
            if (eContext.text == "Addition")//compares section titles
            {
                end = 0;//Assigns the section value for addition
            }
            else if (eContext.text == "Subtraction")
            {
                end = 1;//Assigns the section value for subtraction
            }
            else if (eContext.text == "Multiplication")
            {
                end = 2;//Assigns the section value for multiplication
            }
            else if (eContext.text == "Division")
            {
                end = 3;//Assigns the section value for division
            }
            else
            {
                end = 4;//Assigns the section value for modulus
            }
            double a, b, c, d;//Input values

            a = b = c = d = 0;
            //A section
            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(aInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {// Save Text from input field into user input
                    a = double.Parse(aInput.text);//tests for doubles
                }
                catch (Exception ex)//activates when the input is invalid
                {
                    aOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    aOutput.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {//Correct double inputed for section
                    aOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (end == 0 && a == 3.0)//new value for each section 
                    {//Addition Section
                        aOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 1 && a == 1)
                    {//Subtraction Section
                        aOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 2 && a == 49)
                    {//Multiplication Section
                        aOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 3 && a == .5)
                    {//Division Section
                        aOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 4 && a == 2)
                    {//Modulus Section
                        aOutput.text = "Correct!";
                        num++;
                    }
                    else//Wrong double inputed in sectiion
                    {
                        aOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        aOutput.text = "Incorrect";
                    }
                }
            }
            else
            {
                aOutput.text = "";
            }
            //B section
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(bInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    b = double.Parse(bInput.text);
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
                    bOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (end == 0 && b == 6)
                    {//Addition Section
                        bOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 1 && b == 8)
                    {//Subtraction Section
                        bOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 2 && b == 37)
                    {//Multiplication Section
                        bOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 3 && b == 0)
                    {//Division Section
                        bOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 4 && b == 2)
                    {//Modulus Section
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
            else
            {
                bOutput.text = "";
            }
            //C section
            safe = true;
            if (!(String.IsNullOrEmpty(cInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    c = double.Parse(cInput.text);
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
                    cOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (end == 0 && c == 3.3)
                    {//Addition Section
                        cOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 1 && c == -1.2)
                    {//Subtraction Section
                        cOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 2 && c == 9)
                    {//Multiplication Section
                        cOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 3 && c == 9.5)
                    {//Division Section
                        cOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 4 && c == 2)
                    {//Modulus Section
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
            else
            {
                cOutput.text = "";
            }
            //D section
            safe = true;
            if (!(String.IsNullOrEmpty(dInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    d = double.Parse(dInput.text);
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
                    dOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (end == 0 && d == 28)
                    {//Addition Section
                        dOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 1 && d == 3.4)
                    {//Subtraction Section
                        dOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 2 && d == 52)
                    {//Multiplication Section
                        dOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 3 && d == 6)
                    {//Division Section
                        dOutput.text = "Correct!";
                        num++;
                    }
                    else if (end == 4 && d == 2)
                    {//Modulus Section
                        dOutput.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        cOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        dOutput.text = "Incorrect";
                    }
                }
            }
            else
            {
                dOutput.text = "";
            }
            if (num == 4 && end == 4)//allows the level to be completed after last section is all correct.
            {
                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
            }
            else if (num == 4 && end == 0)//Updates code text to the subtraction section after the addition section is all correct.
            {
                aContext.color = bContext.color = cContext.color = dContext.color = eContext.color = new Color32(0, 200, 255, 255); //R,G,B, Transparency. 
                aContext.text = "A. x= 2-1\noutput(x)\n\noutput:";
                bContext.text = "B. x= 20-5\ny= 3+4 \nx-=y\noutput(x)\n\noutput:";
                cContext.text = "C. x= 5.5\ny= 6.7 \nx-=y\noutput(y)\n\noutput:";
                dContext.text = "D. x= 10+5\ny= 5-3.3 \nz=x-y\ny= y-z+x\noutput(y)\n\noutput:";
                eContext.text = "Subtraction";
            }
            else if (num == 4 && end == 1)//Updates code text to the multiplication section after subtraction section is all correct.
            {
                aContext.color = bContext.color = cContext.color = dContext.color = eContext.color = new Color32(255, 128, 0, 255);
                aContext.text = "A. x= 2+5\ny= 10-3\nx*=y\noutput(x)\n\noutput:";
                bContext.text = "B. x= 5*3\ny= 10*2.2 \nx+=y\noutput(x)\n\noutput:";
                cContext.text = "C. x= 1+2*3\ny= (1+2)*3 \nz=-1*(1+2*3)\noutput(z+y+x)\n\noutput:";
                dContext.text = "D. x= 3*4-1\ny= 4+1*5 \nz=2*2+2\ny= 2*(y+z+x)\noutput(y)\n\noutput:";
                eContext.text = "Multiplication";
            }
            else if (num == 4 && end == 2)//Updates code text to the division section after multiplication section is all correct.
            {
                aContext.color = bContext.color = cContext.color = dContext.color = eContext.color = new Color32(0, 255, 255, 255);
                aContext.text = "A. x= 2.0/4\noutput(x)\n\noutput:";
                bContext.text = "B. y= 2/4\noutput(y)\n\noutput:";
                cContext.text = "C. x= 1.1*3\ny= 2.2+5 \nz=  10.5-2\nz+= (x+y)\noutput(z/2)\n\noutput:";
                dContext.text = "D. x= 3*3\ny= 2+5 \nz= (x+y)*2\nz+=1\noutput(z/5)\n\noutput:";
                eContext.text = "Division";
            }
            else if (num == 4 && end == 3)//Updates code text to the modulus after division section is all correct.
            {
                aContext.color = bContext.color = cContext.color = dContext.color = eContext.color = new Color32(200, 200, 200, 255);
                aContext.text = "A. x= 2%4\noutput(x)\n\noutput:";
                bContext.text = "B. x= 3%2+1\noutput(x)\n\noutput:";
                cContext.text = "C. x= 3.3*3+3%2\ny= 20%7 \nz= x+y-.9\noutput(z/8)\n\noutput:";
                dContext.text = "D. x= 24%5\ny= 15%2 \nz=5%x+y\noutput(z)\n\noutput:";
                eContext.text = "Modulus";
            }
        }
    }
}