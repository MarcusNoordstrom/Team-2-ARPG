using GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public GameObject mainMenu;
        public Text playBtnTxt;
        public Camera mainMenuCamera;
        public AudioListener mainMenuAudioListener => mainMenuCamera.GetComponent<AudioListener>();

        void Awake() {
            playBtnTxt.text = SceneManager.sceneCount <= 1 ? "Play" : "Resume";
            if (playBtnTxt.text != "Resume") return;
            Destroy(mainMenuAudioListener);
            Destroy(mainMenuCamera);
        }

        public void ShowMainMenu() {
            mainMenu.SetActive(true);
        }

        public void Play() {
            if (playBtnTxt.text == "Resume") {
                StateLogic.OnPause();
            }
            else {
                SceneManager.LoadScene(1);
            }
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}