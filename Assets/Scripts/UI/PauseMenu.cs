using GameStates;
using UnityEngine;
public class PauseMenu : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StateLogic.OnPause();
        }
    }
}