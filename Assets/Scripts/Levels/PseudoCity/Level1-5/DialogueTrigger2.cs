// This script is the secondary header for the Dialogue
//
// Author: Robot and I Team
// Last modification date: 11-15-2022

using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{
	public Dialogue_PC_L1 dialogue;
	
	public void TriggerDialogue(){
		
		FindObjectOfType<DialogueManager2>().StartDialogue(dialogue);
	}
}
