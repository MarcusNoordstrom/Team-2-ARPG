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

        public IEnumerator FadeOut() {
            var alpha = 1f;

            while (alpha > 0) {
                alpha -= Time.deltaTime;
                _canvasGroup.alpha = alpha;

                yield return null;
            }
        }

        public IEnumerator FadeIn() {
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