using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioVolume{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : VolumeControl{
        private void Start(){
            var startVolume = PlayerPrefs.GetFloat(mixerGroupName.ToString(), 1);
            GetComponent<Slider>().value = startVolume;
            SetVolume(startVolume);
        }
        public void SetVolume(float volumeValue){
            audioMixer.SetFloat(mixerGroupName.ToString(), Mathf.Log10(volumeValue)*20);
            PlayerPrefs.SetFloat(mixerGroupName.ToString(),volumeValue);
        }
    }
    
}
//Referance:
//https://stackoverflow.com/questions/46529147/how-to-set-a-mixers-volume-to-a-sliders-volume-in-unity

