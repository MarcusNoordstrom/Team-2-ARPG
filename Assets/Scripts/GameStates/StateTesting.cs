using GameStates;
using UnityEngine;

public class StateTesting : MonoBehaviour {
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StateLogic.GameIsPaused = true;
            StateLogic.CheckState();
        }
    }
}