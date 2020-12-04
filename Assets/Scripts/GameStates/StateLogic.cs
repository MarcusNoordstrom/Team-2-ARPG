using Player;
using Unit;
using UnityEngine;
using UnityEngine.SceneManagement;

/*If we are outside of the namespace ADD:
 Using GameStates*/

//TODO: SHOULD ONLY BE ABLE TO CHANGE STATE IF IT'S THE PLAYER AND NOTHING ELSE!!!!!

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        [SerializeField] private GameObject deathMenu;
        private GameObject Player => FindObjectOfType<Mover>().gameObject;
        private Mover Mover => Player.GetComponent<Mover>();
        private bool IsDead => Player.GetComponent<Health>().IsDead;

        public static bool GameIsPaused;

        public static void CheckState() {
            StateLogic logic = FindObjectOfType<StateLogic>();
            logic.ChangeStateInternal();
        }

        public static void ChangeStateTo(State.GameStates stateToChangeTo) {
            State.CheckState = stateToChangeTo;
        }
        
        private void ChangeStateInternal() {
            State.CheckState = IsDead ? State.GameStates.Dead : State.GameStates.Alive;

            State.CheckState = GameIsPaused ? State.GameStates.Paused : State.GameStates.Alive;

            switch (State.CheckState) {
                case State.GameStates.Alive: {
                    Alive();
                    break;
                }
                case State.GameStates.Dead: {
                    Dead();
                    break;
                }
                case State.GameStates.Paused: {
                    Pause();
                    break;
                }
            }
        }

        private void Alive() {
            Debug.Log("Entered State: ALIVE");
            Time.timeScale = 1f;
            Mover.enabled = true; //Enables Player Input
            
            //Unloads Death Menu OLD
            // if (deathMenu.activeInHierarchy) {
            //     Debug.Log("CAME BACK FROM DEATH STATE");
            //     deathMenu.gameObject.SetActive(false);
            // }

            if (SceneManager.sceneCount > 1) {
                SceneManager.UnloadSceneAsync("Death Scene");
            } 
            
            //Unloads Pause Menu
            if (SceneManager.sceneCount < 1) return;
            Debug.Log("CAME BACK FROM PAUSE STATE");
            SceneManager.UnloadSceneAsync(0);
            Time.timeScale = 1f;
        }
        
        private void Dead() {
            //TODO: CHANGE FROM GAMEOBJECT TO SCENE.ADDITIVE LOAD
            Debug.Log("Entered State: DEAD");
            Mover.enabled = false; //Disables Player Input
            // deathMenu.gameObject.SetActive(true); //Enables DeathMenu(UI)
            SceneManager.LoadScene("Death Scene", LoadSceneMode.Additive);
        }
        
        void Pause() {
            Debug.Log("Entered State: PAUSED");
            if (SceneManager.sceneCount == 1) {
                SceneManager.LoadScene(0, LoadSceneMode.Additive);
            }
            else {
                GameIsPaused = false;
                CheckState();
            }
            Time.timeScale = 1f;
        }
    }
}