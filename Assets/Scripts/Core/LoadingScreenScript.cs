using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    [SerializeField] GameObject loading_Bar_Progress;

    float _progressValue;
    public float loadingTime;
    

    void Awake() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    bool ShowLoadingScreen() {
        if (_progressValue < 1f) {
            _progressValue += loadingTime * 0.0001f;
            loading_Bar_Progress.GetComponent<Image>().fillAmount = _progressValue;
        }
        else if (_progressValue >= 1) {
            return true;
        }
        return false;
    }

    public void StartLoadingScreen(string sceneName) {
        StartCoroutine(Load(sceneName));
    }

    public IEnumerator Load(string sceneName) {
        
        SceneManager.LoadScene(sceneName);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        
        while (!ShowLoadingScreen()) {
            ShowLoadingScreen();
            yield return null;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        _progressValue = 0;
    }
}
