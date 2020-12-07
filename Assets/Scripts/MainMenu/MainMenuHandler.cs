using GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public GameObject mainMenu;
        public GameObject settingsMenu;
        public Text playBtnTxt;
        public Camera mainMenuCamera;
        StateLogic stateHandler => FindObjectOfType<StateLogic>();
        public AudioListener mainMenuAudioListener => mainMenuCamera.GetComponent<AudioListener>();

        void Awake() {
            settingsMenu.SetActive(false);

            playBtnTxt.text = SceneManager.sceneCount <= 1 ? "Play" : "Resume";
            if (playBtnTxt.text != "Resume") return;
            Destroy(mainMenuAudioListener);
            Destroy(mainMenuCamera);
        }

        public void ShowMainMenu() {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        public void ShowSettingsMenu() {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void Play() {
            if (playBtnTxt.text == "Resume") StateLogic.GameIsPaused = false;
            //StateLogic.ChangeState(State.GameStates.Alive);
        }

        //TODO: Replace with the real Quit function later
        public void QuitGame() {
            Application.Quit();
            Debug.Log("Quitting");
        }
    }
}