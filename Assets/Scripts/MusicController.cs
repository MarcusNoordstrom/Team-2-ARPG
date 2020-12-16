using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour{
    AudioSource audioSource;
    [SerializeField]AudioMixer audioMixer;
    [SerializeField] float fadeSpeed;
    float startVolume;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        startVolume = audioSource.volume;
    }

   public void OnDeath(){
       StartCoroutine("AudioFade");
   }
   IEnumerator AudioFade(){
       while (audioSource.volume > 0){
           audioSource.volume -= fadeSpeed * Time.deltaTime;
           yield return new WaitForFixedUpdate();
       }
       audioSource.Stop();
   }

    public void OnResurrect(){
        StopCoroutine("AudioFade");
        audioSource.volume = startVolume;
        audioSource.Play();
    }
}
