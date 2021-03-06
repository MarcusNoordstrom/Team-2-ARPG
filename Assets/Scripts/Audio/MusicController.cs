﻿using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour{
        AudioSource audioSource;
        [SerializeField]AudioMixer audioMixer;
        [SerializeField] float fadeSpeed;
        [SerializeField] float startDelay = 0;
        float startVolume;

        void Start(){
            audioSource = GetComponent<AudioSource>();
            startVolume = audioSource.volume;
            audioSource.PlayDelayed(startDelay);
        }

        public void OnDeath(){
            StartCoroutine("AudioFade");
        }
        IEnumerator AudioFade(){
            while (audioSource.volume > -80){
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
}

