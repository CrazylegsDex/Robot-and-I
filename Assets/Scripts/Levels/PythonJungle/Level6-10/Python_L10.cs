using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine.Audio;
using GameMechanics; // Pulls in the interface from GameMechanics

namespace PythonLevels
{
    public class Python_L10 : MonoBehaviour
    {
        // Public variables
        public TMP_InputField codeInput1;
		public TMP_InputField codeInput2;
		public TMP_InputField codeInput3;
		public TMP_InputField codeInput4;
		public TMP_InputField codeInput5;
		public TMP_InputField codeInput6;
		public TMP_InputField codeInput7;
		public GameObject sendemail;
		public GameObject reset;
        public TextMeshProUGUI programOutput;
		public string explanition;
        public BoxCollider2D levelSprite;
		public GameObject code7;
		private int count = 0;
		private int count2 = 0;
		private string email;
		private string subject;
		private string body;
		
		public void setText(){
			programOutput.text = explanition;
		}
		
		public void setActives(){
			code7.SetActive(false);
			sendemail.SetActive(false);
		}
		
		public void writeEmail(){
			if(count2 == 0){
				email = codeInput7.text;
				programOutput.text = "Subject Line : ";
				count2++;
			}
			else if(count2 == 1){
				subject = codeInput7.text;
				programOutput.text = "Body : ";
				count2++;
			}
			else if(count2 == 2){
				body = codeInput7.text;
				code7.SetActive(false);
				programOutput.text = "Hit next to send email";
				levelSprite.isTrigger = true;		
				count2++;
			}
			else{
				if(count2 == 3){
					programOutput.text = "Email Address : " + email + "\n Subject Line : " + subject + " \n Body : " + body;
					levelSprite.isTrigger = true;
				}
			}
				
		}
		
        public void MainDriver()
        {
			count = 0;
			if(codeInput1.text == "input(\"Email Address : \")"){
				count++;
			}
			if(codeInput2.text == "input(\"Subject Line : \")"){
				count++;
			}
			if(codeInput3.text == "input(\"Body : \")"){
				count++;
			}
			if(codeInput4.text == "email"){
				count++;
			}
			if(codeInput5.text == "subject"){
				count++;
			}
			if(codeInput6.text == "body"){
				count++;
			}
			if(count == 6){
				Audio_Manager.Instance.PlaySound("Correct");
				count2 = 0;
				programOutput.text = "Email Adress: ";
				reset.SetActive(false);
				sendemail.SetActive(true);
				code7.SetActive(true);
			}else{
				Audio_Manager.Instance.PlaySound("Incorrect");
				programOutput.text = "Incorrect";
			}
        }
	}
}
