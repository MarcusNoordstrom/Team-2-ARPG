using UnityEngine;
using GameStates;
using Unit;

public class Death : MonoBehaviour {
    public int health;
    
    //TODO: Rename and refactor according to Health Script later and fix-up.
    public void Die(int health) {
        State.CheckState = health <= 0 ? State.GameStates.Dead : State.GameStates.Alive;
    }

    //TODO: Refactor later as above ^^
    private void Update() {
        // Die(Mathf.RoundToInt(GetComponent<Health>().healthSlider.value));
        Die(health);
    }
}