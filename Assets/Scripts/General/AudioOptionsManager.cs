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

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GameMechanics
{
    public class AudioOptionsManager : MonoBehaviour
    {
        //Access to volume of each sound
        public static float masterVolume { get; private set; } = 0.50f;
        public static float musicVolume { get; private set; } = 0.50f;
        public static float soundEffectsVolume { get; private set; } = 0.50f;
		
		//Access to the Slider UI
		[SerializeField] private Slider MasterSliderValue;
        [SerializeField] private Slider MusicSliderValue;
        [SerializeField] private Slider SoundEffectsSliderValue;

		//Access to the text value of the slider
		[SerializeField] private TextMeshProUGUI masterSliderText;
        [SerializeField] private TextMeshProUGUI musicSliderText;
        [SerializeField] private TextMeshProUGUI soundEffectsSliderText;

        private void Awake()
        {
            // Update each scene with the current value of volumes
            MasterSliderValue.value = masterVolume;
            MusicSliderValue.value = musicVolume;
            SoundEffectsSliderValue.value = soundEffectsVolume;
            masterSliderText.text = ((int)(masterVolume * 100)).ToString();
            musicSliderText.text = ((int)(musicVolume * 100)).ToString();
            soundEffectsSliderText.text = ((int)(soundEffectsVolume * 100)).ToString();
        }

        public void OnMasterSliderValueChange(float value)
        {
			//assigns value to volume
            masterVolume = value;
			musicVolume = value;
			soundEffectsVolume = value;
			
			//updates the slider placement of music and sound effects to go along with master volume value
			MusicSliderValue.value = value;
			SoundEffectsSliderValue.value = value;

			//updates text with the value of the masters, music, and sound efffects new value
            masterSliderText.text = ((int)(value * 100)).ToString();
			musicSliderText.text = ((int)(value * 100)).ToString();
			soundEffectsSliderText.text = ((int)(value * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume();
        }

        public void OnMusicSliderValueChange(float value)
        {
			//assigns value to volume			
            musicVolume = value;

			//updates text with the value of the musics new value
            musicSliderText.text = ((int)(value * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume();
        }

        public void OnSoundEffectsSliderValueChange(float value)
        {
			//assigns value to volume
            soundEffectsVolume = value;

			//updates text with the value of the sound efffects new value
            soundEffectsSliderText.text = ((int)(value * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume();
        }
		
		public void OnMasterSliderValueMute(bool mute)
        {
			// Calls Music and Sound Effects slider for mute
			OnMusicSliderValueMute(mute);
			OnSoundEffectsSliderValueMute(mute);
        }

        public void OnMusicSliderValueMute(bool mute)
        {
            if (mute)
                Audio_Manager.Instance.StopMusic();
            else
                Audio_Manager.Instance.PlayMusic();
        }

        public void OnSoundEffectsSliderValueMute(bool mute)
        {
            if (mute)
                Audio_Manager.Instance.MuteSounds();
            else
                Audio_Manager.Instance.UnMuteSounds();
        }
    }
}