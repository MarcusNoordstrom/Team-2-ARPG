using System.Collections;
using UnityEngine;

namespace UI {
    public class Fader : MonoBehaviour {
        static CanvasGroup _canvasGroup;

        void Start() {
            _canvasGroup = GetComponent<CanvasGroup>();
            CanvasEnabler();
        }

        public static IEnumerator FadeOut() {
            var alpha = 1f;

            while (alpha > 0) {
                alpha -= Time.deltaTime;
                _canvasGroup.alpha = alpha;
                yield return null;
            }
        }

        public static IEnumerator FadeIn() {
            var alpha = 0f;

            while (alpha < 1) {
                alpha += Time.deltaTime;
                _canvasGroup.alpha = alpha;

                yield return null;
            }
        }

        static void CanvasEnabler() {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}