using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue_Trigger_PC_L1 : MonoBehaviour
{	

	public Dialogue_PC_L1 dialogue;
	
	public void TriggerDialogue(){
		
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

	
	}
	
	
}
