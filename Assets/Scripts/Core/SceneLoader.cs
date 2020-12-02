using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Core {
    public class SceneLoader : MonoBehaviour {
        public string sceneToLoad;

        IEnumerator LoadScene() {
            yield return Fader.FadeIn();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
        }

        void OnTriggerEnter(Collider other) {
            if ((1 << other.gameObject.layer) != LayerMask.GetMask("Player")) return;
            StartCoroutine(LoadScene());
        }

        void OnTriggerExit(Collider other) {
            //Todo add on exit shit
        }
    }
}