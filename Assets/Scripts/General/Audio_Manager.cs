using System;
using UnityEngine;
using UnityEngine.Audio;

namespace GameMechanics
{
    public class Audio_Manager : MonoBehaviour
    {
        public static Audio_Manager Instance;

        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
        [SerializeField] private AudioMixerGroup masterMixerGroup;
        [SerializeField] private Sound[] sounds;

        private void Awake()
        {
            Instance = this;

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audioClip;
                s.source.loop = s.isLoop;
                s.source.volume = s.volume;

                switch (s.audioType)
                {
                    case Sound.AudioTypes.soundEffect:
                        s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                        break;

                    case Sound.AudioTypes.music:
                        s.source.outputAudioMixerGroup = musicMixerGroup;
                        break;

                    case Sound.AudioTypes.master:
                        s.source.outputAudioMixerGroup = masterMixerGroup;
                        break;
                }

                if (s.playOnAwake)
                    s.source.Play();
            }
        }

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

        public void UpdateMixerVolume1()
        {
            musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
            soundEffectsMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
            masterMixerGroup.audioMixer.SetFloat("masterVolume", Mathf.Log10(AudioOptionsManager.masterVolume) * 20);
        }

    }
}