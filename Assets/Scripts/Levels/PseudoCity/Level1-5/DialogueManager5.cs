// This script is body of the Dialogue
//
// Author: Robot and I Team
// Last modification date: 11-15-2022

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager5 : MonoBehaviour
{
	// Dialogue Boxes
	public TextMeshProUGUI nameOutput;
	public TextMeshProUGUI dialogueOutput;

	// Objects to hide and turn visible
	public GameObject DialogueObjects;
	public GameObject LessonObjects;

	// String to hold the dialogue
	public Queue<string> sentences;

	void Start()
	{
		sentences = new Queue<string> ();
	}
	
	public void StartDialogue (Dialogue_PC_L1 dialogue)
	{
		Debug.Log("Starting conversation with " + dialogue.name);
		nameOutput.text = dialogue.name;
		sentences.Clear();
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}	
		
		DisplayNextSentence();
	}
	
	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		dialogueOutput.text = sentence;
		Debug.Log(sentence);
	}	
	
	 void EndDialogue()
	 {
		Debug.Log("End of conversation");
		DialogueObjects.SetActive(false);
		LessonObjects.SetActive(true);
	 }
}
