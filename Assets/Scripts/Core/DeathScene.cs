using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class DeathScene : MonoBehaviour{
        public void QuitGameFunc() {
            Application.Quit();
            EditorApplication.isPlaying = false;
        }

        public void GotoMainMenu() {
            SceneManager.LoadScene("Main Menu");
        }
    }
}