using GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public GameObject mainMenu;
        public GameObject settingsMenu;
        public Text playBtnTxt;

        private void Awake() {
            settingsMenu.SetActive(false);

            playBtnTxt.text = SceneManager.sceneCount <= 1 ? "Play" : "Resume";
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
            if (playBtnTxt.text == "Resume") {
                State.CheckState = State.GameStates.Alive;
            }
            else {
                //TODO: LOAD FIRST LEVEL HERE
            }
        }
        
        //TODO: Replace with the real Quit function later
        public void QuitGame() {
            Application.Quit();
            Debug.Log("Quitting");
        }
    }
}