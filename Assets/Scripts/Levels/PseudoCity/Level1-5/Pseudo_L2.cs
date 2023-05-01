// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using GameMechanics; // Pulls in the interface from GameMechanics

namespace PseudoLevels
{
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

        public GameObject complete;
        public GameObject bit;
        //public TextMeshProUGUI soundCheck;
        public GameObject cam;
        float camx, camy, camz;

        int[] fences = new int[9];
        public GameObject[] boxTests;
        public GameObject[] paintTests;
        private Button_Check button_Check;
        public GameObject paintTest;

        void Start()
        {
            complete.SetActive(false);
            paintTests = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject go in paintTests)//serches for "Box" objects
            {
                go.SetActive(false);
            }
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;
        }

        private void Update()
        {
            //Debug.Log(bit.transform.position.x);
            //cam.transform.position = new Vector3(camx, camy, camz);//moves camera to new section
            if (bit.transform.position.x > 1200)//gameplay section
            {
                cam.transform.position = new Vector3(camx + 405, camy, camz);

                boxTests = GameObject.FindGameObjectsWithTag("Button");
                foreach (GameObject go in paintTests)//serches for "Box" objects
                {
                    if (go.GetComponent<Rigidbody2D>().isKinematic == true)
                    {
                        paintTest = go;
                    }
                }


                foreach (GameObject go in boxTests)//serches for "Button" objects
                {
                    if (!go.name.Contains("Arm"))//Button objects that don't use a script
                    {
                        button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                        if (button_Check.complete)
                        {//Paints each fence yellow

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
                    }
                }
            }
            else
            {
                cam.transform.position = new Vector3(camx, camy, camz);
            }
            if (fences[2] == 1 && fences[3] == 1 && fences[4] == 1)
            {
                complete.SetActive(true);
                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
            }
        }

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
                catch (Exception)//activates when the input is invalid
                {
                    aOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    aOutput.text = "Invalid";
                    safe = false;
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
                catch (Exception)
                {
                    bOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    bOutput.text = "Invalid";
                    safe = false;
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
                catch (Exception)
                {
                    cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    cOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (c == 1)
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
                catch (Exception)
                {
                    dOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    dOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (d == 3)
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
                Audio_Manager.Instance.PlaySound("Correct");
            }
            else
            {
                levelSprite.isTrigger = false;
				Audio_Manager.Instance.PlaySound("Incorrect");
            }
        }
    }
}