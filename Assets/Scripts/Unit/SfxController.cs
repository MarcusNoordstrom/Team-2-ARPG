using UnityEngine;
using System.Collections.Generic;

namespace Unit{
    [RequireComponent(typeof(AudioSource))]
    public class SfxController : MonoBehaviour{
        [SerializeField] UnitSfxClip[] sfxClips;
        [SerializeField] Dictionary<UnitSfxId,AudioClip> sfxClipsDictionary = new Dictionary<UnitSfxId, AudioClip>();
        AudioSource audioSource;

        void Awake(){
            audioSource = GetComponent<AudioSource>();
            foreach (var sfxClip in sfxClips){
                sfxClipsDictionary.Add(sfxClip.id,sfxClip.audioClip);
            }
        }

        public void OnPlay(UnitSfxId id){
            audioSource.Stop();
            audioSource.clip = sfxClipsDictionary[id];
            audioSource.Play();
        }
    }
    
    // [CreateAssetMenu(menuName = "Sound/Sfx/Ui")]
    // public class UiSfxClip : ScriptableObject{
    //     public UiSfxId sfx;
    //     public AudioClip audioClip;
    // }
    //
    // [System.Serializable]
    // public enum UiSfxId{
    //     Click,
    //     Hover
    // }
}

