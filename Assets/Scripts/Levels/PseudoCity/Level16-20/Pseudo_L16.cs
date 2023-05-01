// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-12-2022

using System;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using System.Collections;
using GameMechanics; // Pulls in the interface from GameMechanics

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

        public GameObject fail;
        public GameObject complete;
        public GameObject bit;
        public GameObject cam;
        float camx, camy, camz;

        public GameObject[] boxTests;
        public GameObject[] hairTests;
        private Button_Check button_Check;
        public GameObject cat;
        public GameObject left;
        public GameObject right;
        public GameObject top;
        private bool button1;
        private bool button2;
        private bool button3;
        private bool button4;
        private bool turn = false;
        private bool hit = false;
        private bool flip = false;
        private bool fro = true;
        private bool play = false;
        float startTime;
        float endTime;
        public BoxCollider2D levelSprite;
        private IEnumerator CatLeft()
        {
            while (!flip)
            {
                while (cat.transform.position.y > 380)//moves the snakes in the tree
                {
                    cat.transform.Translate(0.006f, 0, 0);
                    yield return new WaitForSeconds(1f);
                }
                if (!turn && !hit)
                {
                    cat.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    turn = true;
                }
                while (cat.transform.position.x < 1700 && !hit)//moves the cat
                {
                    if (left.activeSelf && cat.transform.position.x >= left.transform.position.x - 1 && cat.transform.position.x <= left.transform.position.x + 1)
                    {
                        hit = true;
                        turn = false;
                    }
                    cat.transform.Translate(0.006f, 0, 0);
                    yield return new WaitForSeconds(1f);
                }
                if (!turn && hit)
                {
                    cat.transform.Translate(200f, 200f, 0);
                    cat.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                    cat.transform.Rotate(180.0f, 0.0f, 0.0f, Space.Self);
                    hit = false;
                    flip = true;
                }
                else
                    flip = true;
            }
        }
        private IEnumerator CatRight()
        {
            while (flip)
            {
                while (cat.transform.position.y > 380)//moves the snakes in the tree
                {
                    cat.transform.Translate(0.006f, 0, 0);
                    yield return new WaitForSeconds(1f);
                }
                if (!turn && !hit)
                {
                    cat.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    turn = true;
                }
                while (cat.transform.position.x > 1710 && !hit)//moves the cat
                {
                    if (right.activeSelf && cat.transform.position.x >= right.transform.position.x - 1 && cat.transform.position.x <= right.transform.position.x + 1)
                    {
                        hit = true;
                        turn = false;
                    }
                    cat.transform.Translate(0.006f, 0, 0);
                    yield return new WaitForSeconds(1f);
                }
                if (!turn && hit)
                {
                    cat.transform.Translate(160f, 200f, 0);
                    cat.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                    cat.transform.Rotate(180.0f, 0.0f, 0.0f, Space.Self);
                    hit = false;
                    flip = false;
                }
                else
                    flip = false;
            }
        }
        void Start()
        {
            left.SetActive(false);
            right.SetActive(false);
            top.SetActive(false);
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
            fail.SetActive(false);
            //play = true;
        }
        void Update()
        {
            cam.transform.position = new Vector3(camx + 485, camy, camz);//moves camera to new section
            if (bit.transform.position.x > 1331)//gameplay section
            {
                cam.transform.position = new Vector3(camx + 505, camy, camz);
                if (play)
                {
                    boxTests = GameObject.FindGameObjectsWithTag("Button");
                    if (!flip && fro)
                    {
                        startTime = Time.time;
                        fro = false;
                        StartCoroutine(CatLeft());
                        if (cat.transform.position.x < 1711 && cat.transform.position.x > 1699)
                        {
                            button4 = false;
                            foreach (GameObject go in hairTests)//serches for "Box" objects
                            {
                                if(go.name.Contains("4"))
                                    go.SetActive(false);
                            }
                        }
                    }
                    if (flip && endTime < 60 && button4)
                    {
                        endTime = Time.time - startTime;
                        StopCoroutine(CatLeft());
                        StartCoroutine(CatRight());
                        if (cat.transform.position.x < 1711 && cat.transform.position.x > 1699)
                        {
                            button4 = false;
                            foreach (GameObject go in hairTests)//serches for "Box" objects
                            {
                                if (go.name.Contains("4"))
                                    go.SetActive(false);
                            }
                        }
                    }
                    if (!flip && endTime < 60 && button4)
                    {
                        endTime = Time.time - startTime;
                        StopCoroutine(CatRight());
                        StartCoroutine(CatLeft());
                        if (cat.transform.position.x < 1711 && cat.transform.position.x > 1699)
                        {
                            button4 = false;
                            foreach (GameObject go in hairTests)//serches for "Box" objects
                            {
                                if (go.name.Contains("4"))
                                    go.SetActive(false);
                            }
                        }
                    }

                    foreach (GameObject go in boxTests)//serches for "Button" objects
                    {
                        if (!go.name.Contains("Arm"))//Button objects that don't use a script
                        {
                            button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                            if (button_Check.boxFirstName == "1" && button_Check.name.Contains("(1)"))
                            {

                                if (button_Check.complete)
                                {
                                    left.SetActive(true);
                                }
                                else
                                    left.SetActive(false);
                            }
                            else if (button_Check.boxFirstName == "1" && button_Check.name.Contains("(2)"))
                            {

                                if (button_Check.complete)
                                {
                                    top.SetActive(true);
                                }
                                else
                                    top.SetActive(false);
                            }
                            if (button_Check.boxFirstName == "1" && button_Check.name.Contains("(3)"))
                            {

                                if (button_Check.complete)
                                {
                                    right.SetActive(true);
                                }
                                else
                                    right.SetActive(false);
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
                            if (endTime >= 60 && button4)
                            {
                                StopCoroutine(CatLeft());
                                StopCoroutine(CatRight());
                                cat.SetActive(false);
                                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
                                complete.SetActive(true);//Displays completion icon above npc
                            }
                            else if (!button4)
                            {
                                fail.SetActive(true);//Displays failure icon above npc
                                levelSprite.isTrigger = false;
                            }
                            else
                            {
                                levelSprite.isTrigger = false;
                                complete.SetActive(false);
                                fail.SetActive(false);
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
                }
                catch (Exception)
                {
                    safe = false;
                    if (cInput.text == "part1" || cInput.text == "part2")
                    {
                        c = 2;
                        safe = true;
                    }
                    if (!safe)
                    {
                        cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 

                        cOutput.text = "Invalid";
                        safe = false;
                    }
                }
                try
                {
                    c2 = int.Parse(c2Input.text);
                }
                catch (Exception)
                {
                    safe = false;
                    if (c2Input.text == "part1" || c2Input.text == "part2" || c2Input.text == "temp")
                    {
                        c2 = 2;
                        safe = true;
                    }
                    if (!safe)
                    {
                        cOutput.color = new Color32(255, 100, 100, 255);//Changes font color to red 

                        cOutput.text = "Invalid";
                        safe = false;
                    }
                }
                try 
                {
                    c3 = int.Parse(c3Input.text);
                }
                catch (Exception)
                {
                    safe = false;
                    if (c3Input.text == "part1" || c3Input.text == "part2" || c3Input.text == "temp")
                    {
                        c3 = 2;
                        safe = true;
                    }
                    if (!safe)
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
				
                foreach (GameObject go in hairTests)//searches for "Grabbable" objects
                {
                    go.SetActive(true);
                }
                play = true;
                Audio_Manager.Instance.PlaySound("Correct");
            }
			else
				Audio_Manager.Instance.PlaySound("Incorrect");


        }

    }
}


