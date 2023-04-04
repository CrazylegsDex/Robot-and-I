using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }
	public static float masterVolume { get; private set; }


	[SerializeField] private Slider MusicSliderValue;
	[SerializeField] private Slider SoundEffectsSliderValue;
	[SerializeField] private Slider MasterSliderValue;
	
    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
	[SerializeField] private TextMeshProUGUI masterSliderText;

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        
        musicSliderText.text = ((int)(value * 100)).ToString();
        Audio_Manager.Instance.UpdateMixerVolume1();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        Audio_Manager.Instance.UpdateMixerVolume1();
    }
	
	public void OnMasterSliderValueChange(float value)
    {
        masterVolume = value;

        masterSliderText.text = ((int)(value * 100)).ToString();
        Audio_Manager.Instance.UpdateMixerVolume1();
    }
	
	public void OnMusicSliderValueMute(bool m_IsOn)
    {
        if(!m_IsOn){
			musicVolume = (float)1;
			MusicSliderValue.value = 1;}
		if(m_IsOn){
			musicVolume = (float)0.0001;
			MusicSliderValue.value = (float)0.0001;}

        musicSliderText.text = ((int)(musicVolume * 100)).ToString();
        Audio_Manager.Instance.UpdateMixerVolume1();
    }

    public void OnSoundEffectsSliderValueMute(bool m_IsOn)
    {
        if(!m_IsOn){
			soundEffectsVolume = (float)1;
			SoundEffectsSliderValue.value = 1;
		}
		if(m_IsOn){
			soundEffectsVolume = (float)0.0001;
			SoundEffectsSliderValue.value=(float)0.0001;
		}
			
        soundEffectsSliderText.text = ((int)(soundEffectsVolume * 100)).ToString();
        Audio_Manager.Instance.UpdateMixerVolume1();
    }
	
    public void OnMasterSliderValueMute(bool m_IsOn)
    {
        if(!m_IsOn){
			masterVolume = (float)1;
			MasterSliderValue.value=1;
		}
		if(m_IsOn){
			masterVolume = (float)0.0001;
			MasterSliderValue.value = (float) 0.0001;
		}

        masterSliderText.text = ((int)(masterVolume * 100)).ToString();
        Audio_Manager.Instance.UpdateMixerVolume1();
    }
}