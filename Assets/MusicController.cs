using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicController : MonoBehaviour{
    public AudioSource gameplayMusicSource, pauseMusicSource;
    [SerializeField] float fadeSpeed;
    float startVolume;
    public static MusicController _musicController;
    
    void Start(){ 
        startVolume = gameplayMusicSource.volume;
        _musicController = this;
    }

   public void OnDeath(){
       StartCoroutine("AudioFade");
   }

   public void OnPause() {
       gameplayMusicSource.Pause();
       pauseMusicSource.Play();
   }

   public void OnUnpause() {
       pauseMusicSource.Pause();
       gameplayMusicSource.Play();
   }
   
   IEnumerator AudioFade(){
       while (gameplayMusicSource.volume > 0){
           gameplayMusicSource.volume -= fadeSpeed * Time.deltaTime;
           yield return new WaitForFixedUpdate();
       }
       gameplayMusicSource.Stop();
   }

    public void OnResurrect(){
        StopCoroutine("AudioFade");
        gameplayMusicSource.volume = startVolume;
        gameplayMusicSource.Play();
    }
}
