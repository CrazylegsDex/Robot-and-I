// This script provides control and completion Level 0
//
// Author: Robot and I Team
// Last modification date: 4-19-23

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using GameMechanics; // Pulls in the interface from GameMechanics

namespace LVL_0
{
    public class Level_0 : MonoBehaviour
    {
        public Levels CurrentLevel;
        public Level_Pop_Up PopUp;
        public TextMeshProUGUI ExitLvlTx;   //
        public GameObject Problem1;         // GameObject to hide when the "Hello World" Question is Solved
        public TMP_InputField HelloBox;     // Input field expecting "Hello World" to be typed in
        public TextMeshProUGUI HelloError;  // Text feild that will display when there is an error of the input
        public GameObject door;             // GameObject that blocks the players path from screens 1 and 2, to screen 3 and exit
        public GameObject screen2F;         // Final Screen State for Screen 2
        public GameObject Dialogue3;        // Screen 3 to be revealed upon completion of Problem1
        public GameObject Problem2;         // GameObject to hide when the Nybl Question is Solved
        public TextMeshProUGUI Q1;          // TextMesh Object that will contain the 
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
        public GameObject screen3F;         // Final Screen State for Screen 3
        public GameObject BridgeBroken;
        public GameObject BridgeFixed;
        
        private int rand, i, AnsIn, Tot;
        private List<int> answers = new List<int>(8);
        private List<string> questions= new List<string>(8);
        private string Typed, ExitType;

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
            Q5.text = "E. " + questions[5].Substring(0, 4) + " " + questions[5].Substring(4, 4) + " =";
            Q6.color = new Color32(0, 255, 9, 255);
            Q6.text = "F. " + questions[6].Substring(0, 4) + " " + questions[6].Substring(4, 4) + " =";
            Q7.color = new Color32(0, 255, 9, 255);
            Q7.text = "G. " + questions[7].Substring(0, 4) + " " + questions[7].Substring(4, 4) + " =";
            Q8.color = new Color32(0, 255, 9, 255);
            Q8.text = "H. " + questions[8].Substring(0, 4) + " " + questions[8].Substring(4, 4) + " =";

            
            if (CurrentLevel.Completed)
            {
                PopUp.QuitLocationLevel = "PseudoIsland";
                ExitLvlTx.text = "<- Exit to World";
            }
            else
            {
                PopUp.QuitLocationLevel = "MainMenu";
                ExitLvlTx.text = "<- Exit to Title";
            }
        }
        public void HelloCheck()
        {
            HelloError.text = "";
            if (!(System.String.IsNullOrEmpty(HelloBox.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    Typed = HelloBox.text;//tests for strings
                    if (Typed == "Hello World")
                    {
                        Audio_Manager.Instance.PlaySound("Correct");
                        screen2F.SetActive(true);
                        Dialogue3.SetActive(true);
                        Problem1.SetActive(false);
                        door.SetActive(false);
                    }
                    else
                    {
                        Audio_Manager.Instance.PlaySound("Incorrect");
                        HelloError.color = new Color32(255, 200, 0, 255); // yellow
                        HelloError.text = "Type in 'Hello World' in the box below,\n" + "to open the door.";
                    }
                }
                catch (System.Exception)
                {
                    Audio_Manager.Instance.PlaySound("Incorrect");
                    HelloError.color = new Color32(255, 200, 0, 255); // yellow
                    HelloError.text = "Type in 'Hello World' in the box below,\n" + "to open the door.";
                }
            }
        }
        public void BinaryCheckNibl()
        {

            Tot = 0;
            // Question 1
            Out1.text = "";
            if (!(System.String.IsNullOrEmpty(A1.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A1.text);//tests for strings
                    if (AnsIn != answers[1])
                    {
                        Out1.color = new Color32(255, 200, 0, 255); // yellow
                        Out1.text = "Incorrect";
                    }
                    else
                    {
                        Out1.color = new Color32(0, 255, 255, 255);// cyan
                        Out1.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out1.color = new Color32(255, 200, 0, 255); // yellow
                    Out1.text = "Incorrect, Not a Integer";
                }
            }

            // Question 2
            Out2.text = "";
            if (!(System.String.IsNullOrEmpty(A2.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A2.text);//tests for strings
                    if (AnsIn != answers[2])
                    {
                        Out2.color = new Color32(255, 200, 0, 255); // yellow
                        Out2.text = "Incorrect";
                    }
                    else
                    {
                        Out2.color = new Color32(0, 255, 255, 255);// cyan
                        Out2.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out2.color = new Color32(255, 200, 0, 255); // yellow
                    Out2.text = "Incorrect, Not a Integer";
                }
            }

            // Question 3
            Out3.text = "";
            if (!(System.String.IsNullOrEmpty(A3.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A3.text);//tests for strings
                    if (AnsIn != answers[3])
                    {
                        Out3.color = new Color32(255, 200, 0, 255); // yellow
                        Out3.text = "Incorrect";
                    }
                    else
                    {
                        Out3.color = new Color32(0, 255, 255, 255);// cyan
                        Out3.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out3.color = new Color32(255, 200, 0, 255); // yellow
                    Out3.text = "Incorrect, Not a Integer";
                }
            }

            // Question 4
            Out4.text = "";
            if (!(System.String.IsNullOrEmpty(A4.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A4.text);//tests for strings
                    if (AnsIn != answers[4])
                    {
                        Out4.color = new Color32(255, 200, 0, 255); // yellow
                        Out4.text = "Incorrect";
                    }
                    else
                    {
                        Out4.color = new Color32(0, 255, 255, 255);// cyan
                        Out4.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out4.color = new Color32(255, 200, 0, 255); // yellow
                    Out4.text = "Incorrect, Not a Integer";
                }
            }

            if (Tot == 4)
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
            
            Tot = 0;
            // Question 5
            Out5.text = "";
            if (!(System.String.IsNullOrEmpty(A5.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A5.text);//tests for strings
                    if (AnsIn != answers[5])
                    {
                        Out5.color = new Color32(255, 200, 0, 255); // yellow
                        Out5.text = "Incorrect";
                    }
                    else
                    {
                        Out5.color = new Color32(0, 255, 255, 255);// cyan
                        Out5.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out5.color = new Color32(255, 200, 0, 255); // yellow
                    Out5.text = "Incorrect, Not a Integer";
                }
            }

            // Question 6
            Out6.text = "";
            if (!(System.String.IsNullOrEmpty(A6.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A6.text);//tests for strings
                    if (AnsIn != answers[6])
                    {
                        Out6.color = new Color32(255, 200, 0, 255); // yellow
                        Out6.text = "Incorrect";
                    }
                    else
                    {
                        Out6.color = new Color32(0, 255, 255, 255);// cyan
                        Out6.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out6.color = new Color32(255, 200, 0, 255); // yellow
                    Out6.text = "Incorrect, Not a Integer";
                }
            }

            // Question 7
            Out7.text = "";
            if (!(System.String.IsNullOrEmpty(A7.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A7.text);//tests for strings
                    if (AnsIn != answers[7])
                    {
                        Out7.color = new Color32(255, 200, 0, 255); // yellow
                        Out7.text = "Incorrect";
                    }
                    else
                    {
                        Out7.color = new Color32(0, 255, 255, 255);// cyan
                        Out7.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out7.color = new Color32(255, 200, 0, 255); // yellow
                    Out7.text = "Incorrect, Not a Integer";
                }
            }

            // Question 8
            Out8.text = "";
            if (!(System.String.IsNullOrEmpty(A8.text))) //Checks if values were inputed skips if no value
            {
                try
                {
                    AnsIn = int.Parse(A8.text);//tests for strings
                    if (AnsIn != answers[8])
                    {
                        Out8.color = new Color32(255, 200, 0, 255); // yellow
                        Out8.text = "Incorrect";
                    }
                    else
                    {
                        Out8.color = new Color32(0, 255, 255, 255);// cyan
                        Out8.text = "Correct";
                        Tot++;
                    }
                }
                catch (System.Exception)
                {
                    Out8.color = new Color32(255, 200, 0, 255); // yellow
                    Out8.text = "Incorrect, Not a Integer";
                }
            }

            if (Tot == 4)
            {
                Audio_Manager.Instance.PlaySound("Correct");
                screen3F.SetActive(true);
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
