using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// class for dialogue when the DT_overworld scritpt is placed on an npc
// this is what the devloper fills with data.

public class DialogueOver
{
    public string name;
	public string[] sentences;
	public string LevelToLoad;
	public int levelFinished = 0;
	
	public void setLevelFinished(int newLF){
		levelFinished = newLF;
	}
}
