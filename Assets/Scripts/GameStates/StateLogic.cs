using System;
using Player;
using Unit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*If we are outside of the namespace ADD:
 Using GameStates*/

namespace GameStates {
    [Serializable] public class CheckStateEvent : UnityEvent{}
    public class StateLogic : MonoBehaviour {
        [SerializeField] public CheckStateEvent checkStateEvent;
        
        [SerializeField] private GameObject deathMenu;

        private GameObject Player => FindObjectOfType<Mover>().gameObject;
        
        private bool IsDead => Player.GetComponent<Health>().IsDead;

        public static bool GameIsPaused;

        //SWITCH TO APPROPRIATE STATE
        public void CheckState() {
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
            GetComponent<Mover>().enabled = true; //Enables Player Input
            
            //Unloads Death Menu
            if (deathMenu.activeInHierarchy) {
                Debug.Log("CAME BACK FROM DEATH STATE");
                deathMenu.gameObject.SetActive(false);
            }
            
            //Unloads Pause Menu
            if (SceneManager.sceneCount <= 1) return;
            Debug.Log("CAME BACK FROM PAUSE STATE");
            SceneManager.UnloadSceneAsync(0);
            Time.timeScale = 1f;
        }
        
        private void Dead() {
            Debug.Log("Entered State: DEAD");
            GetComponent<Mover>().enabled = false; //Disables Player Input
            deathMenu.gameObject.SetActive(true); //Enables DeathMenu(UI)
        }
        
        void Pause() {
            Debug.Log("Entered State: PAUSED");
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }

        private void Update() {
            //Check if we should pause the game.
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("GAS");
                GameIsPaused = true;
                checkStateEvent?.Invoke();
            }
        }
    }
}