using UnityEngine;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public GameObject mainMenu;
        public GameObject settingsMenu;

        private void Awake() {
            settingsMenu.SetActive(false);
        }

        public void ShowMainMenu() {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        public void ShowSettingsMenu() {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        
        //TODO: Replace with the real Quit function later
        public void QuitGame() {
            Application.Quit();
            Debug.Log("Quitting");
        }
    }
}