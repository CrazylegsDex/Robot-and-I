using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{
	//[SerializeField] private GameObject dialogueBox;
	//[SerializeField] private TMP_Text textLabel;
	
	public TextMeshProUGUI nameOutput;
	public TextMeshProUGUI dialogueOutput; 

	public Queue<string> sentences;
		
	void Start()
	{
		sentences = new Queue<string> ();
	}
	
	public void StartDialogue (Dialogue_PC_L1 dialogue){
		
	
		Debug.Log("Starting conversation with " + dialogue.name);
		nameOutput.text = dialogue.name;
		sentences.Clear();
		foreach (string sentence in dialogue.sentences){
		
			sentences.Enqueue(sentence);
		
		}	
		
		DisplayNextSentence();
		

	}
	
	public void DisplayNextSentence (){
		
		if(sentences.Count == 0){
				EndDialogue();
				return;
		}	
		
		string sentence = sentences.Dequeue();
		dialogueOutput.text = sentence;
		Debug.Log(sentence);
		
		
	}	
	
	 void EndDialogue(){
		Debug.Log("End of conversation");
		
	}
}
