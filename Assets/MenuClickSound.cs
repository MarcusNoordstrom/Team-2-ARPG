using UnityEngine;

public class MenuClickSound : MonoBehaviour {
    public AudioClip btnClickSound, btnHoverSound;
    AudioSource _source;

    void Awake() {
        _source = GetComponent<AudioSource>();
    }

    public void PlayPressSound() {
        _source.Stop();
        _source.clip = btnClickSound;
        _source.Play();
    }

    public void PlayHoverSound() {
        _source.Stop();
        _source.clip = btnHoverSound;
        _source.Play();
    }
}