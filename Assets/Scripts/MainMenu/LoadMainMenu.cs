using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {
    //TODO: Maybe change how we load later and implement that.
    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }
}