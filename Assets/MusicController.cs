using System;
using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;

public class MusicController : MonoBehaviour, IResurrect{
    AudioSource audioSource => GetComponent<AudioSource>();
    [SerializeField] float fadeSpeed;
    float startVolume;
    private void Start(){
        startVolume = audioSource.volume;
        audioSource.Play();
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

    public void OnResurrect(bool onCorpse){
        StopCoroutine("AudioFade");
        audioSource.volume = startVolume;
        audioSource.Play();
        
    }
}
