using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioVolume{
    [RequireComponent(typeof(Toggle))]
    public class VolumeToggle : VolumeControl{
        private void Start(){
            var startVolume = PlayerPrefs.GetInt(mixerGroupName.ToString(), 0) != 0;
            GetComponent<Toggle>().isOn = startVolume;
            SetVolume(startVolume);
        }
        public void SetVolume(bool isMute){
            int isMuteOnStart;
            float volume;
            if (isMute){
                isMuteOnStart = 1;
                volume = 0.0001f;
            }
            else{
                isMuteOnStart = 0;
                volume = 1f;
            }
            audioMixer.SetFloat(mixerGroupName.ToString(), Mathf.Log10(volume)*20);
            PlayerPrefs.SetInt(mixerGroupName.ToString(),isMuteOnStart);
        }
    }
}

