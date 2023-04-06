// Authors:
//	Ordaricc on GitHub
//  Ricky Dev on Youtube
//
// Copyright (c) Novell, Inc. (http://www.novell.com)
//
// Copied from:
//  https://github.com/Ordaricc/Audio-Types
//  All Credit goes to Ordaricc
//
// Modified by:
//  Robot and I Team - Mainly comments and addition of master volume slider 
//  and mute button
//

using System;
using UnityEngine;
using UnityEngine.Audio;

namespace GameMechanics
{
    public class Audio_Manager : MonoBehaviour
    {
        public static Audio_Manager Instance;

		//creates mixer groups to assign sounds/songs
        [SerializeField] private AudioMixerGroup masterMixerGroup;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
		
		//access to the sounds/songs
        [SerializeField] private Sound[] sounds;

        private void Awake()
        {
            Instance = this;

            foreach (Sound s in sounds)
            {
				//creates audio source, clip, loop, and volume access
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audioClip;
                s.source.loop = s.isLoop;
                s.source.volume = s.volume;

				//assigns output mixer group to the correct mixer group
                switch (s.audioType)
                {
					case Sound.AudioTypes.master:
                        s.source.outputAudioMixerGroup = masterMixerGroup;
                        break;
						
                    case Sound.AudioTypes.soundEffect:
                        s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                        break;

                    case Sound.AudioTypes.music:
                        s.source.outputAudioMixerGroup = musicMixerGroup;
                        break;
                }

				//if assigned to play on awake, call play function
                if (s.playOnAwake)
                    s.source.Play();
            }
        }

		//play sound/song function
        public void Play(string clipname)
        {
            Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
            if (s == null)
            {
                Debug.LogError("Sound: " + clipname + " does NOT exist!");
                return;
            }
            s.source.Play();
        }

		//stop sound/song function
        public void Stop(string clipname)
        {
            Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
            if (s == null)
            {
                Debug.LogError("Sound: " + clipname + " does NOT exist!");
                return;
            }
            s.source.Stop();
        }

		//updates volume of mixerr groups
        public void UpdateMixerVolume1()
        {
			masterMixerGroup.audioMixer.SetFloat("masterVolume", Mathf.Log10(AudioOptionsManager.masterVolume) * 20);
            musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
            soundEffectsMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
        }

    }
}