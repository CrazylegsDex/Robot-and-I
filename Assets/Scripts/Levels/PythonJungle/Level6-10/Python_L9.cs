using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Text;
using TMPro;

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
        public BoxCollider2D levelSprite;
		private int count = 0;
		
		public void setText(){
			programOutput.text = explanition;
		}
		
        public void MainDriver()
        {
            if(codeInput1.text == "\"red\""){
				count++;
			}
			if(codeInput2.text == "2"){
				count++;
			}
			if(codeInput3.text == "\"blue\""){
				count++;
			}
			if(codeInput4.text == "5"){
				count++;
			}
			if(codeInput5.text == "1"){
				count++;
			}
			if(count == 5){
				programOutput.text = "Congratulations";
				levelSprite.isTrigger = true;
			}else{
				programOutput.text = "Incorrect";
			}				
        }
	}
}