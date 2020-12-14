using UnityEngine;
using UnityEngine.Audio;

namespace UI.AudioVolume{
    public class VolumeControl : MonoBehaviour
    {
        [SerializeField] protected AudioMixer audioMixer;
        [SerializeField] protected MixerGroupName mixerGroupName;
        protected enum MixerGroupName{
            MasterVolume,
            MusicVolume,
            SfxVolume
        }
    }
    
}

