// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L11 : MonoBehaviour
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

        public TextMeshProUGUI aOutput; // References the TMP Output Field
        public TextMeshProUGUI bOutput;
        public TextMeshProUGUI cOutput;
        public TextMeshProUGUI dOutput;

        public GameObject complete;

        public GameObject wall;
        public GameObject bit;
        public GameObject cam;
        float camx, camy, camz;

        public GameObject[] boxTests;
        public GameObject[] hairTests;
        private Button_Check button_Check;
        private bool button1;
        private bool button2;
        public BoxCollider2D levelSprite;
        void Start()
        {
            hairTests = GameObject.FindGameObjectsWithTag("Box");
            foreach (GameObject go in hairTests)//serches for "Box" objects
            {
                go.SetActive(false);
            }
            //codeComp = false;
            button1 = false;
            button2 = false;
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;
            complete.SetActive(false);
        }
        void Update()
        {
            cam.transform.position = new Vector3(camx + 485, camy, camz);//moves camera to new section
            //Debug.Log(cam.transform.position.y);
            if (bit.transform.position.x > 1331)//gameplay section
            {
                cam.transform.position = new Vector3(camx + 505, camy, camz);
                boxTests = GameObject.FindGameObjectsWithTag("Button");
                foreach (GameObject go in boxTests)//serches for "Button" objects
                {
                    if (!go.name.Contains("Arm"))//Button objects that don't use a script
                    {
                        button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                        if (button_Check.boxFirstName == "1")
                        {

                            if (button_Check.complete)
                            {
                                button1 = true;
                                //Debug.Log(" 1 Works!");
                            }
                            else
                                button1 = false;
                        }
                        else if (button_Check.boxFirstName == "2")
                        {

                            if (button_Check.complete)
                            {
                                button2 = true;
                                //Debug.Log(" 2 Works!");
                            }
                            else
                                button2 = false;
                        }
                        if (button1 && button2)
                        {
                            levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                            Debug.Log("Good!");
                            complete.SetActive(true);
                        }
                        else
                        {
                            //Debug.Log("Not working!");
                            levelSprite.isTrigger = false;
                            complete.SetActive(false);
                        }
                    }

                }
                //cam.transform.position.x = camx + 485; 

            }
            else
            {
                //Debug.Log("Not yet!");
                cam.transform.position = new Vector3(camx, camy, camz);
                //cam.transform.position.x = camx;
            }
        }
        public void Code_Compiler()
        {
            //A
            int num = 0;//counts up everytime a try block receives valid input.
            int a, b, c, d;//Input values
            int x, y;
            a = b = c = d = 0;
            x = 5;

            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(aInput.text)) )//Checks if values were inputed skips if no value
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
                    if (a == 5)
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
            int b2;
            int b3;
            int b4;
            b4 = 0;

            b2 = b3 = 0;
            y = 7;
            x = 0;
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(b2Input.text)) && !(String.IsNullOrEmpty(bInput.text)) && !(String.IsNullOrEmpty(b4Input.text)) &&
                !(String.IsNullOrEmpty(b3Input.text)))//Checks if values were inputed skips if no value
            {

                try
                {
                    b = int.Parse(bInput.text);
                    x = b;
                    if (b2Input.text != "x")
                    {
                        b2 = int.Parse(b2Input.text);//tests for only integers
                    }
                    else if (b2Input.text == "x")
                        b2 = x;
                    y = b2;
                    if (b3Input.text != "y" || b3Input.text != "x")
                        b3 = int.Parse(b3Input.text);
                    else if (b3Input.text == "x")
                        b3 = x;
                    else
                    {
                        b3 = y;
                    }
                    if (b4Input.text != "y" || b4Input.text != "x")
                        b4 = int.Parse(b4Input.text);
                    else if (b4Input.text == "x")
                        b4 = x;
                    else
                    {
                        b4 = y;
                    }

                }
                catch (Exception)
                {
                    bOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    bOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (y == 3 && x < b3 && y < b4)
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
                    if (c == 0)
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
            x = 4;
            y = 2;
            safe = true;
            if (!(String.IsNullOrEmpty(dInput.text)) && !(String.IsNullOrEmpty(d2Input.text)) &&
                !(String.IsNullOrEmpty(d3Input.text)))//Checks if values were inputed skips if no value
            {
                int d2;
                d = d2 = 0;
                try
                {
                    d = int.Parse(dInput.text);//tests for only integers
                    x = d;
                    d2 = int.Parse(d2Input.text);
                   
                        



                }
                catch (Exception)
                {
                    dOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    dOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {


                    if (d2 - d == 3 && d3Input.text == "x")
                    {
                        dOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                        dOutput.text = "Correct!";
                        num++;
                        //Debug.Log(num);
                    }
                    else
                    {

                        dOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        dOutput.text = "Incorrect";
                    }

                }
            }
            //Debug.Log(num);
            if (num == 4)
            {
                Debug.Log("Works!");
                //hairTests = GameObject.FindGameObjectsWithTag("Box");
                foreach (GameObject go in hairTests)//serches for "Box" objects
                {
                    go.SetActive(true);
                }
            }


        }

    }
}


