// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L9 : MonoBehaviour
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

        public GameObject bit;
        public GameObject cam;
        float camx, camy, camz;

        public GameObject[] boxTests;
        public GameObject[] hairTests;
        private Button_Check button_Check;
        private bool[] button;
        public BoxCollider2D levelSprite;
        void Start()
        {
            hairTests = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject go in hairTests)//searches for "Grabbable" objects
            {
                go.SetActive(false);
            }
            button = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                button[i] = false;
            }
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;
            complete.SetActive(false);
        }
        void Update()
        {
            cam.transform.position = new Vector3(camx + 485, camy, camz);//moves camera to new section
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
                                button[0] = true;
                            }
                            else
                                button[0] = false;
                        }
                        else if (button_Check.boxFirstName == "2")
                        {

                            if (button_Check.complete)
                            {
                                button[1] = true;
                            }
                            else
                                button[1] = false;
                        }
                        if (button[0] && button[5])
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
                cam.transform.position = new Vector3(camx, camy, camz);
            }
        }
        public void Code_Compiler()
        {
            //A
            int num = 0;//counts up everytime a try block receives valid input.
            int a, b, c, d;//Input values
            int x, y;
            a = b = c = d = 0;
            x = 6;

            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(aInput.text)) && !(String.IsNullOrEmpty(bInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {// Save Text from input field into user input
                    a = int.Parse(aInput.text);//tests for only integers
                    if (bInput.text != "x")
                    {
                        b = int.Parse(bInput.text);//tests for only integers
                    }
                    else if (bInput.text == "x")
                        b = x;
                }
                catch (Exception)//activates when the input is invalid
                {
                    aOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    aOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (a == 6 && b == 6)
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
            
            
            b2 = b3 = 0;
            y = 7;
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(b2Input.text)) &&
                !(String.IsNullOrEmpty(b3Input.text)) )//Checks if values were inputed skips if no value
            {

                try
                {
                    b2 = int.Parse(b2Input.text);
                    
                    if (b3Input.text != "y")
                        b3 = int.Parse("w");
                    y = b2;
                    
                }
                catch (Exception)
                {
                    bOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    bOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (b2 == 1 && y == b2)
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
            int b4;
            b4 = 0;
            safe = true;
            if (!(String.IsNullOrEmpty(cInput.text)) && !(String.IsNullOrEmpty(b4Input.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    c = int.Parse(cInput.text);//tests for only integers
                    if (b4Input.text !="break")
                    {
                        b4 = int.Parse("q");
                    }
                    
                }
                catch (Exception)
                {
                    cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    cOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (c == 5)
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
                int d2, d3;
                d = d2 = d3 = 0;
                try
                {
                    if (dInput.text != "switch")
                    {
                        d = int.Parse("q");//tests for only integers
                    }
                    
                    if (d2Input.text != "yes")
                    {
                        d2 = 3;
                    }
                    
                    if (d3Input.text != "break")
                    {
                        d3 = int.Parse("q");
                    }
                    

                }
                catch (Exception)
                {
                    dOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    dOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {


                    if (d == d2 && d2 == d3)
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
            //Debug.Log(num);
            if (num == 4)
            {
                Debug.Log("Works!");
                foreach (GameObject go in hairTests)//searches for "Grabbable" objects
                {
                    go.SetActive(true);
                }
            }


        }

    }
}


