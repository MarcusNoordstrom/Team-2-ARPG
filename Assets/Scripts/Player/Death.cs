using GameStates;
using Player;
using Unit;
using UnityEngine;

public class Death : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            FindObjectOfType<PlayerController>().GetComponent<Health>().RevivePlayer();
            //StateLogic.CheckState();
        }
    }

    // public int health;
    //Rename and refactor according to Health Script later and fix-up.
    // public void Die(int health) {
    //     State.CheckState = health <= 0 ? State.GameStates.Dead : State.GameStates.Alive;
    // }
    //
    //Refactor later as above ^^
    // private void Update() {
    //     // Die(Mathf.RoundToInt(GetComponent<Health>().healthSlider.value));
    //     Die(health);
    // }
}