using System;
using Player;
using Unit;
using UnityEngine;
using UnityEngine.SceneManagement;

/*If we are outside of the namespace ADD:
 Using GameStates*/

namespace GameStates {
    public class StateLogic : MonoBehaviour {
        private bool IsDead => GetComponent<Health>().IsDead;

        //Now we can check what state we are in and do something
        private void CurrentState() {
            if (IsDead)
                State.CheckState = State.GameStates.Dead;

            if (Input.GetKeyDown(KeyCode.Escape) && State.CheckState != State.GameStates.Paused) {
                State.CheckState = State.GameStates.Paused;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && State.CheckState == State.GameStates.Paused) {
                
                State.CheckState = State.GameStates.Alive;
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
                    Paused();
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
        }
        
        private void Paused() {
            //TODO: Add logic for pause.
            //TODO: Player can still ROTATE when timescale is 0f fix this (Later)!
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
            Time.timeScale = 0f;
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