using GameStates;
using UnityEngine;
public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;

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
        MusicController._musicController.OnPause();
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
        MusicController._musicController.OnUnpause();
    }
}