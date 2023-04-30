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
    public class Pseudo_L1 : MonoBehaviour
    {
        public TMP_InputField userInput; // References the User's Input Field
        public TMP_InputField stringInput;
        public TMP_InputField boolInput;
        public TMP_InputField floatInput;
        public TextMeshProUGUI programOutput; // References the TMP Output Field
        public TextMeshProUGUI stringOutput;
        public TextMeshProUGUI boolOutput;
        public TextMeshProUGUI floatOutput;
        public BoxCollider2D levelSprite;

        public GameObject[] boxTests;
        public GameObject[] basketTests;
        public GameObject complete;
        public GameObject bit;
        public GameObject box;
        public GameObject cam;
        float camx, camy, camz;
        int count;
        private Button_Check button_Check;

        void Start()
        {
            count = 0;
            boxTests = GameObject.FindGameObjectsWithTag("Grabbable");
            basketTests = GameObject.FindGameObjectsWithTag("Button");
            foreach (GameObject go in boxTests)//searches for "Grabbable" objects
            {
                go.SetActive(false);
            }
            camx = cam.transform.position.x;
            camy = cam.transform.position.y;
            camz = cam.transform.position.z;
            complete.SetActive(false);
        }

        void Update()
        {
            if (bit.transform.position.x > 1331)//gameplay section
            {

                cam.transform.position = new Vector3(camx + 405, camy, camz);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    foreach (GameObject go in basketTests)//serches for "Button" objects
                    {
                        if (!go.name.Contains("Arm"))//Button objects that don't use a script
                        {
                            button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                            if (button_Check.boxFirstName == "1" && count == 0)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("41"))
                                            fo.SetActive(true);
                                    }
                                }
                            }
                            else if (button_Check.boxFirstName == "2" && count == 0)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("11"))
                                            fo.SetActive(true);
                                    }
                                }
                            }
                            else if (button_Check.boxFirstName == "3" && count == 0)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("21"))
                                            fo.SetActive(true);
                                    }
                                }
                            }
                            else if (button_Check.boxFirstName == "4" && count == 0)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("12"))
                                            fo.SetActive(true);
                                        
                                    }
                                    count++;
                                }
                            }
                            else if (button_Check.boxFirstName == "1" && count == 1)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("22"))
                                            fo.SetActive(true);
                                    }
                                }
                            }
                            else if (button_Check.boxFirstName == "2" && count == 1)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("32"))
                                            fo.SetActive(true);
                                    }
                                }
                            }
                            else if (button_Check.boxFirstName == "3" && count == 1)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        if (fo.name.Contains("42"))
                                            fo.SetActive(true);
                                    }
                                }
                            }
                            else if (button_Check.boxFirstName == "4" && count == 1)
                            {

                                if (button_Check.complete)
                                {
                                    foreach (GameObject fo in boxTests)//searches for "Grabbable" objects
                                    {
                                        if (fo.activeSelf)
                                        {
                                            fo.SetActive(false);
                                            fo.transform.position = new Vector3(camx + 605, camy, camz);
                                        }
                                        
                                    }
                                    count++;
                                }
                            }
                            if (count == 2)
                            {
                                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
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
            }
            else
                cam.transform.position = new Vector3(camx, camy, camz);
        }

                public void Code_Compiler()
        {
            //Ints
            int num = 0;//counts up everytime a try block receives valid input.

            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(userInput.text)))//Checks if values were inputed skips if no value
            {
                try
                { // Saves Text from input field into user input
                    int i = int.Parse(userInput.text);//tests for only integers
                }
                catch (Exception)//activates when the input is invalid
                {
                    programOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow //R,G,B, Transparency. 
                    programOutput.text = "Incorrect";
                    safe = false;
                }
                if (safe)
                {
                    programOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    programOutput.text = "Correct!";
                    num++;

                }
            }

            //String
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(stringInput.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    string s = stringInput.text;//tests for strings
                }
                catch (Exception)
                {
                    stringOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    stringOutput.text = "Incorrect";
                    safe = false;
                }
                if (safe)
                {
                    string s = stringInput.text;

                    if (s.StartsWith("\"") && s.EndsWith("\"") && s.Length != 1)
                    {
                        stringOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                        stringOutput.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        stringOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        stringOutput.text = "Incorrect";
                    }
                }
            }
            //bool
            safe = true;
            if (!(String.IsNullOrEmpty(boolInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    bool i = bool.Parse(boolInput.text);//tests for bools
                }
                catch (Exception)
                {//below makes the use of 1 or 0 for bool acceptable.
                    if (1 == int.Parse(boolInput.text) || 0 == int.Parse(boolInput.text))
                    {
                        boolOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                        boolOutput.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        boolOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        boolOutput.text = "Incorrect";

                    }
                    safe = false;
                }
                if (safe)
                {
                    boolOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    boolOutput.text = "Correct!";
                    num++;
                }
            }
            safe = true;
            //float
            if (!(String.IsNullOrEmpty(floatInput.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    float i = float.Parse(floatInput.text);//tests for floats
                }
                catch (Exception)
                {
                    floatOutput.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                    floatOutput.text = "Incorrect";
                    safe = false;
                }
                if (safe)
                {
                    floatOutput.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    floatOutput.text = "Correct!";
                    num++;
                }
            }

            if (num == 4)
            {
                box.SetActive(true);
				Audio_Manager.Instance.PlaySound("Correct");
            }
            else
            {
                levelSprite.isTrigger = false; // Cannot finish the level
				Audio_Manager.Instance.PlaySound("Incorrect");
				
            }
        }
    }
}

