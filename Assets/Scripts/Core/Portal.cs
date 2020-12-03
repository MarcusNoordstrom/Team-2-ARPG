using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class Portal : MonoBehaviour {
        public string sceneToLoad;

        Fader _fader;

        void Start() {
            this._fader = GetComponent<Fader>();
        }

        void OnTriggerEnter(Collider other) {
            if ((1 << other.gameObject.layer) != LayerMask.GetMask("Player")) return;
        }

        IEnumerator Transition() {
            yield return this._fader.FadeIn();
            yield return SceneManager.LoadSceneAsync(FindObjectOfType<Portal>().sceneToLoad);
            //Todo Game Designers wants a loading screen, how long should the waiting time be? Also needs to stop movement during the wait period
            //yield return new WaitForSeconds(5f);
            yield return this._fader.FadeOut();
        }

        void OnTriggerExit(Collider other) {
            //Todo add on exit shit
        }
    }
}