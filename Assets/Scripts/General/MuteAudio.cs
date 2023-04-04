using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MuteAudio : MonoBehaviour
{
    public void muteHandler1(bool mute){
		if(mute)
			AudioListener.volume = 0;
		else	
			AudioListener.volume = 1;
	}
	
	public void muteHandler2(bool mute){
		if(mute)
			AudioListener.volume = 0;
		else	
			AudioListener.volume = 1;
	}
	
	public void muteHandler3(bool mute){
		if(mute)
			AudioListener.volume = 0;
		else	
			AudioListener.volume = 1;
	}
}
