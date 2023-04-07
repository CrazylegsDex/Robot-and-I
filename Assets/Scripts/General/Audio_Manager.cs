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
//  Robot and I Team - Gave the script Data Persistence
//  and scene management to have audio consistent throughout
//  the game.
//

using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GameMechanics
{
    public class Audio_Manager : MonoBehaviour
    {
        // This class is designed to be a "Singleton" class.
        // This means there will be only one instance of this
        // class at all times.
        public static Audio_Manager Instance { get; private set; }

        //creates mixer groups to assign sounds/songs
        [SerializeField] private AudioMixerGroup masterMixerGroup;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;

        // An array of audio sources to add to this object.
        // The Indices are specifically set as below
        // 0th Index: Audio Clip to play
        // 1st Index: Click Sound Effect
        // 2nd Index: Correct Sound Effect
        // 3rd Index: Incorrect Sound Effect
        [SerializeField] private Sound[] sounds;
		

        // An array of sounds to play in the game.
        // The Indices are specifically set as below
        // 0th Index: MainMenu
        // 1st Index: Island
        // 2nd Index: Pseudo
        // 3rd Index: Python
        // 4th Index: C
        [SerializeField] private AudioClip[] musicClips;

        private AudioSource music = null;

        private void Awake()
        {
            // There should only be one instance at any given time
            if (Instance != null)
            {
                // If found a second instance, destroy new one and keep old instance
                Destroy(gameObject);
                return;
            }

            Instance = this; // If there is no current instance, create a singleton instance
            DontDestroyOnLoad(gameObject); // Don't destroy this script on scene transition

            // Loop through each sound element and add the
            // AudioSource to this object with the sound element's settings.
            for (int i = 0; i < sounds.Length; ++i)
            {
                // Add an AudioSource component to this object
                // Assign to the AudioSource the Clip, Loop and Volume
                sounds[i].source = gameObject.AddComponent<AudioSource>();
                sounds[i].source.clip = sounds[i].audioClip;
                sounds[i].source.loop = sounds[i].isLoop;
                sounds[i].source.volume = sounds[i].volume;

                // If this is the first AudioSource, assign it to change songs
                if (i == 0)
                    music = sounds[i].source;

                // Assign the mixer group to the AudioSource
                switch (sounds[i].audioType)
                {
                    case Sound.AudioTypes.master:
                        sounds[i].source.outputAudioMixerGroup = masterMixerGroup;
                        break;

                    case Sound.AudioTypes.soundEffect:
                        sounds[i].source.outputAudioMixerGroup = soundEffectsMixerGroup;
                        break;

                    case Sound.AudioTypes.music:
                        sounds[i].source.outputAudioMixerGroup = musicMixerGroup;
                        break;
                }

                if (sounds[i].playOnAwake)
                    sounds[i].source.Play();
            }
        }

        // This Unity function is called when the object is enabled and active.
        // This function "Subscribes" to the scene loader
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // This Unity function is called when the behavior becomes disabled or inactive
        // This function "UnSubscribes" to the scene loader
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // This function is called whenever a scene is loaded in Unity.
        // By "Subscribing" to the scene manager with the above functions,
        // this function stays current with each scene that is loaded in the game.
        // This function will check the active scene and play the correct audio
        // for that scene
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Based on the scene, set the musicClip to play
            if (scene.name == "MainMenu")
            {
                music.clip = musicClips[0];
                music.Play();
            }

            if (scene.name.Contains("Island") || scene.name.Contains("Map"))
            {
                music.clip = musicClips[1];
                music.Play();
            }

            if (scene.name.Contains("PS") || scene.name.Contains("Level_0"))
            {
                music.clip = musicClips[2];
                music.Play();
            }

            if (scene.name.Contains("PY"))
            {
                music.clip = musicClips[3];
                music.Play();
            }

            if (scene.name.Contains("CS"))
            {
                music.clip = musicClips[4];
                music.Play();
            }
        }

        // PlayMusic starts and un-mutes the Main Music song
        public void PlayMusic()
        {
            music.mute = false;
            music.Play();
        }

        // StopMusic stops and mutes the Main Music song
        public void StopMusic()
        {
            music.Stop();
            music.mute = true;
        }

        // PlaySound plays a particular sound based on the soundname
        public void PlaySound(string clipname)
        {
            Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
            if (s == null)
            {
                Debug.LogError("Sound: " + clipname + " does NOT exist!");
                return;
            }
            s.source.Play();
        }

		// MuteSounds mutes all the sound effects from playing
        public void MuteSounds()
        {
            for (int i = 1; i < sounds.Length; ++i)
                sounds[i].source.mute = true;
        }

        // UnMuteSounds un-mutes all the sounds effects so they can play
        public void UnMuteSounds()
        {
            for (int i = 1; i < sounds.Length; ++i)
                sounds[i].source.mute = false;
        }

		// Updates each volume groups volume
        public void UpdateMixerVolume()
        {
			masterMixerGroup.audioMixer.SetFloat("masterVolume", Mathf.Log10(AudioOptionsManager.masterVolume) * 20);
            musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
            soundEffectsMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
        }
    }
}
