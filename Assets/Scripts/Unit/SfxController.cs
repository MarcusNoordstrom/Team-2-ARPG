using UnityEngine;
using System.Collections.Generic;

namespace Unit{
    [RequireComponent(typeof(AudioSource))]
    public class SfxController : MonoBehaviour{
        [SerializeField] UnitSfxClip[] sfxClips;
        [SerializeField] Dictionary<UnitSfxId, AudioClip> sfxClipsDictionary = new Dictionary<UnitSfxId, AudioClip>();
        [SerializeField]AudioSource audioSource2D, audioSource3D;

        void Awake(){
            foreach (var sfxClip in sfxClips){
                sfxClipsDictionary.Add(sfxClip.id, sfxClip.audioClip);
            }
        }
        public void OnPlay(UnitSfxId id){
            audioSource3D.PlayOneShot(sfxClipsDictionary[id]);
        }
        public void OnPlay2D(UnitSfxId id){
            audioSource2D.Stop();
            audioSource2D.clip = sfxClipsDictionary[id];
            audioSource2D.Play();
        }
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
