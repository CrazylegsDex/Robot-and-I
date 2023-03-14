// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L7 : MonoBehaviour
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


        public GameObject wall;
        public GameObject bit;
        public GameObject cam;
        float camx, camy, camz;

        int[] fences = new int[9];

        public GameObject[] boxTests;
        public GameObject[] paintTests;
        public GameObject paintTest;
        bool paint = false;
        private Button_Check button_Check;
        public BoxCollider2D levelSprite;
        void Start()
        {
            paintTests = GameObject.FindGameObjectsWithTag("Box");
            foreach (GameObject go in paintTests)//serches for "Box" objects
            {
                go.SetActive(false);
            }
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;

        }
        void Update()
        {
            cam.transform.position = new Vector3(camx + 485, camy, camz);//moves camera to new section
            if (bit.transform.position.x > 1331)//gameplay section
            {
                
                cam.transform.position = new Vector3(camx + 505, camy, camz);
                boxTests = GameObject.FindGameObjectsWithTag("Button");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    foreach (GameObject go in paintTests)//serches for "Box" objects
                    {
                        if (go.GetComponent<Rigidbody2D>().isKinematic == true)
                        {
                            paintTest = go;
                            paint = true;
                        }
                    }


                    foreach (GameObject go in boxTests)//serches for "Button" objects
                    {
                        if (!go.name.Contains("Arm"))//Button objects that don't use a script
                        {
                            //float boxPos = go.transform.position.x;
                            button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                            if (button_Check.complete && paintTest.name.Contains("Yell"))
                            {

                                var goRenderer = go.GetComponent<Renderer>();
                                goRenderer.material.SetColor("_Color", Color.yellow);
                                if (go.name.Contains("(1)"))
                                {
                                    fences[0] = 1;
                                }
                                else if (go.name.Contains("(2)"))
                                {
                                    fences[1] = 1;
                                }
                                else if (go.name.Contains("(3)"))
                                {
                                    fences[2] = 1;
                                }
                                else if (go.name.Contains("(4)"))
                                {
                                    fences[3] = 1;
                                }
                                else if (go.name.Contains("(5)"))
                                {
                                    fences[4] = 1;
                                }
                                else if (go.name.Contains("(6)"))
                                {
                                    fences[5] = 1;
                                }
                                else if (go.name.Contains("(7)"))
                                {
                                    fences[6] = 1;
                                }
                                else if (go.name.Contains("(8)"))
                                {
                                    fences[7] = 1;
                                }
                                else if (go.name.Contains("(9)"))
                                {
                                    fences[8] = 1;
                                }

                            }

                            if (button_Check.complete && paintTest.name.Contains("Black"))
                            {
                                var goRenderer = go.GetComponent<Renderer>();
                                goRenderer.material.SetColor("_Color", Color.black);
                                if (go.name.Contains("(1)"))
                                {
                                    fences[0] = 2;
                                }
                                else if (go.name.Contains("(2)"))
                                {
                                    fences[1] = 2;
                                }
                                else if (go.name.Contains("(3)"))
                                {
                                    fences[2] = 2;
                                }
                                else if (go.name.Contains("(4)"))
                                {
                                    fences[3] = 2;
                                }
                                else if (go.name.Contains("(5)"))
                                {
                                    fences[4] = 2;
                                }
                                else if (go.name.Contains("(6)"))
                                {
                                    fences[5] = 2;
                                }
                                else if (go.name.Contains("(7)"))
                                {
                                    fences[6] = 2;
                                }
                                else if (go.name.Contains("(8)"))
                                {
                                    fences[7] = 2;
                                }
                                else if (go.name.Contains("(9)"))
                                {
                                    fences[8] = 2;
                                }
                            }
                            if (button_Check.complete && paintTest.name.Contains("Blue"))
                            {
                                var goRenderer = go.GetComponent<Renderer>();
                                goRenderer.material.SetColor("_Color", Color.blue);
                                if (go.name.Contains("(1)"))
                                {
                                    fences[0] = 3;
                                }
                                else if (go.name.Contains("(2)"))
                                {
                                    fences[1] = 3;
                                }
                                else if (go.name.Contains("(3)"))
                                {
                                    fences[2] = 3;
                                }
                                else if (go.name.Contains("(4)"))
                                {
                                    fences[3] = 3;
                                }
                                else if (go.name.Contains("(5)"))
                                {
                                    fences[4] = 3;
                                }
                                else if (go.name.Contains("(6)"))
                                {
                                    fences[5] = 3;
                                }
                                else if (go.name.Contains("(7)"))
                                {
                                    fences[6] = 3;
                                }
                                else if (go.name.Contains("(8)"))
                                {
                                    fences[7] = 3;
                                }
                                else if (go.name.Contains("(9)"))
                                {
                                    fences[8] = 3;
                                }
                            }
                            string fenceCount = "";
                            for(int i = 0; i<9; i++)
                            {
                                fenceCount += fences[i];
                            }
                            //Debug.Log(fenceCount);
                            if (fenceCount == "123213123")
                            {
                                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                                Debug.Log("Good!");
                            }
                            else
                            {
                                levelSprite.isTrigger = false;
                            }
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
                    if (a <= 5 && a >= 1)
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
                    if (b2Input.text != "x" || b2Input.text != "y")
                    {
                        b2 = int.Parse(b2Input.text);
                    }
                    else if (bInput.text == "x")
                        b2 = x;
                    else
                        b2 = y;
                    if (b3Input.text != "x" || b3Input.text != "y")
                    {
                        b3 = int.Parse(b3Input.text);
                    }
                    else if (bInput.text == "x")
                        b3 = x;
                    else
                        b3 = y;
                    if (b4Input.text != "x" || b4Input.text != "y")
                    {
                        b4 = int.Parse(b4Input.text);
                    }
                    else if (b4Input.text == "x")
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
                    x = b;
                    y = b2;
                    if (x >= b3 && x < 5 && y >= 5 &&y < b4)
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
                    if (c == 4)
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
                d2 = d3 = 0;
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
                    
                }
                catch (Exception)
                {
                    dOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    dOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if(y < d2)
                    {
                        y += 5;
                    }
                    
                    if (x <= d && y == d3)
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
                foreach (GameObject go in paintTests)//serches for "Box" objects
                {
                    go.SetActive(true);
                }
            }


        }

    }
}


