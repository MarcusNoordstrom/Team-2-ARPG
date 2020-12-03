using GameStates;
using UnityEngine;

public class StateTesting : MonoBehaviour {
    void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        StateLogic.GameIsPaused = true;
        StateLogic.CheckState();

        if (Input.anyKeyDown) {
            StateLogic.ChangeStateTo(EnumerateStates());
        }
    }
    
    private State.GameStates EnumerateStates() {
        KeyCode[] allowedIndex = {KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2};
        State.GameStates[] stateArray = {State.GameStates.Alive, State.GameStates.Dead, State.GameStates.Paused};
        var stateToBe = State.GameStates.Alive;
        for (var i = 1; i < allowedIndex.Length; i++) {
            if (Input.GetKeyDown(allowedIndex[i])) {
                stateToBe = stateArray[i];
            }
        }
        return stateToBe;
    }
}