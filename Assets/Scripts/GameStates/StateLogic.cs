using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        static bool _gameIsPaused;
        static PlayerController _playerController;
        static PauseMenu PauseMenu => FindObjectOfType<PauseMenu>();

        void Start() {
            _playerController = FindObjectOfType<PlayerController>();
        }
        
        
        
        public static void OnPause() {
            _gameIsPaused = !_gameIsPaused;
            if (_gameIsPaused) {
                PauseMenu.ShowPauseMenu();
                Time.timeScale = 0f;
            }
            else {
                PauseMenu.HidePauseMenu();
                Time.timeScale = 1f;
            }
        }

        public static void OnDeath() {
            SceneManager.LoadScene("Death Scene", LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }

        //assigned to button
        public static void OnResurrect() {
            SceneManager.UnloadSceneAsync("Death Scene");
            _playerController.CallOnResurrect(true);
        }

        //assigned to button
        public static void OnResurrectAtCheckpoint() {
            SceneManager.UnloadSceneAsync("Death Scene");
            _playerController.CallOnResurrect(false);
        }
    }
}