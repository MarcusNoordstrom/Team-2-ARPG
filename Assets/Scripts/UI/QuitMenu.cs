using UnityEngine;

public class QuitMenu : MonoBehaviour {
    public GameObject menu;
    bool isOpen;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) isOpen = !isOpen;

        if (isOpen)
            menu.gameObject.SetActive(true);
        else menu.gameObject.SetActive(false);
    }

    public void QuitApplication() {
        Application.Quit();
    }
}