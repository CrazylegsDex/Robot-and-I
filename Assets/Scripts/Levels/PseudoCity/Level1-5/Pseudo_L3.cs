// This script provides control and completion for Pseudo levels
//
// Author: Robot and I Team
// Last modification date: 11-14-2022

using System;
using UnityEngine;
using TMPro;

namespace PseudoLevels
{
    public class Pseudo_L3 : MonoBehaviour
    {
        // The Scene Objects
        public GameObject CheckAnswer1;
        public GameObject Subtraction;
        public GameObject CheckAnswer2;
        public GameObject Multiplication;
        public GameObject CheckAnswer3;
        public GameObject Division;
        public GameObject CheckAnswer4;
        public GameObject Modulus;
        public GameObject CheckAnswer5;
        public GameObject Gate1;
        public GameObject Gate2;
        public GameObject Gate3;
        public GameObject Gate4;
		public GameObject Dialogue2;
		public GameObject Dialogue3;
		public GameObject Dialogue4;
		public GameObject Dialogue5;

        // Input field boxes
        private TMP_InputField InputA;
        private TMP_InputField InputB;
        private TMP_InputField InputC;
        private TMP_InputField InputD;

        // Output field boxes
        private TextMeshProUGUI OutputA;
        private TextMeshProUGUI OutputB;
        private TextMeshProUGUI OutputC;
        private TextMeshProUGUI OutputD;

        // Level Collider
        public BoxCollider2D levelSprite;

        bool button1;
        bool button2;
        bool button3;
        public GameObject complete;
        public GameObject[] boxTests;
        public GameObject[] basketTests;
        private Button_Check button_Check;

        void Start()
        {
            button1 = false;
            button2 = false;
            button3 = false;
            boxTests = GameObject.FindGameObjectsWithTag("Grabbable");
            basketTests = GameObject.FindGameObjectsWithTag("Button");
            foreach (GameObject go in boxTests)//searches for "Grabbable" objects
            {
                go.SetActive(false);
            }
            complete.SetActive(false);
        }
        void Update()
        {
                foreach (GameObject go in basketTests)//serches for "Button" objects
                {
                    if (!go.name.Contains("Arm"))//Button objects that don't use a script
                    {
                        button_Check = go.GetComponent<Button_Check>();//Gets variables from script
                        if (button_Check.boxFirstName == "1")
                        {

                            if (button_Check.complete)
                            {
                            //Debug.Log("1");
                            button1 = true;
                            }
                            else
                                button1 = false;
                        }
                        else if (button_Check.boxFirstName == "2")
                        {

                            if (button_Check.complete)
                            {
                            //Debug.Log("2");
                            button2 = true;
                            }
                            else
                                button2 = false;
                        }
                        else if (button_Check.boxFirstName == "3")
                        {

                            if (button_Check.complete)
                            {
                                //Debug.Log("3");
                                button3 = true;
                            }
                            else
                                button3 = false;
                        }

                    }
                }

            if(button1 && button2 && button3)
            {
                complete.SetActive(true);
                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
            }
        }
                    // Awake is called after all objects are initialized
                    // Used to initialize the TMP objects
                    // private void Awake()
                    //{
                    //   InputA = GameObject.Find("CodeBoxA1").GetComponent<TMP_InputField>();
                    //  InputB = GameObject.Find("CodeBoxB1").GetComponent<TMP_InputField>();
                    // InputC = GameObject.Find("CodeBoxC1").GetComponent<TMP_InputField>();
                    //     InputD = GameObject.Find("CodeBoxD1").GetComponent<TMP_InputField>();
                    //   OutputA = GameObject.Find("OutputBoxA1").GetComponent<TextMeshProUGUI>();
                    //   OutputB = GameObject.Find("OutputBoxB1").GetComponent<TextMeshProUGUI>();
                    //   OutputC = GameObject.Find("OutputBoxC1").GetComponent<TextMeshProUGUI>();
                    //   OutputD = GameObject.Find("OutputBoxD1").GetComponent<TextMeshProUGUI>();
                    // }

                    // Code for the Addition Area
                    public void Addition_Area()
        {
			InputA = GameObject.Find("CodeBoxA1").GetComponent<TMP_InputField>();
            InputB = GameObject.Find("CodeBoxB1").GetComponent<TMP_InputField>();
            InputC = GameObject.Find("CodeBoxC1").GetComponent<TMP_InputField>();
            InputD = GameObject.Find("CodeBoxD1").GetComponent<TMP_InputField>();
            OutputA = GameObject.Find("OutputBoxA1").GetComponent<TextMeshProUGUI>();
            OutputB = GameObject.Find("OutputBoxB1").GetComponent<TextMeshProUGUI>();
            OutputC = GameObject.Find("OutputBoxC1").GetComponent<TextMeshProUGUI>();
            OutputD = GameObject.Find("OutputBoxD1").GetComponent<TextMeshProUGUI>();
            int num = 0;//counts up everytime a try block receives valid input.
            double a, b, c, d;//Input values

            a = b = c = d = 0;
            //A section
            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(InputA.text)))//Checks if values were inputed skips if no value
            {
                try
                {// Save Text from input field into user input
                    a = double.Parse(InputA.text);//tests for doubles
                }
                catch (Exception)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {//Correct double inputed for section
                    OutputA.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (a == 49)//new value for each section 
                    {
                        OutputA.text = "Correct!";
                        num++;
                    }
                    else//Wrong double inputed in section
                    {
                        OutputA.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputA.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputA.text = "";
            }
            //B section
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(InputB.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    b = double.Parse(InputB.text);
                }
                catch (Exception)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 37)
                    {
                        OutputB.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputB.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputB.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputB.text = "";
            }
            //C section
            safe = true;
            if (!(String.IsNullOrEmpty(InputC.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    c = double.Parse(InputC.text);
                }
                catch (Exception)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    OutputC.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (c == 9)
                    {
                        OutputC.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputC.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputC.text = "";
            }
            //D section
            safe = true;
            if (!(String.IsNullOrEmpty(InputD.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    d = double.Parse(InputD.text);
                }
                catch (Exception)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    OutputD.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (d == 52)
                    {
                        OutputD.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputD.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputD.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputD.text = "";
            }

            // If player successfully input all correct values
            if (num == 4)
            {
                // Lift the gate (NOTE: Rotation is 90, therefore X = Y and Y = X
                Gate1.transform.Translate(26, 0, 0); // X, Y, Z Translation move

                // Deactivate this area, Activate the next area
                CheckAnswer1.SetActive(false);
                Dialogue5.SetActive(true);

            }
        }

        // Code for the Subtraction Area

        // Code for the Multiplication Area

        // Code for the Division Area

        // Code for the Modulus Area
        public void Modulus_Area()
        {
			// Set Assign the TMP Objects
			InputA = GameObject.Find("CodeBoxA5").GetComponent<TMP_InputField>();
            InputB = GameObject.Find("CodeBoxB5").GetComponent<TMP_InputField>();
            InputC = GameObject.Find("CodeBoxC5").GetComponent<TMP_InputField>();
            InputD = GameObject.Find("CodeBoxD5").GetComponent<TMP_InputField>();
            OutputA = GameObject.Find("OutputBoxA5").GetComponent<TextMeshProUGUI>();
            OutputB = GameObject.Find("OutputBoxB5").GetComponent<TextMeshProUGUI>();
            OutputC = GameObject.Find("OutputBoxC5").GetComponent<TextMeshProUGUI>();
            OutputD = GameObject.Find("OutputBoxD5").GetComponent<TextMeshProUGUI>();
			
            int num = 0;//counts up everytime a try block receives valid input.
            double a, b, c, d;//Input values

            a = b = c = d = 0;
            //A section
            bool safe = true;//goes false if the input in the try blocks is invalid
            if (!(String.IsNullOrEmpty(InputA.text)))//Checks if values were inputed skips if no value
            {
                try
                {// Save Text from input field into user input
                    a = double.Parse(InputA.text);//tests for doubles
                }
                catch (Exception)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {//Correct double inputed for section
                    OutputA.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (a == 2)
                    {
                        OutputA.text = "Correct!";
                        num++;
                    }
                    else//Wrong double inputed in sectiion
                    {
                        OutputA.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputA.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputA.text = "";
            }
            //B section
            safe = true;//resets safe for next input
            if (!(String.IsNullOrEmpty(InputB.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    b = double.Parse(InputB.text);
                }
                catch (Exception)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 1)
                    {
                        OutputB.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputB.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputB.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputB.text = "";
            }
            //C section
            safe = true;
            if (!(String.IsNullOrEmpty(InputC.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    c = double.Parse(InputC.text);
                }
                catch (Exception)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    OutputC.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (c == 2)
                    {
                        OutputC.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputC.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputC.text = "";
            }
            //D section
            safe = true;
            if (!(String.IsNullOrEmpty(InputD.text)))//Checks if values were inputed skips if no value
            {
                try
                {
                    d = double.Parse(InputD.text);
                }
                catch (Exception)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                }
                if (safe)
                {
                    OutputD.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (d == 1.2)
                    {
                        OutputD.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputD.color = new Color32(255, 200, 0, 255);//changes font color to yellow
                        OutputD.text = "Incorrect";
                    }
                }
            }
            else
            {
                OutputD.text = "";
            }

            // If player successfully input all correct values
            if (num == 4)
            {
                // Deactivate this area, Set level to complete
                CheckAnswer5.SetActive(false);
                foreach (GameObject go in boxTests)//searches for "Grabbable" objects
                {
                    go.SetActive(true);
                }
            }
        }
    }
}