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
        public static float musicVolume { get; private set; }
        public static float soundEffectsVolume { get; private set; }
        public static float masterVolume { get; private set; }
		
		//Access to the Slider UI
		[SerializeField] private Slider MasterSliderValue;
        [SerializeField] private Slider MusicSliderValue;
        [SerializeField] private Slider SoundEffectsSliderValue;

		//Access to the text value of the slider
		[SerializeField] private TextMeshProUGUI masterSliderText;
        [SerializeField] private TextMeshProUGUI musicSliderText;
        [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
        

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
            Audio_Manager.Instance.UpdateMixerVolume1();
        }

        public void OnMusicSliderValueChange(float value)
        {
			//assigns value to volume			
            musicVolume = value;

			//updates text with the value of the musics new value
            musicSliderText.text = ((int)(value * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume1();
        }

        public void OnSoundEffectsSliderValueChange(float value)
        {
			//assigns value to volume
            soundEffectsVolume = value;

			//updates text with the value of the sound efffects new value
            soundEffectsSliderText.text = ((int)(value * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume1();
        }
		
		public void OnMasterSliderValueMute(bool m_IsOn)
        {
            if (!m_IsOn)
            {
				//if not mute, set volume to 100
                masterVolume = (float)1;
				
				//set slider to 100
                MasterSliderValue.value = 1;
            }
            if (m_IsOn)
            {
				//if mute, set volume to 0
                masterVolume = (float)0.0001;
				
				//set slider to 0
                MasterSliderValue.value = (float)0.0001;
            }

			//updates text with the value of the masters new value
            masterSliderText.text = ((int)(masterVolume * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume1();
			
			//call music and sound effects mute to equate the values with master volume, text, and slider values
			OnMusicSliderValueMute(m_IsOn);
			OnSoundEffectsSliderValueMute(m_IsOn);
        }

        public void OnMusicSliderValueMute(bool m_IsOn)
        {
            if (!m_IsOn)
            {
				//if not mute, set volume to 100
                musicVolume = (float)1;
				
				//updates the slider placement of music
                MusicSliderValue.value = 1;
            }
            if (m_IsOn)
            {
				//if mute, set volume to 0
                musicVolume = (float)0.0001;
				
				//updates the slider placement of music to 0
                MusicSliderValue.value = (float)0.0001;
            }

			//updates text with the value of the musics new value
            musicSliderText.text = ((int)(musicVolume * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume1();
        }

        public void OnSoundEffectsSliderValueMute(bool m_IsOn)
        {
            if (!m_IsOn)
            {
				//if not mute, set volume to 100
                soundEffectsVolume = (float)1;
				
				//updates the slider placement of sound effects to 100
                SoundEffectsSliderValue.value = 1;
            }
            if (m_IsOn)
            {
				//if mute, set volume to 0
                soundEffectsVolume = (float)0.0001;
				
				//updates the slider placement of sound effects to 0
                SoundEffectsSliderValue.value = (float)0.0001;
            }
			
			//updates text with the value of the sound efffects new value
            soundEffectsSliderText.text = ((int)(soundEffectsVolume * 100)).ToString();
			
			//updates mixer volume in the audio manager script
            Audio_Manager.Instance.UpdateMixerVolume1();
        }

    }
}