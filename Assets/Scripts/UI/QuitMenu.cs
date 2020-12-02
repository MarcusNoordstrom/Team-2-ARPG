using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour {
    private bool isOpen;
    public GameObject menu;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isOpen = !isOpen;
        }

        if (isOpen) {
            menu.gameObject.SetActive(true);
        }else menu.gameObject.SetActive(false);
    }

    public void QuitApplication() {
        Application.Quit();
    }
}