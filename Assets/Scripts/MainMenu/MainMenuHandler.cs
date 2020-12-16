using GameStates;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public Text playBtnTxt;
        public Camera mainMenuCamera;
        public AudioListener mainMenuAudioListener => mainMenuCamera.GetComponent<AudioListener>();

        public void ResumeGame() {
            StateLogic.OnPause();
        }

        public void Play() {
            SceneManager.LoadScene(1);
        }

        public void QuitGame() {
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    }
}