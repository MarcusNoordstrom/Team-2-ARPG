using System;
using Player;
using Unit;
using UnityEngine;
using UnityEngine.SceneManagement;

/*If we are outside of the namespace ADD:
 Using GameStates*/

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        [SerializeField] private GameObject deathMenu;
        private bool IsDead => GetComponent<Health>().IsDead;
        public static bool GameIsPaused = false;

        //Now we can check what state we are in and do something
        private void CurrentState() {
            if (IsDead)
                State.CheckState = State.GameStates.Dead;
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (GameIsPaused) {
                    Resume();
                }
                else {
                    Pause();
                }
            }
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
            //TODO: Add logic for when Alive
            Debug.Log("ALIVE");
            Time.timeScale = 1f;
            GetComponent<Mover>().enabled = true; //Enables Player Input
        }
        
        private void Dead() {
            //TODO: Add logic for Death & refactor later
            GetComponent<Mover>().enabled = false; //Disables Player Input
            deathMenu.gameObject.SetActive(true); //Enables DeathMenu(UI)
        }

        public void Resume() {
            SceneManager.UnloadSceneAsync(0);
            Time.timeScale = 1f;
            GameIsPaused = false;
            State.CheckState = State.GameStates.Alive;
        }

        void Pause() {
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
            Time.timeScale = 0f;
            GameIsPaused = true;
            State.CheckState = State.GameStates.Paused;

        }
        private void FixedUpdate() {
            //TODO: Move to Event or other means later when we have more structure on code.
            CurrentState();
        }
    }
}


//This is how you change state to "Alive"
// public void AliveState() {
//     State.CurrentState = State.GameStates.Alive;
// }
        
//This is how you change state to "Dead"
// public void DeadState() {
//     if (IsDead()) {
//         State.CurrentState = State.GameStates.Dead;
//     }
// }