using Player;
using Unit;
using UnityEngine;
using UnityEngine.SceneManagement;

/*If we are outside of the namespace ADD:
 Using GameStates*/

//TODO: SHOULD ONLY BE ABLE TO CHANGE STATE IF IT'S THE PLAYER AND NOTHING ELSE!!!!!

//TODO: KEEP MAIN MENU AT BUILD INDEX 0 AND DEATH SCENE AS LAST INDEX PLZ

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        public static bool GameIsPaused;
        [SerializeField] GameObject deathMenu;
        GameObject Player => FindObjectOfType<PlayerController>().gameObject;
        PlayerController PlayerController => Player.GetComponent<PlayerController>();
        bool IsDead => Player.GetComponent<Health>().IsDead;

        public static void CheckState() {
            var logic = FindObjectOfType<StateLogic>();
            logic.ChangeStateInternal();
        }

        public static void ChangeStateTo(State.GameStates stateToChangeTo) {
            State.CheckState = stateToChangeTo;
        }

        void ChangeStateInternal() {
            // State.CheckState = IsDead ? State.GameStates.Dead : State.GameStates.Alive;
            // State.CheckState = GameIsPaused ? State.GameStates.Paused : State.GameStates.Alive;

            if (IsDead && State.CheckState != State.GameStates.Dead)
                State.CheckState = State.GameStates.Dead;
            else if (!IsDead && GameIsPaused && State.CheckState != State.GameStates.Paused)
                State.CheckState = State.GameStates.Paused;
            else
                State.CheckState = State.GameStates.Alive;


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

        void Alive() {
            Debug.Log("Entered State: ALIVE");
            Time.timeScale = 1f;
            PlayerController.enabled = true;
            Player.GetComponent<Health>().enabled = true; //Enables Player Input

            //Unloads Death Menu OLD
            // if (deathMenu.activeInHierarchy) {
            //     Debug.Log("CAME BACK FROM DEATH STATE");
            //     deathMenu.gameObject.SetActive(false);
            // }

            if (SceneManager.sceneCount > 1) SceneManager.UnloadSceneAsync("Death Scene");

            //Unloads Pause Menu
            if (!GameIsPaused) return;
            Debug.Log("CAME BACK FROM PAUSE STATE");
            SceneManager.UnloadSceneAsync(0);
            Time.timeScale = 1f;
        }

        void Dead() {
            //TODO: Fix so ENEMIES does NOT attack when you are dead.
            Debug.Log("Entered State: DEAD");
            PlayerController.enabled = false; //Disables Player Input
            Player.GetComponent<Health>().enabled = false;
            //TODO: Using BuildIndexCount - 1 does not work? out of range exception, WTF?
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }

        void Pause() {
            Debug.Log("Entered State: PAUSED");
            if (SceneManager.sceneCount >= 1 && GameIsPaused) {
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