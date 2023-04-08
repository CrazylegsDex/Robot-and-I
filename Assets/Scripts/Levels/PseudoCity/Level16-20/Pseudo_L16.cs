// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L16 : MonoBehaviour
    {
        public TMP_InputField aInput; // References the User's Input Field
        public TMP_InputField bInput;
        public TMP_InputField cInput;
        public TMP_InputField c2Input;
        public TMP_InputField c3Input;
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
        private bool button1;
        private bool button2;
        private bool button3;
        private bool button4;
        public BoxCollider2D levelSprite;
        void Start()
        {
            hairTests = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject go in hairTests)//serches for "Box" objects
            {
                go.SetActive(false);
            }
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
                        if (button_Check.boxFirstName == "3")
                        {

                            if (button_Check.complete)
                            {
                                button3 = true;
                            }
                            else
                                button3 = false;
                        }
                        else if (button_Check.boxFirstName == "4")
                        {

                            if (button_Check.complete)
                            {
                                button4 = true;
                            }
                            else
                                button4 = false;
                        }
                        if (button1 && button2 && button3 && button4)
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
            int c;//Input values
            c = 0;
            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(aInput.text)))//Checks if values were inputed skips if no value
            {
                if (safe)
                {
                    if (aInput.text == "yes!3")
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
                    if (bInput.text == "3")
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
            int c3;
            int c2;
            c3 = c2= 0;
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(cInput.text)) && !(String.IsNullOrEmpty(c2Input.text)) &&
                !(String.IsNullOrEmpty(c3Input.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    c = int.Parse(cInput.text);
                    c2 = int.Parse(c2Input.text);
                    c3 = int.Parse(c3Input.text);
                }
                catch (Exception)
                {
                    if (cInput.text == "part1" || cInput.text == "part2" )
                        c = 2;
                    if (c2Input.text == "part1" || c2Input.text == "part2" || c2Input.text == "temp")
                        c2 = 2;
                    if (c3Input.text == "part1" || c3Input.text == "part2" || c3Input.text == "temp")
                        c3 = 2;
                    else
                    {
                        cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 

                        cOutput.text = "Invalid";
                        safe = false;
                    }
                }
                if (safe)
                {
                    if (cInput.text == "part1" && c2Input.text == "temp" && c3 == 14)
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
            if (!(String.IsNullOrEmpty(dInput.text)) && !(String.IsNullOrEmpty(d2Input.text)) &&
                !(String.IsNullOrEmpty(d3Input.text)))//Checks if values were inputed skips if no value
            {
                if (safe)
                {
                    if (dInput.text == "j" && d2Input.text == "i" && d3Input.text == "min")
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
                Debug.Log("Works!");
                foreach (GameObject go in hairTests)//searches for "Grabbable" objects
                {
                    go.SetActive(true);
                }
            }


        }

    }
}

