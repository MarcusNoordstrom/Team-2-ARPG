using Player;
using Unit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        static bool _gameIsPaused;
        static PlayerController _playerController;

        void Start() {
            _playerController = FindObjectOfType<PlayerController>();
        }

        public static void OnPause() {
            _gameIsPaused = !_gameIsPaused;
            if (_gameIsPaused) {
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
                Time.timeScale = 0f;
            }
            else {
                SceneManager.UnloadSceneAsync("Main Menu");
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
            foreach (var resurrect in _playerController.GetComponents<IResurrect>()) {
                Time.timeScale = 1f;
                resurrect.OnResurrect(true);
            }
        }
        
        //assigned to button
        public static void OnResurrectAtCheckpoint() {
            SceneManager.UnloadSceneAsync("Death Scene");
            foreach (var resurrect in _playerController.GetComponents<IResurrect>()) {
                resurrect.OnResurrect(false);
            }
        }
    }
}