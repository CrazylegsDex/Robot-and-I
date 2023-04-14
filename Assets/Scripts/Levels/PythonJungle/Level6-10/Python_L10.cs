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
        public TextMeshProUGUI programOutput;
		public string explanition;
        public BoxCollider2D levelSprite;
		private int count = 0;
		
		public void setText(){
			programOutput.text = explanition;
		}
		
        public void MainDriver()
        {
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
				programOutput.text = "Congratulations";
				levelSprite.isTrigger = true;
			}else{
				Audio_Manager.Instance.PlaySound("Incorrect");
				programOutput.text = "Incorrect";
			}
        }
	}
}
