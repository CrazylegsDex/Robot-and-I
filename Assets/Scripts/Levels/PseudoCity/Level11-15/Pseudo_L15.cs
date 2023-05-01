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
    public class Pseudo_L15 : MonoBehaviour
    {
        public TMP_InputField aInput; // References the User's Input Field
        public TMP_InputField bInput;
        public TMP_InputField cInput;
        public TMP_InputField c2Input;
        public TMP_InputField c3Input;
        public TMP_InputField dInput;
        public TMP_InputField d2Input;

        public TextMeshProUGUI aOutput; // References the TMP Output Field
        public TextMeshProUGUI bOutput;
        public TextMeshProUGUI cOutput;
        public TextMeshProUGUI dOutput;

        public GameObject chicken;
        public GameObject complete;
        public GameObject bit;
        public GameObject cam;
        float camx, camy, camz;

        private bool end = false;

        public GameObject[] boxTests;
        public GameObject[] hairTests;
        private Button_Check button_Check;
        private bool[] buttonArr = new bool[8];
        public BoxCollider2D levelSprite;
        void Start()
        {
            for (int i = 0; i < 8; i++)
                buttonArr[i] = false; 
            hairTests = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject go in hairTests)//searches for "Grabbable" objects
            {
                go.SetActive(false);
            }
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;
            complete.SetActive(false);
            chicken.SetActive(false);
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
                        if (button_Check.boxFirstName == "1" && button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[4] = true;
                            }
                            else
                                buttonArr[4] = false;
                        }
                        else if (button_Check.boxFirstName == "1" && !button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[0] = true;
                            }
                            else
                                buttonArr[0] = false;
                        }
                        else if (button_Check.boxFirstName == "2" && !button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[1] = true;
                            }
                            else
                                buttonArr[1] = false;
                        }
                        else if (button_Check.boxFirstName == "2" && button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[5] = true;
                            }
                            else
                                buttonArr[5] = false;
                        }
                        else if (button_Check.boxFirstName == "3" && button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[6] = true;
                            }
                            else
                                buttonArr[6] = false;
                        }
                        else if (button_Check.boxFirstName == "3" && !button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[2] = true;
                            }
                            else
                                buttonArr[2] = false;
                        }
                        else if (button_Check.boxFirstName == "4" && button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[7] = true;
                            }
                            else
                                buttonArr[7] = false;
                        }
                        else if (button_Check.boxFirstName == "4" && !button_Check.name.Contains("(1)"))
                        {

                            if (button_Check.complete)
                            {
                                buttonArr[3] = true;
                            }
                            else
                                buttonArr[3] = false;
                        }
                        if (end)
                        {
                            levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                            complete.SetActive(true);//Displays completion icon above npc
                            chicken.SetActive(true);//Displays chicken icon
                        }
                        else
                        {
                            int num = 0;
                            for(int i = 0; i < 8; i++)
                            {
                                if (buttonArr[i] == true)
                                    num++;
                            }
                            if (num == 8)
                                end = true;
                            levelSprite.isTrigger = false;
                            complete.SetActive(false);
                            chicken.SetActive(false);
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
                    if (aInput.text == "B6")
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
                    if (bInput.text == "5A5A5A5A")
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
            int c3,c2;
            c3 = 0;
            c2 = 1;
            safe = true;//resets safe for next input
            bool good = false;
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
                    if (cInput.text == "x")
                        good = true;
                    else
                    {
                        cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 

                        cOutput.text = "Invalid";
                        safe = false;
                    }
                }
                if (safe)
                {
                    if (good&& c2 == 3 && c3Input.text == "1")
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
                if (safe)
                {
                    if (dInput.text == "6" && d2Input.text == "5")
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


