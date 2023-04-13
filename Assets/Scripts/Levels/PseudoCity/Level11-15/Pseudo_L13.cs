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
    public class Pseudo_L13 : MonoBehaviour
    {
        public TMP_InputField aInput; // References the User's Input Field
        public TMP_InputField bInput;
        public TMP_InputField cInput;
        public TMP_InputField c2Input;
        public TMP_InputField dInput;
        public TMP_InputField d2Input;

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
        private int button1;
        private int button2;
        private int button3;
        private int button4;
        private int button5;
        public BoxCollider2D levelSprite;
        void Start()
        {
            hairTests = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject go in hairTests)//serches for "Grabbable" objects
            {
                go.SetActive(false);
            }
            button1 = 0;
            button2 = 0;
            button3 = 0;
            button4 = 0;
            button5 = 0;
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
                        if (go.name.StartsWith("1"))
                        {

                            if (Input.GetMouseButtonDown(0) && button_Check.complete )
                            {
                                button1++;
                            }
                        }
                        if (go.name.StartsWith("2"))
                        {
                            if (Input.GetMouseButtonDown(0) && button_Check.complete)
                            {
                                button2++;
                            }
                        }
                        if (go.name.StartsWith("3"))
                        {
                            if (Input.GetMouseButtonDown(0) && button_Check.complete)
                            {
                                button3++;
                            }
                        }
                        if (go.name.StartsWith("4"))
                        {
                            if (Input.GetMouseButtonDown(0) && button_Check.complete)
                            {
                                button4++;
                            }
                        }
                        if (go.name.StartsWith("5"))
                        {
                            if (Input.GetMouseButtonDown(0) && button_Check.complete)
                            {
                                button5++;
                            }
                        }
                        if (button1 > 4 && button2 > 4 && button3 > 4&& button4 > 4 && button5 > 4)//All trees are chopped down!
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
                    if (a == 01234)
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
            safe = true;
            if (!(String.IsNullOrEmpty(bInput.text)))//Checks if values were inputed skips if no value
            {

                if (safe)
                {
                    if (bInput.text == "6666654444")
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
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(cInput.text)) && !(String.IsNullOrEmpty(c2Input.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    c = int.Parse(cInput.text);
                }
                catch (Exception)
                {
                    cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    cOutput.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    if (c2Input.text == "a" && c == 1)
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
            if (!(String.IsNullOrEmpty(dInput.text)) && !(String.IsNullOrEmpty(d2Input.text)))//Checks if values were inputed skips if no value
            {
                int d2;
                d = d2 = 0;
                try
                {
                    d = int.Parse(dInput.text);
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
                    if (d == 1 && d2 == 10)
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
				Audio_Manager.Instance.PlaySound("Correct");
                Debug.Log("Works!");
                foreach (GameObject go in hairTests)//searches for "Grabbable" objects
                {
                    go.SetActive(true);
                }
            }
			else
				Audio_Manager.Instance.PlaySound("Incorrect");

        }

    }
}


