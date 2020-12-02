using UnityEngine;
/*If we are outside of the namespace ADD:
 Using GameStates*/

namespace GameStates {
    public class HowToUseStates : MonoBehaviour {
        
        //This is how you change state to "Alive"
        public void AliveState() {
            State.CurrentState = State.GameStates.Alive;
        }
        
        //This is how you change state to "Dead"
        public void DeadState() {
            State.CurrentState = State.GameStates.Dead;
        }
        
        //Now we can check what state we are in and do something
        private void CheckState() {
            switch (State.CurrentState) {
                case State.GameStates.Alive: {
                    //TODO: Add logic for when Alive
                    //TODO: Enable all player input again
                    //Get <Script == InputScript> <- ENABLE THIS

                    //TESTING TO SEE IF IT WORKS
                    if (Input.GetKeyDown(KeyCode.A)) {
                        Debug.Log("WE ARE ALIVE!");
                    }
                    break;
                }
                case State.GameStates.Dead: {
                    //TODO: Add logic for Death
                    //TODO: Disable all player input
                    //Get <Script == InputScript> <- DISABLE THIS

                    //TESTING TO SEE IF IT WORKS
                    if (Input.GetKeyDown(KeyCode.A)) {
                        Debug.Log("WE ARE DEAD!");
                    }
                    break;
                }
            }
        }

        private void Update() {
            CheckState();
        }
    }
}

