// This script provides control and completion Level 0
//
// Author: Robot and I Team
// Last modification date: 4-16-23

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using GameMechanics; // Pulls in the interface from GameMechanics

namespace LVL_0
{
    public class Level_0 : MonoBehaviour
    {

        public GameObject Problem1;
        public TMP_InputField HelloBox;
        public TextMeshProUGUI HelloError;
        public GameObject door;
        public GameObject Problem2;
        public TextMeshProUGUI Q1;
        public TMP_InputField A1;
        public TextMeshProUGUI Out1;
        public TextMeshProUGUI Q2;
        public TMP_InputField A2;
        public TextMeshProUGUI Out2;
        public TextMeshProUGUI Q3;
        public TMP_InputField A3;
        public TextMeshProUGUI Out3;
        public TextMeshProUGUI Q4;
        public TMP_InputField A4;
        public TextMeshProUGUI Out4;
        public GameObject Problem3;
        public TextMeshProUGUI Q5;
        public TMP_InputField A5;
        public TextMeshProUGUI Out5;
        public TextMeshProUGUI Q6;
        public TMP_InputField A6;
        public TextMeshProUGUI Out6;
        public TextMeshProUGUI Q7;
        public TMP_InputField A7;
        public TextMeshProUGUI Out7;
        public TextMeshProUGUI Q8;
        public TMP_InputField A8;
        public TextMeshProUGUI Out8;
        public GameObject BridgeBroken;
        public GameObject BridgeFixed;
        
        private int rand, i, AnsIn;
        private List<int> answers = new List<int>(8);
        private List<string> questions= new List<string>(8);
        private bool Correct; //, Check;
        private string Typed;

        void Start()
        {
            for (i = 0; i <= 8; i++)
            {
                if (i <= 4)
                {
                    rand = Random.Range(0, 16); // randomize int from 0 to 15
                    while (answers.Contains(rand))
                    {
                        rand = Random.Range(0, 16); // randomize again if number was already chosen
                    }
                    answers.Add(rand);
                    questions.Add(System.Convert.ToString(answers[i], 2).PadLeft(4, '0'));
                }
                else
                {
                    rand = Random.Range(1, 114) + 15; // randomize int from 16 to 128
                    while (answers.Contains(rand))
                    {
                        rand = Random.Range(1, 114) + 15; // randomize again if number was already chosen
                    }
                    answers.Add(rand);
                    questions.Add(System.Convert.ToString(answers[i], 2).PadLeft(8, '0'));
                }
            }
            Q1.color = new Color32(0, 255, 9, 255); // changes font color to Green
            Q1.text = "A. " + questions[1] + " =";           // set the text to be the binary number repeats for Q2-Q8
            Q2.color = new Color32(0, 255, 9, 255);
            Q2.text = "B. " + questions[2] + " =";
            Q3.color = new Color32(0, 255, 9, 255);
            Q3.text = "C. " + questions[3] + " =";
            Q4.color = new Color32(0, 255, 9, 255);
            Q4.text = "D. " + questions[4] + " =";
            Q5.color = new Color32(0, 255, 9, 255);
            Q5.text = "E. " + questions[5] + " =";
            Q6.color = new Color32(0, 255, 9, 255);
            Q6.text = "F. " + questions[6] + " =";
            Q7.color = new Color32(0, 255, 9, 255);
            Q7.text = "G. " + questions[7] + " =";
            Q8.color = new Color32(0, 255, 9, 255);
            Q8.text = "H. " + questions[8] + " =";
        }
        public void HelloCheck()
        {
            //resets Correct and Check next input
            Correct = true;
            if (!(System.String.IsNullOrEmpty(HelloBox.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    Typed = HelloBox.text;//tests for strings
                    if (Typed != "Hello World")
                    {
                        Audio_Manager.Instance.PlaySound("Incorrect");
                        HelloError.color = new Color32(255, 200, 0, 255); // yellow
                        HelloError.text = "Type in 'Hello World' in the box below,\n" + "to open the door.";
                        Correct = false;
                    }
                }
                catch (System.Exception)
                {
                    Audio_Manager.Instance.PlaySound("Incorrect");
                    HelloError.color = new Color32(255, 200, 0, 255); // yellow
                    HelloError.text = "Type in 'Hello World' in the box below,\n" + "to open the door.";
                    Correct = false;
                }
            }
            if (Correct)
            {
                Audio_Manager.Instance.PlaySound("Correct");
                door.SetActive(false);
            }
        }
        public void BinaryCheckNibl()
        {
            //resets Correct and Check next input
            Correct = true;
            if (!(System.String.IsNullOrEmpty(HelloBox.text))) //Checks if values were inputed skips if no value
            {
                // Question 1
                try
                {
                    AnsIn = int.Parse(A1.text);//tests for strings
                    if (AnsIn != answers[1])
                    {
                        Out1.color = new Color32(255, 200, 0, 255); // yellow
                        Out1.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out1.color = new Color32(0, 255, 255, 255);// cyan
                        Out1.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out1.color = new Color32(255, 200, 0, 255); // yellow
                    Out1.text = "Incorrect, Not a Integer";
                    Correct = false;
                }

                // Question 2
                try
                {
                    AnsIn = int.Parse(A2.text);//tests for strings
                    if (AnsIn != answers[2])
                    {
                        Out2.color = new Color32(255, 200, 0, 255); // yellow
                        Out2.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out2.color = new Color32(0, 255, 255, 255);// cyan
                        Out2.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out2.color = new Color32(255, 200, 0, 255); // yellow
                    Out2.text = "Incorrect, Not a Integer";
                    Correct = false;
                }

                // Question 3
                try
                {
                    AnsIn = int.Parse(A3.text);//tests for strings
                    if (AnsIn != answers[3])
                    {
                        Out3.color = new Color32(255, 200, 0, 255); // yellow
                        Out3.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out3.color = new Color32(0, 255, 255, 255);// cyan
                        Out3.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out3.color = new Color32(255, 200, 0, 255); // yellow
                    Out3.text = "Incorrect, Not a Integer";
                    Correct = false;
                }

                // Question 4
                try
                {
                    AnsIn = int.Parse(A4.text);//tests for strings
                    if (AnsIn != answers[4])
                    {
                        Out4.color = new Color32(255, 200, 0, 255); // yellow
                        Out4.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out4.color = new Color32(0, 255, 255, 255);// cyan
                        Out4.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out4.color = new Color32(255, 200, 0, 255); // yellow
                    Out4.text = "Incorrect, Not a Integer";
                    Correct = false;
                }
            }

            if (Correct)
            {
                Audio_Manager.Instance.PlaySound("Correct");
                Problem2.SetActive(false);
                Problem3.SetActive(true);
            }
            else
            {
                Audio_Manager.Instance.PlaySound("Incorrect");
            }
        }
        public void BinaryCheckByte()
        {
            //resets Correct and Check next input
            Correct = true;
            if (!(System.String.IsNullOrEmpty(HelloBox.text))) //Checks if values were inputed skips if no value
            {
                // Question 5
                try
                {
                    AnsIn = int.Parse(A5.text);//tests for strings
                    if (AnsIn != answers[5])
                    {
                        Out5.color = new Color32(255, 200, 0, 255); // yellow
                        Out5.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out5.color = new Color32(0, 255, 255, 255);// cyan
                        Out5.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out5.color = new Color32(255, 200, 0, 255); // yellow
                    Out5.text = "Incorrect, Not a Integer";
                    Correct = false;
                }

                // Question 6
                try
                {
                    AnsIn = int.Parse(A6.text);//tests for strings
                    if (AnsIn != answers[6])
                    {
                        Out6.color = new Color32(255, 200, 0, 255); // yellow
                        Out6.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out6.color = new Color32(0, 255, 255, 255);// cyan
                        Out6.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out6.color = new Color32(255, 200, 0, 255); // yellow
                    Out6.text = "Incorrect, Not a Integer";
                    Correct = false;
                }

                // Question 7
                try
                {
                    AnsIn = int.Parse(A7.text);//tests for strings
                    if (AnsIn != answers[7])
                    {
                        Out7.color = new Color32(255, 200, 0, 255); // yellow
                        Out7.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out7.color = new Color32(0, 255, 255, 255);// cyan
                        Out7.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out7.color = new Color32(255, 200, 0, 255); // yellow
                    Out7.text = "Incorrect, Not a Integer";
                    Correct = false;
                }

                // Question 8
                try
                {
                    AnsIn = int.Parse(A8.text);//tests for strings
                    if (AnsIn != answers[8])
                    {
                        Out8.color = new Color32(255, 200, 0, 255); // yellow
                        Out8.text = "Incorrect";
                        Correct = false;
                    }
                    else
                    {
                        Out8.color = new Color32(0, 255, 255, 255);// cyan
                        Out8.text = "Correct";
                    }
                }
                catch (System.Exception)
                {
                    Out8.color = new Color32(255, 200, 0, 255); // yellow
                    Out8.text = "Incorrect, Not a Integer";
                    Correct = false;
                }
            }

            if (Correct)
            {
                Audio_Manager.Instance.PlaySound("Correct");
                Problem3.SetActive(false);
                BridgeBroken.SetActive(false);
                BridgeFixed.SetActive(true);
            }
            else
            {
                Audio_Manager.Instance.PlaySound("Incorrect");
            }
        }
    }
}
