using GameStates;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public GameObject mainMenu;
        public GameObject settingsMenu;
        public Text playBtnTxt;
        StateLogic stateHandler => FindObjectOfType<StateLogic>();
        
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
                StateLogic.GameIsPaused = false;
                //StateLogic.ChangeState(State.GameStates.Alive);
            }
            else {
                //TODO: LOAD FIRST LEVEL HERE
                //SceneManager.LoadScene("First Level");
            }
        }
        
        //TODO: Replace with the real Quit function later
        public void QuitGame() {
            Application.Quit();
            Debug.Log("Quitting");
        }
    }
}