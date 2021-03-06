﻿using GameStates;
using UnityEngine;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    [SerializeField] AudioMixerSnapshot OnPlay;
    [SerializeField] AudioMixerSnapshot OnPause;
    void Start() {
        HidePauseMenu();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StateLogic.OnPause();
            
        }
    }

    public void ShowPauseMenu() {
        pauseMenu.SetActive(true);
        OnPause.TransitionTo(0f);
    }

    public void HidePauseMenu() {
        OnPlay.TransitionTo(0.5f);
        pauseMenu.SetActive(false);
    }
}