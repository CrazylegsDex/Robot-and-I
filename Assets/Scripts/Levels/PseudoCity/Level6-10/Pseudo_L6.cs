// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L6 : MonoBehaviour
    {
        public TMP_InputField aInput; // References the User's Input Field
        public TMP_InputField bInput;
        public TMP_InputField b2Input;
        public TMP_InputField b3Input;
        public TMP_InputField b4Input;
        public TMP_InputField cInput;
        public TMP_InputField dInput;
        public TMP_InputField d2Input;
        public TMP_InputField d3Input;
        public TMP_InputField d4Input;

        public TextMeshProUGUI aOutput; // References the TMP Output Field
        public TextMeshProUGUI bOutput;
        public TextMeshProUGUI cOutput;
        public TextMeshProUGUI dOutput;

        public GameObject complete;
        public GameObject bit;
        public GameObject cam;
        float camx, camy, camz;

        public GameObject[] tonTests;
        public GameObject[] boxTests;
        private Button_Check button_Check;
        private bool button1;
        private bool button2;
        public BoxCollider2D levelSprite;
        void Start()
        {
            button1 = false;
            button2 = false;
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;
            complete.SetActive(false);
            boxTests = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject go in boxTests)//searches for "Grabbable" objects
            {
                go.SetActive(false);
            }
        }
        void Update()
        {
            cam.transform.position = new Vector3(camx + 485, camy, camz);
            if (bit.transform.position.x > 1331)//gameplay section
            {
                
                cam.transform.position = new Vector3(camx + 505, camy, camz);//moves camera to new section
                tonTests = GameObject.FindGameObjectsWithTag("Button");
                foreach (GameObject go in tonTests)//serches for "Button" objects
                {
                    if (!go.name.Contains("Arm"))//Button objects that don't use a script
                    {
                        button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                        if (button_Check.boxFirstName == "1")
                        {
                            
                            if (button_Check.complete)
                            {
                                button1 = true;
                            }
                            else
                                button1 = false;
                        }
                        else if (button_Check.boxFirstName == "2")
                        {
                            
                            if (button_Check.complete)
                            {
                                button2 = true;
                            }
                            else
                                button2 = false;
                        }
                        if (button1 && button2)
                        {
                            levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                            Debug.Log("Good!");
                            complete.SetActive(true);//Displays completion icon above npc
                        }
                        else
                        {
                            levelSprite.isTrigger = false;
                            complete.SetActive(false);
                        }
                    }

                }

            }
            else
            {
                cam.transform.position = new Vector3(camx, camy, camz);//Forces Bounds to move camera to next scene
            }
        }
        public void Code_Compiler()
        {
            //A
            int num = 0;//counts up everytime a try block receives valid input.
            int a, b, c, d;//Input values
            int x, y;
            a = b = c = d = 0;

            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(aInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {// Save Text from input field into user input
                    a = int.Parse(aInput.text);//tests for only integers
                }
                catch (Exception)//activates when the input is invalid
                {
                    aOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    aOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (a == 534)//Tests to make sure the answer in the section is correct
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
            int b2, b3, b4;
            b2 = b3 = b4 = 0;
            x = 5;
            y = 7;
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(bInput.text)) && !(String.IsNullOrEmpty(b2Input.text)) &&
                !(String.IsNullOrEmpty(b3Input.text)) && !(String.IsNullOrEmpty(b4Input.text)))//Checks if values were inputed skips if no value
            {

                try
                {
                    if (bInput.text != "x" || bInput.text != "y")
                    {
                        b = int.Parse(bInput.text);//tests for only integers
                    }
                    else if (bInput.text == "x")
                        b = x;
                    else
                        b = y;
                    if (bInput.text != "x" || bInput.text != "y")
                    {
                        b2 = int.Parse(b2Input.text);
                    }
                    else if (bInput.text == "x")
                        b2 = x;
                    else
                        b2 = y;
                    if (bInput.text != "x" || bInput.text != "y")
                    {
                        b3 = int.Parse(b3Input.text);
                    }
                    else if (bInput.text == "x")
                        b3 = x;
                    else
                        b3 = y;
                    if (bInput.text != "x" || bInput.text != "y")
                    {
                        b4 = int.Parse(b4Input.text);
                    }
                    else if (bInput.text == "x")
                        b4 = x;
                    else
                        b4 = y;
                }
                catch (Exception)
                {
                    bOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    bOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (b == x && y > b2 && x < b3 && y < b4)//Tests to make sure every answer in the section is correct
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
                catch (Exception)
                {
                    cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    cOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (c == 5)//Tests to make sure the answer in the section is correct
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
            x = 8;
            y = 3;
            safe = true;
            if (!(String.IsNullOrEmpty(dInput.text)) && !(String.IsNullOrEmpty(d2Input.text)) &&
                !(String.IsNullOrEmpty(d3Input.text)) && !(String.IsNullOrEmpty(d4Input.text)))//Checks if values were inputed skips if no value
            {
                int d2, d3, d4;
                d2 = d3 = d4 = 0;
                try
                {
                    if (dInput.text != "x" || dInput.text != "y")
                    {
                        d = int.Parse(dInput.text);//tests for only integers
                    }
                    else if (dInput.text == "x")
                        d = x;
                    else
                        d = y;
                    if (d2Input.text != "x" || d2Input.text != "y")
                    {
                        d2 = int.Parse(d2Input.text);
                    }
                    else if (d2Input.text == "x")
                        d2 = x;
                    else
                        d2 = y;
                    if (d3Input.text != "x" || d3Input.text != "y")
                    {
                        d3 = int.Parse(d3Input.text);
                    }
                    else if (d3Input.text == "x")
                        d3 = x;
                    else
                        d3 = y;
                    if (d4Input.text != "x" || d4Input.text != "y")
                    {
                        d4 = int.Parse(d4Input.text);
                    }
                    else if (d4Input.text == "x")
                        d4 = x;
                    else
                        d4 = y;
                }
                catch (Exception)
                {
                    dOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    dOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (x != d)
                    {
                        x /= 2;
                    }
                    if(y > d2){
                        y -= 1;
                        
                    }
                    if (x < d3)
                    {
                        y *= 2;
                        x /= 2;
                        
                    }
                    if (d == 8 && y == 4 && x == 4 && d4 == y)//Tests to make sure every answer in the section is correct
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
                foreach (GameObject go in boxTests)//searches for "Grabbable" objects
                {
                    go.SetActive(true);
                }
            }
            //
            

        }

    }
}


