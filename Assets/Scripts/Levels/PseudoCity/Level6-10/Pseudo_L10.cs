// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L10 : MonoBehaviour
    {
        public TMP_InputField aInput; // References the User's Input Field
        public TMP_InputField bInput;
        public TMP_InputField b2Input;
        public TMP_InputField c2Input;
        public TMP_InputField cInput;
        public TMP_InputField dInput;


        public TextMeshProUGUI aOutput; // References the TMP Output Field
        public TextMeshProUGUI bOutput;
        public TextMeshProUGUI cOutput;
        public TextMeshProUGUI dOutput;

        int[] road = new int[10];

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
                       
                        if (button_Check.complete)
                        {
                            var goRenderer = go.GetComponent<Renderer>();
                            goRenderer.material.SetColor("_Color", Color.gray);
                            if (go.name.Contains("(0)"))
                            {
                                road[0] = 1;
                            }
                            if (go.name.Contains("(1)"))
                            {
                                road[1] = 1;
                            }
                            else if (go.name.Contains("(2)"))
                            {
                                road[2] = 1;
                            }
                            else if (go.name.Contains("(3)"))
                            {
                                road[3] = 1;
                            }
                            else if (go.name.Contains("(4)"))
                            {
                                road[4] = 1;
                            }
                            else if (go.name.Contains("(5)"))
                            {
                                road[5] = 1;
                            }
                            else if (go.name.Contains("(6)"))
                            {
                                road[6] = 1;
                            }
                            else if (go.name.Contains("(7)"))
                            {
                                road[7] = 1;
                            }
                            else if (go.name.Contains("(8)"))
                            {
                                road[8] = 1;
                            }
                            else if (go.name.Contains("(9)"))
                            {
                                road[9] = 1;
                            }
                        }
                        string roadCount = "";
                        for (int i = 0; i < 10; i++)
                        {
                            roadCount += road[i];
                        }
                        if (roadCount == "1111111111")
                        {
                            levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                            Debug.Log("Good!");
                        }
                        else
                        {
                            //Debug.Log("Not working!");
                            levelSprite.isTrigger = false;
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
                    if (a == 1234)
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


            b2 = 0;
            y = 9;
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(bInput.text)) &&
                !(String.IsNullOrEmpty(b2Input.text)))//Checks if values were inputed skips if no value
            {

                try
                {
                    if (bInput.text != "y")
                        b = int.Parse(bInput.text);
                    else
                        b = y;

                    if (b2Input.text != "y")
                        b2 = int.Parse(b2Input.text);
                    else
                        b2 = y;

                }
                catch (Exception)
                {
                    bOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    bOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (bInput.text == "y" && b2 == 3)
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
            int c2;
            c2 = 0;
            safe = true;
            if (!(String.IsNullOrEmpty(cInput.text)) && !(String.IsNullOrEmpty(c2Input.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    
                    if (cInput.text != "while" && cInput.text != "if")
                    {
                        c = int.Parse(cInput.text);
                    }
                    if (c2Input.text != "if")
                    {
                        c2 = int.Parse(c2Input.text);//tests for only integers
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
                    if (cInput.text == "while")
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
            if (!(String.IsNullOrEmpty(dInput.text)))//Checks if values were inputed skips if no value
            {
                d = 0;
                try
                {
                    if (dInput.text != "ADBABC")
                    {
                        d = 1;//tests for only integers
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


                    if (d == 0)
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


