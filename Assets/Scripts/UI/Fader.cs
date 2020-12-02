using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class Fader : MonoBehaviour {
        static CanvasGroup _canvasGroup;

        void Start() {
            _canvasGroup = GetComponent<CanvasGroup>();

            CanvasEnabler();
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(this.transform.parent.gameObject);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.E)) {
                StartCoroutine(Fading());
            }
        }

        public void LoadShit() {
            StartCoroutine(Fading());
        }
        IEnumerator Fading() {
            yield return FadeIn();
            yield return SceneManager.LoadSceneAsync(FindObjectOfType<SceneLoader>().sceneToLoad);
            //Todo Game Designers wants a loading screen, how long should the waiting time be? Also needs to stop movement during the wait period
            yield return new WaitForSeconds(5f);
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