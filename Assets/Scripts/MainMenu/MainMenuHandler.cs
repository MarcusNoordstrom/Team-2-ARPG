using GameStates;
using UnityEditor;
using UnityEngine;

namespace MainMenu {
    public class MainMenuHandler : MonoBehaviour {
        public void ResumeGame() {
            StateLogic.OnPause();
        }

        public void Play() {
            FindObjectOfType<LoadingScreenScript>().StartLoadingScreen("FINAL LEVEL 1");
        }

        public void QuitGame() {
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    }
}