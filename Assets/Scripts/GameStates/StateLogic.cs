using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        static bool _gameIsPaused;

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
        }

        public static void OnResurrect() {
            SceneManager.UnloadSceneAsync("Death Scene");
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        


        // static void Alive(){
        //     Debug.Log("Entered State: ALIVE");
        //     Time.timeScale = 1f;
        //
        //     if (SceneManager.sceneCount > 1) {
        //         SceneManager.UnloadSceneAsync(1);
        //     }
        //
        //     // if (!GameIsPaused) return;
        //     Debug.Log("CAME BACK FROM PAUSE STATE");
        //     Time.timeScale = 1f;
        // }
        //
        // static void Dead() {
        //     //TODO: Fix so ENEMIES does NOT attack when you are dead.
        //     Debug.Log("Entered State: DEAD");
        //     //Disables Player Input
        //     
        //     //TODO: Using BuildIndexCount - 1 does not work? out of range exception, WTF?
        //     SceneManager.LoadScene(3, LoadSceneMode.Additive);
        // }
        //
        // static void Pause() {
        //     Debug.Log("Entered State: PAUSED");
        //     if (SceneManager.sceneCount >= 1 && GameIsPaused) {
        //         SceneManager.LoadScene(0, LoadSceneMode.Additive);
        //     }
        //     else {
        //         GameIsPaused = false;
        //     }
        //     Time.timeScale = 0f;
        // }
    }
}