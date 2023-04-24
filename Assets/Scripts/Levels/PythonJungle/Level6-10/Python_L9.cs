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
    public class Python_L9 : MonoBehaviour
    {
        // Public variables
        public TMP_InputField codeInput1;
		public TMP_InputField codeInput2;
		public TMP_InputField codeInput3;
		public TMP_InputField codeInput4;
		public TMP_InputField codeInput5;
        public TextMeshProUGUI programOutput;
		public string explanition;
		public GameObject watering;
		public GameObject wall;
		public BoxCollider2D levelSprite;
		private int count = 0;
		
		public void setText(){
			programOutput.text = explanition;
		}
		
        public void MainDriver()
        {
			count = 0;
            if(codeInput1.text == "\"red\""){
				count++;
			}
			if(codeInput2.text == "2"){
				count++;
			}
			if(codeInput3.text == "\"blue\""){
				count++;
			}
			if(codeInput4.text == "3"){
				count++;
			}
			if(codeInput5.text == "1"){
				count++;
			}
			if(count == 5){
				programOutput.text = "Congratulations";
				Audio_Manager.Instance.PlaySound("Congratulations");
				watering.SetActive(true);
				wall.SetActive(false);
				levelSprite.isTrigger = true;
			}else{
				programOutput.text = "Incorrect";
				Audio_Manager.Instance.PlaySound("Incorrect");
			}				
        }
	}
}