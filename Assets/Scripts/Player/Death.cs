using UnityEngine;
using GameStates;
public class Death : MonoBehaviour {
    
    public int healthTest;
    
    //TODO: Rename and refactor according to Health Script later and fix-up.
    public void Die(int health) {
        State.CurrentState = health <= 0 ? State.GameStates.Dead : State.GameStates.Alive;
    }

    private void Update() {
        Die(healthTest);
        if (Input.GetKeyDown(KeyCode.Z)) {
            healthTest--;
        }
    }
}