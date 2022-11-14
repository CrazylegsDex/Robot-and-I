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

        // Awake is called after all objects are initialized
        // Used to initialize the TMP objects
        private void Awake()
        {
            InputA = GameObject.Find("CodeBoxA1").GetComponent<TMP_InputField>();
            InputB = GameObject.Find("CodeBoxB1").GetComponent<TMP_InputField>();
            InputC = GameObject.Find("CodeBoxC1").GetComponent<TMP_InputField>();
            InputD = GameObject.Find("CodeBoxD1").GetComponent<TMP_InputField>();
            OutputA = GameObject.Find("OutputBoxA1").GetComponent<TextMeshProUGUI>();
            OutputB = GameObject.Find("OutputBoxB1").GetComponent<TextMeshProUGUI>();
            OutputC = GameObject.Find("OutputBoxC1").GetComponent<TextMeshProUGUI>();
            OutputD = GameObject.Find("OutputBoxD1").GetComponent<TextMeshProUGUI>();
        }

        // Code for the Addition Area
        public void Addition_Area()
        {
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
                catch (Exception ex)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {//Correct double inputed for section
                    OutputA.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (a == 3.0)//new value for each section 
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
                catch (Exception ex)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 6)
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
                catch (Exception ex)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputC.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (c == 3.3)
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
                catch (Exception ex)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);

                }
                if (safe)
                {
                    OutputD.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (d == 28)
                    {
                        OutputD.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
                Subtraction.SetActive(true);

                // Set Assign the TMP Objects
                InputA = GameObject.Find("CodeBoxA2").GetComponent<TMP_InputField>();
                InputB = GameObject.Find("CodeBoxB2").GetComponent<TMP_InputField>();
                InputC = GameObject.Find("CodeBoxC2").GetComponent<TMP_InputField>();
                InputD = GameObject.Find("CodeBoxD2").GetComponent<TMP_InputField>();
                OutputA = GameObject.Find("OutputBoxA2").GetComponent<TextMeshProUGUI>();
                OutputB = GameObject.Find("OutputBoxB2").GetComponent<TextMeshProUGUI>();
                OutputC = GameObject.Find("OutputBoxC2").GetComponent<TextMeshProUGUI>();
                OutputD = GameObject.Find("OutputBoxD2").GetComponent<TextMeshProUGUI>();
            }
        }

        // Code for the Subtraction Area
        public void Subtraction_Area()
        {
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
                catch (Exception ex)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {//Correct double inputed for section
                    OutputA.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (a == 1)
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
                catch (Exception ex)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 8)
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
                catch (Exception ex)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputC.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (c == -1.2)
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
                catch (Exception ex)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);

                }
                if (safe)
                {
                    OutputD.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (d == 3.4)
                    {
                        OutputD.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
                Gate2.transform.Translate(26, 0, 0); // X, Y, Z Translation move

                // Deactivate this area, Activate the next area
                CheckAnswer2.SetActive(false);
                Multiplication.SetActive(true);

                // Set Assign the TMP Objects
                InputA = GameObject.Find("CodeBoxA3").GetComponent<TMP_InputField>();
                InputB = GameObject.Find("CodeBoxB3").GetComponent<TMP_InputField>();
                InputC = GameObject.Find("CodeBoxC3").GetComponent<TMP_InputField>();
                InputD = GameObject.Find("CodeBoxD3").GetComponent<TMP_InputField>();
                OutputA = GameObject.Find("OutputBoxA3").GetComponent<TextMeshProUGUI>();
                OutputB = GameObject.Find("OutputBoxB3").GetComponent<TextMeshProUGUI>();
                OutputC = GameObject.Find("OutputBoxC3").GetComponent<TextMeshProUGUI>();
                OutputD = GameObject.Find("OutputBoxD3").GetComponent<TextMeshProUGUI>();
            }
        }

        // Code for the Multiplication Area
        public void Multiplication_Area()
        {
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
                catch (Exception ex)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {//Correct double inputed for section
                    OutputA.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (a == 49)
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
                catch (Exception ex)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 37)
                    {//Multiplication Section
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
                catch (Exception ex)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
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
                catch (Exception ex)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);

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
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
                Gate3.transform.Translate(26, 0, 0); // X, Y, Z Translation move

                // Deactivate this area, Activate the next area
                CheckAnswer3.SetActive(false);
                Division.SetActive(true);

                // Set Assign the TMP Objects
                InputA = GameObject.Find("CodeBoxA4").GetComponent<TMP_InputField>();
                InputB = GameObject.Find("CodeBoxB4").GetComponent<TMP_InputField>();
                InputC = GameObject.Find("CodeBoxC4").GetComponent<TMP_InputField>();
                InputD = GameObject.Find("CodeBoxD4").GetComponent<TMP_InputField>();
                OutputA = GameObject.Find("OutputBoxA4").GetComponent<TextMeshProUGUI>();
                OutputB = GameObject.Find("OutputBoxB4").GetComponent<TextMeshProUGUI>();
                OutputC = GameObject.Find("OutputBoxC4").GetComponent<TextMeshProUGUI>();
                OutputD = GameObject.Find("OutputBoxD4").GetComponent<TextMeshProUGUI>();
            }
        }

        // Code for the Division Area
        public void Division_Area()
        {
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
                catch (Exception ex)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {//Correct double inputed for section
                    OutputA.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (a == .5)
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
                catch (Exception ex)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 0)
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
                catch (Exception ex)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputC.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (c == 9.5)
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
                catch (Exception ex)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);

                }
                if (safe)
                {
                    OutputD.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (d == 6)
                    {
                        OutputD.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
                Gate4.transform.Translate(26, 0, 0); // X, Y, Z Translation move

                // Deactivate this area, Activate the next area
                CheckAnswer4.SetActive(false);
                Modulus.SetActive(true);

                // Set Assign the TMP Objects
                InputA = GameObject.Find("CodeBoxA5").GetComponent<TMP_InputField>();
                InputB = GameObject.Find("CodeBoxB5").GetComponent<TMP_InputField>();
                InputC = GameObject.Find("CodeBoxC5").GetComponent<TMP_InputField>();
                InputD = GameObject.Find("CodeBoxD5").GetComponent<TMP_InputField>();
                OutputA = GameObject.Find("OutputBoxA5").GetComponent<TextMeshProUGUI>();
                OutputB = GameObject.Find("OutputBoxB5").GetComponent<TextMeshProUGUI>();
                OutputC = GameObject.Find("OutputBoxC5").GetComponent<TextMeshProUGUI>();
                OutputD = GameObject.Find("OutputBoxD5").GetComponent<TextMeshProUGUI>();
            }
        }

        // Code for the Modulus Area
        public void Modulus_Area()
        {
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
                catch (Exception ex)//activates when the input is invalid
                {
                    OutputA.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputA.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
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
                catch (Exception ex)
                {
                    OutputB.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputB.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
                }
                if (safe)
                {
                    OutputB.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (b == 2)
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
                catch (Exception ex)
                {
                    OutputC.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputC.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);
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
                catch (Exception ex)
                {
                    OutputD.color = new Color32(255, 100, 100, 255);//Changes font color to red 
                    OutputD.text = "Invalid";
                    safe = false;
                    Debug.Log(ex.Message);

                }
                if (safe)
                {
                    OutputD.color = new Color32(0, 255, 255, 255);//changes font color to cyan
                    if (d == 2)
                    {
                        OutputD.text = "Correct!";
                        num++;
                    }
                    else
                    {
                        OutputC.color = new Color32(255, 200, 0, 255);//changes font color to yellow
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
                
                levelSprite.isTrigger = true; // Sets levelSprite to trigger complete
            }
        }
    }
}