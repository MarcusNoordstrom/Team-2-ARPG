using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class Fader : MonoBehaviour {
        static CanvasGroup _canvasGroup;

        void Start() {
            _canvasGroup = GetComponent<CanvasGroup>();

            CanvasEnabler();
            DontDestroyOnLoad(this.gameObject);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.E)) {
                StartCoroutine(Fading());
            }
        }

        IEnumerator Fading() {
            yield return FadeIn();
            yield return SceneManager.LoadSceneAsync(FindObjectOfType<SceneLoader>().sceneToLoad);
            yield return FadeOut();
        }

        IEnumerator FadeOut() {
            var alpha = 1f;

            while (alpha > 0) {
                alpha -= Time.deltaTime;
                _canvasGroup.alpha = alpha;

                yield return null;
            }
        }

        static public IEnumerator FadeIn() {
            var alpha = 0f;

            while (alpha < 1) {
                alpha += Time.deltaTime;
                _canvasGroup.alpha = alpha;

                yield return null;
            }
        }

        void CanvasEnabler() {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}